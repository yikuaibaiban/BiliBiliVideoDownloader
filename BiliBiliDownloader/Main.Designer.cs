
using BiliBiliDownloader.Controls;

namespace BiliBiliDownloader
{
    partial class Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.SystemConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Lv_TaskList = new BiliBiliDownloader.Controls.ListViewNF();
            this.Cid = new System.Windows.Forms.ColumnHeader();
            this.Title = new System.Windows.Forms.ColumnHeader();
            this.Bvid = new System.Windows.Forms.ColumnHeader();
            this.Page = new System.Windows.Forms.ColumnHeader();
            this.Part = new System.Windows.Forms.ColumnHeader();
            this.Progress = new System.Windows.Forms.ColumnHeader();
            this.url = new System.Windows.Forms.TextBox();
            this.Add = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.toolStripMenuItem1, this.SystemConfig, this.关于ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(44, 21);
            this.toolStripMenuItem1.Text = "文件";
            // 
            // SystemConfig
            // 
            this.SystemConfig.Name = "SystemConfig";
            this.SystemConfig.Size = new System.Drawing.Size(44, 21);
            this.SystemConfig.Text = "设置";
            this.SystemConfig.Click += new System.EventHandler(this.SystemConfig_Click);
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.关于ToolStripMenuItem.Text = "关于";
            // 
            // Lv_TaskList
            // 
            this.Lv_TaskList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {this.Cid, this.Title, this.Bvid, this.Page, this.Part, this.Progress});
            this.Lv_TaskList.FullRowSelect = true;
            this.Lv_TaskList.GridLines = true;
            this.Lv_TaskList.HideSelection = false;
            this.Lv_TaskList.Location = new System.Drawing.Point(12, 85);
            this.Lv_TaskList.Name = "Lv_TaskList";
            this.Lv_TaskList.Size = new System.Drawing.Size(776, 353);
            this.Lv_TaskList.TabIndex = 1;
            this.Lv_TaskList.UseCompatibleStateImageBehavior = false;
            this.Lv_TaskList.View = System.Windows.Forms.View.Details;
            // 
            // Cid
            // 
            this.Cid.Text = "Cid";
            this.Cid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Cid.Width = 100;
            // 
            // Title
            // 
            this.Title.Text = "标题";
            this.Title.Width = 200;
            // 
            // Bvid
            // 
            this.Bvid.Text = "Bvid";
            this.Bvid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Bvid.Width = 100;
            // 
            // Page
            // 
            this.Page.Text = "序号";
            this.Page.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Part
            // 
            this.Part.Text = "章节";
            this.Part.Width = 200;
            // 
            // Progress
            // 
            this.Progress.Text = "进度";
            this.Progress.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Progress.Width = 80;
            // 
            // url
            // 
            this.url.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (134)));
            this.url.Location = new System.Drawing.Point(12, 37);
            this.url.Name = "url";
            this.url.Size = new System.Drawing.Size(681, 31);
            this.url.TabIndex = 2;
            // 
            // Add
            // 
            this.Add.AutoSize = true;
            this.Add.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (134)));
            this.Add.Location = new System.Drawing.Point(699, 38);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(89, 29);
            this.Add.TabIndex = 3;
            this.Add.Text = "添加";
            this.Add.UseVisualStyleBackColor = true;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Add);
            this.Controls.Add(this.url);
            this.Controls.Add(this.Lv_TaskList);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BiliBili下载器";
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ColumnHeader Bvid;
        private System.Windows.Forms.ColumnHeader Cid;
        private System.Windows.Forms.ColumnHeader Page;
        private System.Windows.Forms.ColumnHeader Part;
        private System.Windows.Forms.ColumnHeader Progress;
        private System.Windows.Forms.ColumnHeader Title;

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem SystemConfig;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private ListViewNF Lv_TaskList;
        private System.Windows.Forms.TextBox url;
        private System.Windows.Forms.Button Add;
    }
}

