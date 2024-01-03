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

namespace BiliBiliVideoDownloader.Result
{
    /// <summary>
    /// 任务传递参数
    /// </summary>
    public class TaskParams
    {
        public Program.TaskChange TaskChange { get; set; }
        public string Cookies { get; set; }
        public string Url { get; set; }
        public string SaveRoot { get; set; }
    }
}