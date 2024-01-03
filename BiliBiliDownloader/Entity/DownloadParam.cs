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
    /// 下载进程传递参数
    /// </summary>
    public class DownloadParam
    {
        public Main.TaskChange TaskChange { get; set; }
        public Main.TaskProgressUpdate TaskProgressUpdate { get; set; }
        public int Page { get; set; }
        public string Title { get; set; }
        public int Cid { get; set; }
        public string SaveRoot { get; set; }
        public string Referer { get; set; }
        public string Part { get; set; }
        public string Cookies { get; set; }
        public string Bvid { get; set; }
    }
}