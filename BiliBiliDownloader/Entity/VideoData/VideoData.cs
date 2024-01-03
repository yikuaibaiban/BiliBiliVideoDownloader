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

using System.Collections.Generic;

namespace BiliBiliVideoDownloader.Result.VideoData
{
    /// <summary>
    /// 视频信息
    /// </summary>
    public class VideoData
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
        /// 视频数量
        /// </summary>
        public int Videos { get; set; }

        /// <summary>
        /// 视频标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public VideoDataOwner Owner { get; set; }

        /// <summary>
        /// 视频合集信息
        /// </summary>
        public List<VideoDataPage> Pages { get; set; }
    }
}