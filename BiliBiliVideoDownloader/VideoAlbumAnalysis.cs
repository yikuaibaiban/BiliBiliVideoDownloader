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
using System.Text;
using System.Threading;
using BiliBiliVideoDownloader.Result;
using BiliBiliVideoDownloader.Utils;

namespace BiliBiliVideoDownloader
{
    /// <summary>
    /// 视频页面分析
    /// </summary>
    public static class VideoAlbumAnalysis
    {
        /// <summary>
        /// windows非法路径字符
        /// </summary>
        private static readonly char[] InvalidChar = {'\\', '/', ':', '*', '?', '"', '<', '>', '|'};

        /// <summary>
        /// 待下载链接分析器
        /// </summary>
        /// <param name="obj">待下载地址</param>
        public static void StartAnalysis(object obj)
        {
            if (obj == null)
            {
                Console.WriteLine("获取任务参数失败");
                return;
            }

            var taskParams = (TaskParams) obj;

            // 视频页面源码
            var sourceCode =
                UrlSourceDownloader.GetDownloadSource(new Uri(taskParams.Url), Encoding.UTF8, taskParams.Cookies);
            if (string.IsNullOrEmpty(sourceCode))
            {
                Console.WriteLine("获取页面源码失败");
                return;
            }

            // 提取视频合集列表
            var videoData = VideoPageAnalysis.GetVideoData(sourceCode);
            if (string.IsNullOrEmpty(videoData))
            {
                Console.WriteLine("获取视频合集失败");
                return;
            }

            var convertVideoData = VideoPageAnalysis.ConvertVideoData(videoData);

            Console.WriteLine($"视频任务：{convertVideoData.Bvid}->{convertVideoData.VideoData.Title}");
            Console.WriteLine(
                $"UP主：{convertVideoData.VideoData.Owner.Name}({convertVideoData.VideoData.Owner.Mid})");
            Console.WriteLine($"总计视频数量：{convertVideoData.VideoData.Pages.Count}");

            // 过滤非法文件名
            convertVideoData.VideoData.Title = InvalidChar.Aggregate(convertVideoData.VideoData.Title,
                (current, pathChar) => current.Replace(pathChar.ToString(), "_"));

            var finalSaveRoot = taskParams.SaveRoot + convertVideoData.VideoData.Title + "\\";

            if (!Directory.Exists(finalSaveRoot))
            {
                Directory.CreateDirectory(finalSaveRoot);
            }

            Console.WriteLine("=====================================");

            foreach (var videoDataPage in convertVideoData.VideoData.Pages)
            {
                Console.WriteLine($"{videoDataPage.Cid}->{videoDataPage.Page}->{videoDataPage.Part}");
                // 视频信息
                var downloadSource = UrlSourceDownloader.GetDownloadSource(
                    new Uri(
                        $"http://api.bilibili.com/x/player/playurl?bvid={convertVideoData.Bvid}&cid={videoDataPage.Cid}&qn=80&otype=json&fourk=1"),
                    Encoding.UTF8, taskParams.Cookies, "GET", "application/json;charset=utf-8");

                if (string.IsNullOrEmpty(downloadSource))
                {
                    Console.WriteLine($"{videoDataPage.Page}->{videoDataPage.Part}");
                    Console.WriteLine("获取下载地址失败");
                    continue;
                }

                var downloadData = VideoPageAnalysis.ConvertVideoDownloadData(downloadSource);

                if (downloadData.Code != 0)
                {
                    Console.WriteLine("获取下载地址失败");
                    continue;
                }

                // Console.WriteLine($"下载地址：{downloadData.Data.Durl[0].Url}");

                lock (Program.Tasks)
                {
                    Program.Tasks.Add(new DownloadTask
                    {
                        Id = Guid.NewGuid(),
                        Page = videoDataPage.Page,
                        Title = videoDataPage.Part,
                        Url = downloadData.Data.Durl[0].Url,
                        Referer = taskParams.Url,
                        FinalSaveRoot = finalSaveRoot,
                        Album = convertVideoData.VideoData.Title
                    });
                }

                taskParams.TaskChange?.Invoke(null, null);

                Thread.Sleep(new Random(DateTime.Now.Millisecond).Next(0, 1000));
            }
        }
    }
}