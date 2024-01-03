using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using BiliBiliDownloader.Entity;
using BiliBiliDownloader.Utils;

namespace BiliBiliDownloader
{
    public partial class Config : Form
    {
        private ApplicationConfigurationManager _applicationConfigurationManager;

        public Config()
        {
            InitializeComponent();
            _applicationConfigurationManager = new ApplicationConfigurationManager();
        }

        private void Config_Load(object sender, EventArgs e)
        {
            if (_applicationConfigurationManager.Config == null)
            {
                _applicationConfigurationManager.Config = new ConfigModel();
                Nup_DownloadThreads.Value = 10;
            }
            else
            {
                Lb_SavePath.Text = _applicationConfigurationManager.Config.SavePath;
                Tb_Cookies.Text = _applicationConfigurationManager.Config.Cookies;
                Nup_DownloadThreads.Value = _applicationConfigurationManager.Config.DownloadThreads;
            }
        }

        private void SelectSaveDir_Click(object sender, EventArgs e)
        {
            var folderDialog = new FolderBrowserDialog {ShowNewFolderButton = true};
            var dialogResult = folderDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                Lb_SavePath.Text = folderDialog.SelectedPath;
            }
        }

        private void SaveConfig_Click(object sender, EventArgs e)
        {
            _applicationConfigurationManager.Config.Cookies = Tb_Cookies.Text;
            _applicationConfigurationManager.Config.DownloadThreads = (int) Nup_DownloadThreads.Value;
            _applicationConfigurationManager.Config.SavePath = Lb_SavePath.Text;

            if (!_applicationConfigurationManager.ScanAndRepairConfig())
            {
                MessageBox.Show("配置检测未通过", "错误");
                return;
            }

            if (!_applicationConfigurationManager.Save())
            {
                MessageBox.Show("保存失败", "错误");
            }

            // this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}