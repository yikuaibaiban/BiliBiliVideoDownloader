// /*==========================================================
// Copyright (c) 2021
// BiliBiliDownloader - All rights reserved
// Create By : ykbb
// Create with Jetbrains Rider.
// ===========================================================
// File description:
// ===========================================================
// Date            Name                 Description of Change
// 2021-02-23      ykbb
// ==========================================================*/

namespace BiliBiliDownloader.Entity
{
    public class TaskChangeParam
    {
        public string Title { get; set; }
        public string Bvid { get; set; }
        public int Cid { get; set; }
        public int Page { get; set; }
        public string Part { get; set; }
        public string Referer { get; set; }
        public string Cookies { get; set; }
    }

    public enum TaskChangeType
    {
        Add,
        Remove,
        Retry,
        Refresh,
    }
}