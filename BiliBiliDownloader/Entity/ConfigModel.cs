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

namespace BiliBiliDownloader.Entity
{
    /// <summary>
    /// 程序配置模型
    /// </summary>
    public class ConfigModel
    {
        public string Cookies { get; set; }
        public string SavePath { get; set; }
        public int DownloadThreads { get; set; }
    }
}