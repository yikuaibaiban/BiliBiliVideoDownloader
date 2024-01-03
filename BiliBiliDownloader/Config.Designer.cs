
namespace BiliBiliDownloader
{
    partial class Config
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Tb_Cookies = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SelectSaveDir = new System.Windows.Forms.Button();
            this.Lb_SavePath = new System.Windows.Forms.Label();
            this.SaveConfig = new System.Windows.Forms.Button();
            this.Nup_DownloadThreads = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize) (this.Nup_DownloadThreads)).BeginInit();
            this.SuspendLayout();
            // 
            // Tb_Cookies
            // 
            this.Tb_Cookies.Location = new System.Drawing.Point(12, 24);
            this.Tb_Cookies.Multiline = true;
            this.Tb_Cookies.Name = "Tb_Cookies";
            this.Tb_Cookies.Size = new System.Drawing.Size(451, 149);
            this.Tb_Cookies.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 241);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "保存目录";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "登录的cookies";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 188);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "下载线程数量";
            // 
            // SelectSaveDir
            // 
            this.SelectSaveDir.Location = new System.Drawing.Point(12, 260);
            this.SelectSaveDir.Name = "SelectSaveDir";
            this.SelectSaveDir.Size = new System.Drawing.Size(75, 23);
            this.SelectSaveDir.TabIndex = 5;
            this.SelectSaveDir.Text = "修改目录";
            this.SelectSaveDir.UseVisualStyleBackColor = true;
            this.SelectSaveDir.Click += new System.EventHandler(this.SelectSaveDir_Click);
            // 
            // Lb_SavePath
            // 
            this.Lb_SavePath.AutoSize = true;
            this.Lb_SavePath.Location = new System.Drawing.Point(10, 286);
            this.Lb_SavePath.Name = "Lb_SavePath";
            this.Lb_SavePath.Size = new System.Drawing.Size(0, 12);
            this.Lb_SavePath.TabIndex = 6;
            // 
            // SaveConfig
            // 
            this.SaveConfig.Location = new System.Drawing.Point(388, 334);
            this.SaveConfig.Name = "SaveConfig";
            this.SaveConfig.Size = new System.Drawing.Size(75, 23);
            this.SaveConfig.TabIndex = 7;
            this.SaveConfig.Text = "保存";
            this.SaveConfig.UseVisualStyleBackColor = true;
            this.SaveConfig.Click += new System.EventHandler(this.SaveConfig_Click);
            // 
            // Nup_DownloadThreads
            // 
            this.Nup_DownloadThreads.Location = new System.Drawing.Point(16, 203);
            this.Nup_DownloadThreads.Maximum = new decimal(new int[] {20, 0, 0, 0});
            this.Nup_DownloadThreads.Name = "Nup_DownloadThreads";
            this.Nup_DownloadThreads.Size = new System.Drawing.Size(120, 21);
            this.Nup_DownloadThreads.TabIndex = 8;
            // 
            // Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 367);
            this.Controls.Add(this.Nup_DownloadThreads);
            this.Controls.Add(this.SaveConfig);
            this.Controls.Add(this.Lb_SavePath);
            this.Controls.Add(this.SelectSaveDir);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Tb_Cookies);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Config";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "参数配置";
            this.Load += new System.EventHandler(this.Config_Load);
            ((System.ComponentModel.ISupportInitialize) (this.Nup_DownloadThreads)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox Tb_Cookies;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button SelectSaveDir;
        private System.Windows.Forms.Label Lb_SavePath;
        private System.Windows.Forms.Button SaveConfig;
        private System.Windows.Forms.NumericUpDown Nup_DownloadThreads;
    }
}