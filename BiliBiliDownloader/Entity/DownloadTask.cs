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

using System;

namespace BiliBiliDownloader.Entity
{
    /// <summary>
    /// 下载任务模型
    /// </summary>
    public class DownloadTask
    {
        public int Page { get; set; }
        public string Title { get; set; }
        public int Cid { get; set; }
        public string SaveRoot { get; set; }
        public string Referer { get; set; }
        public string Part { get; set; }
        public bool IsRunning { get; set; }
        public double Progress { get; set; }
        public string Bvid { get; set; }
        public string Cookies { get; set; }
    }
}