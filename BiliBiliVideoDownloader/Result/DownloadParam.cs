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

namespace BiliBiliVideoDownloader.Result
{
    /// <summary>
    /// 下载进程传递参数
    /// </summary>
    public class DownloadParam
    {
        public Program.TaskChange TaskChange { get; set; }
        public string Cookies { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public int Page { get; set; }
        public Guid Id { get; set; }
        public string Referer { get; set; }
        public string FinalSaveRoot { get; set; }
        public string Album { get; set; }
    }
}