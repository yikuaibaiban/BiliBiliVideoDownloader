// /*==========================================================
// Copyright (c) 2021
// BiliBiliVideoDownloader - All rights reserved
// Create By : ykbb
// Create with Jetbrains Rider.
// ===========================================================
// File description:
// ===========================================================
// Date            Name                 Description of Change
// 2021-02-23      ykbb
// ==========================================================*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using BiliBiliDownloader.Entity;
using BiliBiliDownloader.Utils;

namespace BiliBiliDownloader
{
    /// <summary>
    /// 视频下载器
    /// </summary>
    public class VideoDownloader
    {
        /// <summary>
        /// windows路径非法字符
        /// </summary>
        private static readonly char[] InvalidChar = {'\\', '/', ':', '*', '?', '"', '<', '>', '|'};

        /// <summary>
        /// 下载进程
        /// </summary>
        /// <param name="obj">传入参数</param>
        public static void DownloaderThread(object obj)
        {
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}]=>下载线程已启动");
            if (obj == null || obj.GetType() != typeof(DownloadParam))
            {
                Console.WriteLine("下载进程创建失败");
                return;
            }

            var downloadParam = (DownloadParam) obj;

            var downloadSource = UrlSourceDownloader.GetDownloadSource(
                new Uri(
                    $"http://api.bilibili.com/x/player/playurl?bvid={downloadParam.Bvid}&cid={downloadParam.Cid}&qn=80&otype=json&fourk=1"),
                Encoding.UTF8, downloadParam.Cookies, "GET", "application/json;charset=utf-8");

            if (string.IsNullOrEmpty(downloadSource))
            {
                Console.WriteLine("获取下载地址失败");
                return;
            }

            var downloadData = VideoPageAnalysis.ConvertVideoDownloadData(downloadSource);

            if (downloadData.Code != 0)
            {
                Console.WriteLine("获取下载地址失败");
                return;
            }

            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}]=>下载地址已获取");

            // 过滤非法字符名称
            downloadParam.Title = InvalidChar.Aggregate(downloadParam.Title,
                (current, invalidNameChar) => current.Replace(invalidNameChar.ToString(), "_"));
            downloadParam.Part = InvalidChar.Aggregate(downloadParam.Part,
                (current, invalidNameChar) => current.Replace(invalidNameChar.ToString(), "_"));

            // 创建文件夹
            var finalSaveRoot = $"{downloadParam.SaveRoot}\\{downloadParam.Title}\\";
            if (!Directory.Exists(finalSaveRoot))
            {
                Directory.CreateDirectory(finalSaveRoot);
            }

            // 创建文件
            var filePath =
                $"{finalSaveRoot}{downloadParam.Title}_P{downloadParam.Page}{downloadParam.Part}.flv";
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;
            }

            var fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            // 下载
            // 获取文件长度 https://www.cnblogs.com/barrysgy/archive/2011/10/28/2227670.html
            for (var i = 0; i < 5; i++)
            {
                try
                {
                    var request = (HttpWebRequest) WebRequest.Create(new Uri(downloadData.Data.Durl[0].Url));
                    request.Method = "GET";
                    request.Referer = downloadParam.Referer;
                    request.UserAgent =
                        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.182 Safari/537.36 Edg/88.0.705.74";
                    if (!string.IsNullOrEmpty(downloadParam.Cookies))
                        request.Headers.Add("Cookie", downloadParam.Cookies);
                    request.Timeout = 10000;
                    request.ServerCertificateValidationCallback += (sender, certificate, chain, errors) => true;

                    var response = (HttpWebResponse) request.GetResponse();

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        Console.WriteLine("读取下载文件失败");
                        Thread.Sleep(new Random(DateTime.Now.Millisecond).Next(3000, 5000));
                        continue;
                    }

                    var responseStream = response.GetResponseStream();
                    if (responseStream == null || !responseStream.CanRead)
                    {
                        responseStream?.Close();
                        Thread.Sleep(new Random(DateTime.Now.Millisecond).Next(3000, 5000));
                        continue;
                    }

                    Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}]=>正在下载");
                    fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);

                    var tempByte = new byte[10240];
                    var totalSize = response.ContentLength;
                    var currentSize = 0L;
                    var flushCountSize = 0;
                    var readSize = responseStream.Read(tempByte, 0, tempByte.Length);
                    while (readSize > 0)
                    {
                        currentSize += readSize;
                        flushCountSize += readSize;
                        fileStream.Write(tempByte, 0, readSize);
                        if (flushCountSize >= 1024 * 1024 * 5)
                        {
                            fileStream.Flush(true);
                            flushCountSize = 0;
                        }

                        readSize = responseStream.Read(tempByte, 0, tempByte.Length);

                        downloadParam.TaskProgressUpdate?.Invoke(downloadParam.Cid, (double) currentSize / totalSize);
                    }

                    responseStream.Close();

                    fileStream.Close();

                    if (currentSize < totalSize) continue;
                    // 通知移除已经完成的任务
                    downloadParam.TaskChange?.Invoke(Thread.CurrentThread.ManagedThreadId, TaskChangeType.Remove,
                        new List<TaskChangeParam>()
                        {
                            new TaskChangeParam
                            {
                                Title = downloadParam.Title,
                                Bvid = downloadParam.Bvid,
                                Cid = downloadParam.Cid,
                                Page = downloadParam.Page,
                                Part = downloadParam.Part,
                                Referer = downloadParam.Referer,
                                Cookies = downloadParam.Cookies
                            }
                        });
                    return;
                }
                catch (Exception e)
                {
                    fileStream?.Close();
                    Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}===>{e.Message}");
                    Thread.Sleep(new Random(DateTime.Now.Millisecond).Next(3000, 5000));
                }
            }

            downloadParam.TaskChange?.Invoke(Thread.CurrentThread.ManagedThreadId, TaskChangeType.Retry,
                new List<TaskChangeParam>()
                {
                    new TaskChangeParam
                    {
                        Title = downloadParam.Title,
                        Bvid = downloadParam.Bvid,
                        Cid = downloadParam.Cid,
                        Page = downloadParam.Page,
                        Part = downloadParam.Part,
                        Referer = downloadParam.Referer,
                        Cookies = downloadParam.Cookies
                    }
                });
        }
    }
}