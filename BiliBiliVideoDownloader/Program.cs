using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using BiliBiliVideoDownloader.Result;
using BiliBiliVideoDownloader.Utils;
using System.Linq;

namespace BiliBiliVideoDownloader
{
    public static class Program
    {
        private static string _loginCookies = string.Empty;
        private static string _saveRoot = string.Empty;
        private static int _downloadThreadCount = 10;

        // 下载任务
        public static List<DownloadTask> Tasks { get; set; } = new List<DownloadTask>();

        // 下载线程
        private static List<Thread> DownloaderThreads { get; set; } = new List<Thread>();

        // 自定义任务变更事件
        public delegate void TaskChange(object sender, DownloadRemoveEventArgs args);

        public static event TaskChange OnTaskChange;

        private static void Main(string[] args)
        {
            InitData();

            // 注册任务变更事件
            OnTaskChange += OnTaskChangeHandler;

            // 任务进程
            while (true)
            {
                Console.WriteLine("请输入想要下载的地址：");
                var analysisUrl = Console.ReadLine();

                if (!UrlAddressChecker.Check(analysisUrl))
                {
                    Console.WriteLine("下载地址不存在");
                    return;
                }
                
                var thread = new Thread(VideoAlbumAnalysis.StartAnalysis)
                {
                    IsBackground = true,
                };
                
                thread.Start(new TaskParams
                {
                    Cookies = _loginCookies,
                    Url = analysisUrl,
                    SaveRoot = _saveRoot,
                });
            }
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private static void InitData()
        {
            var applicationConfigurationManager = new ApplicationConfigurationManager();
            if (applicationConfigurationManager.Config == null)
            {
                applicationConfigurationManager.Config = new ConfigModel();

                Console.WriteLine("请输入下载线程数");
                applicationConfigurationManager.Config.DownloadThreads = int.Parse(Console.ReadLine() ?? "10");

                Console.WriteLine("请输入B站登录后的cookies");
                Console.WriteLine("如果选择跳过则无法下载需要登录的部分");
                applicationConfigurationManager.Config.Cookies = Console.ReadLine();

                Console.WriteLine("请输入视频保存位置：");
                applicationConfigurationManager.Config.SavePath = Console.ReadLine();
            }

            applicationConfigurationManager.ScanAndRepairConfig();

            _loginCookies = applicationConfigurationManager.Config.Cookies;
            _saveRoot = applicationConfigurationManager.Config.SavePath;
            _downloadThreadCount = applicationConfigurationManager.Config.DownloadThreads;

            applicationConfigurationManager.Save();
        }

        /// <summary>
        /// 下载任务变更通知
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private static void OnTaskChangeHandler(object sender, DownloadRemoveEventArgs args)
        {
            lock (DownloaderThreads)
            {
                // 清理死进程
                DownloaderThreads.RemoveAll(thread => !thread.IsAlive);

                lock (Tasks)
                {
                    if (args != null)
                    {
                        switch (args.EventType)
                        {
                            case DownloadEventType.Delete:
                                Tasks.RemoveAll(task => task.Id == args.Id);
                                break;
                            case DownloadEventType.Retry:
                            {
                                foreach (var downloadTask in Tasks.Where(task => task.Id == args.Id).ToList())
                                {
                                    downloadTask.IsRunning = false;
                                }

                                break;
                            }
                            case DownloadEventType.Add:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }

                    if (Tasks.Count(task => !task.IsRunning) > 0 && DownloaderThreads.Count < _downloadThreadCount)
                    {
                        foreach (var downloadTask1 in Tasks.Where(downloadTask => !downloadTask.IsRunning)
                            .Take(_downloadThreadCount - DownloaderThreads.Count).ToList())
                        {
                            var task = new Thread(VideoDownloader.DownloaderThread) {IsBackground = true};
                            DownloaderThreads.Add(task);
                            task.Start(new DownloadParam
                            {
                                TaskChange = OnTaskChange,
                                Id = downloadTask1.Id,
                                Cookies = _loginCookies,
                                Url = downloadTask1.Url,
                                Title = downloadTask1.Title,
                                Page = downloadTask1.Page,
                                Referer = downloadTask1.Referer,
                                FinalSaveRoot = downloadTask1.FinalSaveRoot,
                                Album = downloadTask1.Album,
                            });
                            downloadTask1.IsRunning = true;
                        }
                    }

                    if (Tasks.Count == 0)
                        Console.WriteLine("所有任务都已完成");

                    Console.Title =
                        $"当前下载进程：{DownloaderThreads.Count},剩余任务数{Tasks.Count},下载中：{string.Join(",", Tasks.Where(task => task.IsRunning).Select(task => task.Page).ToList())}";
                }
            }
        }
    }
}