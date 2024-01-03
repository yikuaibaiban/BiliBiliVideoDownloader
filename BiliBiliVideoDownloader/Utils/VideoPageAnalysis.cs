// /*==========================================================
// Copyright (c) 2021
// BiliBiliVideoDownloader - All rights reserved
// Create By : ykbb
// Create with Jetbrains Rider.
// ===========================================================
// File description:
// ===========================================================
// Date            Name                 Description of Change
// 2021-02-22      ykbb
// ==========================================================*/

using System.Text.Json;
using System.Text.RegularExpressions;
using BiliBiliVideoDownloader.Result.VideoData;
using BiliBiliVideoDownloader.Result.VideoDownload;

namespace BiliBiliVideoDownloader.Utils
{
    public static class VideoPageAnalysis
    {
        /// <summary>
        /// 分析提取视频信息
        /// </summary>
        /// <param name="sourceCode">数据源</param>
        /// <returns>视频信息结果</returns>
        public static string GetVideoData(string sourceCode)
        {
            // 开始：<script>window.__INITIAL_STATE__=
            // 结束：7133f8d165552bc948.js"]};(function(){var s;(s=document.currentScript
            var dataRegex = new Regex(@"window.__INITIAL_STATE__=(.*);\(function\(\)");

            if (!dataRegex.IsMatch(sourceCode))
            {
                return string.Empty;
            }

            var outData = dataRegex.Match(sourceCode).Groups[1];
            return outData.Value;
        }

        /// <summary>
        /// 转换提取的视频信息为对象
        /// </summary>
        /// <param name="videoData">提取的视频信息</param>
        /// <returns>视频信息结果对象</returns>
        public static MainVideoData ConvertVideoData(string videoData)
        {
            var jsonSerializerOptions = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            return JsonSerializer.Deserialize<MainVideoData>(videoData, jsonSerializerOptions);
        }

        /// <summary>
        /// 转换视频下载信息为对象
        /// </summary>
        /// <param name="downloadData"></param>
        /// <returns></returns>
        public static VideoDownloadData ConvertVideoDownloadData(string downloadData)
        {
            var jsonSerializerOptions = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            return JsonSerializer.Deserialize<VideoDownloadData>(downloadData, jsonSerializerOptions);
        }
    }
}