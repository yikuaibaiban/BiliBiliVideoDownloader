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

namespace BiliBiliVideoDownloader.Result.VideoDownload
{
    public class VideoDownloadData
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public VideoDownloadDataData Data { get; set; }
    }
}