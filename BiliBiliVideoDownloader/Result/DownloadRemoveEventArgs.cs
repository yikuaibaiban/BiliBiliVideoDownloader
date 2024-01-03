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
    /// 下载任务变更模型
    /// </summary>
    public class DownloadRemoveEventArgs : EventArgs
    {
        /// <summary>
        /// 任务类型
        /// </summary>
        public DownloadEventType EventType { get; set; }

        /// <summary>
        /// 待移除的任务
        /// </summary>
        public Guid? Id { get; set; }
    }

    public enum DownloadEventType
    {
        Add,
        Retry,
        Delete
    }
}