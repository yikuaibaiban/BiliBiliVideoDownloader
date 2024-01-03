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

namespace BiliBiliVideoDownloader.Result.VideoData
{
    /// <summary>
    /// 视频页面数据
    /// </summary>
    public class MainVideoData
    {
        /// <summary>
        /// 旧视频编号
        /// </summary>
        public string Avid { get; set; }

        /// <summary>
        /// 新视频编号
        /// </summary>
        public string Bvid { get; set; }

        /// <summary>
        /// 视频信息
        /// </summary>
        public VideoData VideoData { get; set; }
    }
}