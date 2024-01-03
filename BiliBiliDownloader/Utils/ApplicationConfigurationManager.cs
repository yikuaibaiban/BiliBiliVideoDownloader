// /*==========================================================
// Copyright (c) 2021
// BiliBiliVideoDownloader - All rights reserved
// Create By : ykbb
// Create with Jetbrains Rider.
// ===========================================================
// File description:
// ===========================================================
// Date            Name                 Description of Change
// 2021-02-23      ykbb
// ==========================================================*/

using System;
using System.IO;
using BiliBiliDownloader.Entity;
using Newtonsoft.Json;

namespace BiliBiliDownloader.Utils
{
    /// <summary>
    /// 程序配置管理器
    /// </summary>
    public class ApplicationConfigurationManager
    {
        public ConfigModel Config { get; set; }

        private readonly string _configFile = Environment.CurrentDirectory + "\\config.json";

        public ApplicationConfigurationManager()
        {
            if (File.Exists(_configFile))
            {
                Config = JsonConvert.DeserializeObject<ConfigModel>(File.ReadAllText(_configFile));
                ScanAndRepairConfig();
            }
            else
            {
                Config = null;
            }
        }

        /// <summary>
        /// 扫描并修复配置错误
        /// </summary>
        public bool ScanAndRepairConfig()
        {
            if (string.IsNullOrEmpty(Config.SavePath))
            {
                Config = null;
                return false;
            }

            try
            {
                if (!Directory.Exists(Config.SavePath))
                {
                    Directory.CreateDirectory(Config.SavePath);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Config = null;
                return false;
            }

            if (Config.DownloadThreads <= 0) Config.DownloadThreads = 10;
            return true;
        }

        /// <summary>
        /// 保存程序配置
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            if (Config == null)
            {
                Console.WriteLine("未检测到配置，保存失败");
                return false;
            }

            try
            {
                File.WriteAllText(_configFile, JsonConvert.SerializeObject(Config));
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}