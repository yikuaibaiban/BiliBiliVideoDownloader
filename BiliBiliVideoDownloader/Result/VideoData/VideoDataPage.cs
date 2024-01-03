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
    /// 视频合集单视频信息
    /// </summary>
    public class VideoDataPage
    {
        /// <summary>
        /// cid(视频编号)
        /// </summary>
        public int Cid { get; set; }

        /// <summary>
        /// 视频序号
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 上传来源
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Part { get; set; }

        /// <summary>
        /// duration
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// vid
        /// </summary>
        public string Vid { get; set; }

        /// <summary>
        /// weblink
        /// </summary>
        public string Weblink { get; set; }

        /// <summary>
        /// 视频尺寸信息
        /// </summary>
        public VideoDataPagesDimension Dimension { get; set; }
    }
}