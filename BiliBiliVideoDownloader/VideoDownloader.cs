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
using System.IO;
using System.Linq;
using System.Net;
using BiliBiliVideoDownloader.Result;

namespace BiliBiliVideoDownloader
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
            if (obj == null || obj.GetType() != typeof(DownloadParam))
            {
                Console.WriteLine("下载进程创建失败");
                return;
            }

            var downloadParam = (DownloadParam) obj;

            // 过滤非法字符名称
            downloadParam.Title = InvalidChar.Aggregate(downloadParam.Title,
                (current, invalidNameChar) => current.Replace(invalidNameChar.ToString(), "_"));

            // 创建文件
            var filePath =
                $"{downloadParam.FinalSaveRoot}{downloadParam.Album}_P{downloadParam.Page}{downloadParam.Title}.flv";

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

            using var fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            // 下载
            // 获取文件长度 https://www.cnblogs.com/barrysgy/archive/2011/10/28/2227670.html
            try
            {
                var request = (HttpWebRequest) WebRequest.Create(new Uri(downloadParam.Url));
                request.Method = "GET";
                request.Referer = downloadParam.Referer;
                request.UserAgent =
                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.182 Safari/537.36 Edg/88.0.705.74";
                if (!string.IsNullOrEmpty(downloadParam.Cookies))
                    request.Headers.Add("Cookie", downloadParam.Cookies);
                request.ServerCertificateValidationCallback += (sender, certificate, chain, errors) => true;

                var response = (HttpWebResponse) request.GetResponse();

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    Console.WriteLine("读取下载文件失败");
                    downloadParam.TaskChange?.Invoke(null, new DownloadRemoveEventArgs()
                    {
                        EventType = DownloadEventType.Retry,
                        Id = downloadParam.Id
                    });
                    return;
                }

                using var responseStream = response.GetResponseStream();
                if (responseStream == null || !responseStream.CanRead)
                {
                    Console.WriteLine("读取下载文件失败");
                    downloadParam.TaskChange?.Invoke(null, new DownloadRemoveEventArgs()
                    {
                        EventType = DownloadEventType.Retry,
                        Id = downloadParam.Id
                    });
                    return;
                }

                var tempByte = new byte[1024];
                var totalSize = 0;
                var readSize = responseStream.Read(tempByte, 0, tempByte.Length);
                while (readSize > 0)
                {
                    totalSize += readSize;
                    fileStream.Write(tempByte, 0, readSize);
                    if (totalSize >= 1024 * 1024 * 5)
                    {
                        fileStream.Flush(true);
                        totalSize = 0;
                    }

                    readSize = responseStream.Read(tempByte, 0, tempByte.Length);
                }

                fileStream.Close();
                fileStream.Dispose();

                // 通知移除已经完成的任务
                downloadParam.TaskChange?.Invoke(null, new DownloadRemoveEventArgs()
                {
                    EventType = DownloadEventType.Delete,
                    Id = downloadParam.Id
                });
            }
            catch (Exception e)
            {
                fileStream.Close();
                fileStream.Dispose();
                Console.WriteLine(e);
                downloadParam.TaskChange?.Invoke(null, new DownloadRemoveEventArgs()
                {
                    EventType = DownloadEventType.Retry,
                    Id = downloadParam.Id
                });
            }
        }
    }
}