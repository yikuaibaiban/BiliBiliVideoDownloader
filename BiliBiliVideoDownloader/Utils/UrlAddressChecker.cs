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

namespace BiliBiliVideoDownloader.Utils
{
    /// <summary>
    /// url地址检测工具
    /// </summary>
    public static class UrlAddressChecker
    {
        public static bool Check(string url)
        {
            if (string.IsNullOrEmpty(url)) return false;
            return true;
        }
    }
}