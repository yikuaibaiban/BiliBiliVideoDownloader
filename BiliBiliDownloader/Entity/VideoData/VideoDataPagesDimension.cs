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
    /// 视频尺寸信息
    /// </summary>
    public class VideoDataPagesDimension
    {
        /// <summary>
        /// 视频宽度
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 视频高度
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// 旋转角度
        /// </summary>
        public int Rotate { get; set; }
    }
}