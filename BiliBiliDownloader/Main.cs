using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BiliBiliDownloader.Entity;
using BiliBiliDownloader.Utils;

namespace BiliBiliDownloader
{
    public partial class Main : Form
    {
        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole(); //显示控制台

        // [DllImport("kernel32.dll")]
        // public static extern bool FreeConsole(); //释放控制台、关闭控制台

        public string Cookies { get; set; }
        public string SaveRoot { get; set; }
        public int DownloadThreads { get; set; }

        // 下载任务
        public static List<DownloadTask> Tasks { get; set; } = new List<DownloadTask>();

        // 下载线程
        private static List<Thread> DownloaderThreads { get; set; } = new List<Thread>();

        // 下载任务变动委托
        public delegate void TaskChange(int? threadId, TaskChangeType sender, List<TaskChangeParam> args);

        public event TaskChange OnTaskChange;

        // 下载任务进度刷新
        public delegate void TaskProgressUpdate(int cid, double progress);

        public event TaskProgressUpdate OnTaskProgressUpdate;

        public Main()
        {
            AllocConsole();

            InitializeComponent();

            System.Net.ServicePointManager.DefaultConnectionLimit = 200;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            OnTaskChange += TaskChangeHandler;
            OnTaskProgressUpdate += TaskProgressUpdateHandler;
            Show();
            var applicationConfigurationManager = new ApplicationConfigurationManager();
            if (applicationConfigurationManager.Config == null)
                SystemConfig.PerformClick();
            else
            {
                Cookies = applicationConfigurationManager.Config.Cookies;
                SaveRoot = applicationConfigurationManager.Config.SavePath;
                DownloadThreads = applicationConfigurationManager.Config.DownloadThreads;
            }

            new Thread(UpdateProgress) {IsBackground = true}.Start();

            // 加载上次未完成的任务
            var taskStorageManager = new TaskStorageManager();
            OnTaskChange?.Invoke(Thread.CurrentThread.ManagedThreadId, TaskChangeType.Add,
                taskStorageManager.Tasks.ConvertAll(input => new TaskChangeParam
                {
                    Title = input.Title,
                    Bvid = input.Bvid,
                    Cid = input.Cid,
                    Page = input.Page,
                    Part = input.Part,
                    Referer = input.Referer,
                    Cookies = input.Cookies
                }));
        }

        private void UpdateProgress()
        {
            while (true)
            {
                lock (Tasks)
                {
                    foreach (var downloadTask in Tasks.Where(task => task.IsRunning).ToList())
                    {
                        Lv_TaskList.Invoke((Action) delegate
                        {
                            var findItemWithText = Lv_TaskList.FindItemWithText(downloadTask.Cid.ToString());
                            if (findItemWithText != null)
                            {
                                findItemWithText.SubItems[5].Text = $"{downloadTask.Progress * 100:0.0}%";
                            }
                        });
                    }
                }

                Thread.Sleep(1000);
            }
        }

        private void TaskProgressUpdateHandler(int cid, double progress)
        {
            lock (Tasks)
            {
                var downloadTask = Tasks.Find(task => task.Cid == cid);
                if (downloadTask != null)
                {
                    downloadTask.Progress = progress;
                }
            }
        }

        private void TaskChangeHandler(int? threadId, TaskChangeType changeType, List<TaskChangeParam> args)
        {
            lock (DownloaderThreads)
            {
                // 清理死进程
                if (threadId != null)
                    DownloaderThreads.RemoveAll(thread => thread.ManagedThreadId == threadId);

                lock (Tasks)
                {
                    if (changeType == TaskChangeType.Add)
                    {
                        var downloadTasks = args.ConvertAll(taskChangeParam => new DownloadTask
                        {
                            Page = taskChangeParam.Page,
                            Title = taskChangeParam.Title,
                            Cid = taskChangeParam.Cid,
                            SaveRoot = SaveRoot,
                            Referer = taskChangeParam.Referer,
                            Part = taskChangeParam.Part,
                            IsRunning = false,
                            Progress = 0,
                            Bvid = taskChangeParam.Bvid,
                            Cookies = taskChangeParam.Cookies,
                        }).ToList();
                        Tasks.AddRange(downloadTasks);

                        var viewItems = new List<ListViewItem>();
                        foreach (var taskChangeParam in downloadTasks)
                        {
                            var listViewItem = new ListViewItem {Text = taskChangeParam.Cid.ToString()};
                            listViewItem.SubItems.Add(taskChangeParam.Title);
                            listViewItem.SubItems.Add(taskChangeParam.Bvid);
                            listViewItem.SubItems.Add(taskChangeParam.Page.ToString());
                            listViewItem.SubItems.Add(taskChangeParam.Part);
                            listViewItem.SubItems.Add("0.0%");
                            viewItems.Add(listViewItem);
                        }

                        Lv_TaskList.Invoke((Action) delegate
                        {
                            Lv_TaskList.VirtualListSize = Tasks.Count;
                            Lv_TaskList.Items.AddRange(viewItems.ToArray());
                        });
                    }
                    else if (changeType == TaskChangeType.Remove)
                    {
                        foreach (var taskChangeParam in args)
                        {
                            Tasks.RemoveAll(task => task.Cid == taskChangeParam.Cid);
                            Lv_TaskList.Invoke((Action) delegate
                            {
                                Lv_TaskList.FindItemWithText(taskChangeParam.Cid.ToString())?.Remove();
                            });
                        }
                    }
                    else if (changeType == TaskChangeType.Retry)
                    {
                        foreach (var taskChangeParam in args)
                        {
                            foreach (var downloadTask in Tasks.Where(task => task.Cid == taskChangeParam.Cid))
                            {
                                downloadTask.Progress = 0;
                                downloadTask.IsRunning = false;

                                Lv_TaskList.Invoke((Action) delegate
                                {
                                    var findItemWithText = Lv_TaskList.FindItemWithText(taskChangeParam.Cid.ToString());
                                    if (findItemWithText != null) findItemWithText.SubItems[5].Text = "0.0%";
                                });
                            }
                        }
                    }

                    // 缓存任务到硬盘
                    new TaskStorageManager().Save(Tasks);

                    Console.WriteLine("剩余任务：" + Tasks.Count);
                    Console.WriteLine("下载线程：" + DownloaderThreads.Count);

                    if (Tasks.Count(task => !task.IsRunning) > 0 && DownloaderThreads.Count < DownloadThreads)
                    {
                        foreach (var downloadTask1 in Tasks.Where(downloadTask => !downloadTask.IsRunning)
                            .Take(DownloadThreads - DownloaderThreads.Count).ToList())
                        {
                            var task = new Thread(VideoDownloader.DownloaderThread) {IsBackground = true};
                            DownloaderThreads.Add(task);
                            task.Start(new DownloadParam
                            {
                                TaskChange = OnTaskChange,
                                TaskProgressUpdate = OnTaskProgressUpdate,
                                Page = downloadTask1.Page,
                                Title = downloadTask1.Title,
                                Cid = downloadTask1.Cid,
                                SaveRoot = downloadTask1.SaveRoot,
                                Referer = downloadTask1.Referer,
                                Part = downloadTask1.Part,
                                Cookies = downloadTask1.Cookies,
                                Bvid = downloadTask1.Bvid,
                            });
                            downloadTask1.IsRunning = true;

                            // 暂停0.5-1秒
                            // Thread.Sleep(new Random(DateTime.Now.Millisecond).Next(500, 1500));
                        }
                    }
                }
            }
        }

        private void SystemConfig_Click(object sender, EventArgs e)
        {
            new Config().ShowDialog();
            var applicationConfigurationManager = new ApplicationConfigurationManager();
            Cookies = applicationConfigurationManager.Config.Cookies;
            SaveRoot = applicationConfigurationManager.Config.SavePath;
            DownloadThreads = applicationConfigurationManager.Config.DownloadThreads;
            // OnTaskChange?.Invoke(null, TaskChangeType.Refresh, new List<TaskChangeParam>());
        }

        private void Add_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(url.Text))
            {
                MessageBox.Show("网址不能为空", "错误");
                return;
            }

            new Thread(AlumAnalysis)
            {
                IsBackground = true
            }.Start(url.Text);
        }

        private void AlumAnalysis(object obj)
        {
            var downloadSource = UrlSourceDownloader.GetDownloadSource(new Uri((string) obj), Encoding.UTF8);
            if (string.IsNullOrEmpty(downloadSource)) return;

            var videoData = VideoPageAnalysis.GetVideoData(downloadSource);
            if (string.IsNullOrWhiteSpace(videoData)) return;

            var convertVideoData = VideoPageAnalysis.ConvertVideoData(videoData);

            OnTaskChange?.Invoke(Thread.CurrentThread.ManagedThreadId, TaskChangeType.Add, convertVideoData.VideoData
                .Pages.Select(videoDataPage =>
                    new TaskChangeParam
                    {
                        Title = convertVideoData.VideoData.Title,
                        Bvid = convertVideoData.VideoData.Bvid,
                        Cid = videoDataPage.Cid,
                        Page = videoDataPage.Page,
                        Part = videoDataPage.Part,
                        Referer = (string) obj,
                        Cookies = Cookies,
                    }).ToList());
        }
    }
}