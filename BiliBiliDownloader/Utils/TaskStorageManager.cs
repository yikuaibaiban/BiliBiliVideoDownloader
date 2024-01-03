// /*==========================================================
// Copyright (c) 2021
// BiliBiliDownloader - All rights reserved
// Create By : ykbb
// Create with Jetbrains Rider.
// ===========================================================
// File description:
// ===========================================================
// Date            Name                 Description of Change
// 2021-02-24      ykbb
// ==========================================================*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows.Forms;
using BiliBiliDownloader.Entity;
using Newtonsoft.Json;

namespace BiliBiliDownloader.Utils
{
    public class TaskStorageManager
    {
        public List<DownloadTask> Tasks;
        private readonly string _storageFile = Environment.CurrentDirectory + "\\task.json";

        public TaskStorageManager()
        {
            Tasks = File.Exists(_storageFile)
                ? JsonConvert.DeserializeObject<List<DownloadTask>>(File.ReadAllText(_storageFile))
                : new List<DownloadTask>();
        }

        public bool Save(List<DownloadTask> tasks)
        {
            Tasks = tasks;

            try
            {
                File.WriteAllText(_storageFile, JsonConvert.SerializeObject(tasks));
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