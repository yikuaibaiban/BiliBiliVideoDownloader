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
    /// 视频作者信息
    /// </summary>
    public class VideoDataOwner
    {
        /// <summary>
        /// 作者id
        /// </summary>
        public int Mid { get; set; }

        /// <summary>
        /// 作者姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 作者头像
        /// </summary>
        public string Face { get; set; }
    }
}