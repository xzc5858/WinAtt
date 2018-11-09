namespace WinAtt
{
    partial class main
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("指纹记录管理");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("人员信息管理");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("考勤管理系统", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tv = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 495);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1095, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tv
            // 
            this.tv.Dock = System.Windows.Forms.DockStyle.Left;
            this.tv.Location = new System.Drawing.Point(0, 0);
            this.tv.Name = "tv";
            treeNode1.Name = "trOutline";
            treeNode1.Text = "指纹记录管理";
            treeNode2.Name = "trUserOutLine";
            treeNode2.Text = "人员信息管理";
            treeNode3.Name = "trMain";
            treeNode3.Text = "考勤管理系统";
            this.tv.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3});
            this.tv.Size = new System.Drawing.Size(199, 495);
            this.tv.TabIndex = 8;
            this.tv.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tv_NodeMouseClick);
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1095, 517);
            this.Controls.Add(this.tv);
            this.Controls.Add(this.statusStrip1);
            this.IsMdiContainer = true;
            this.Name = "main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "考勤管理系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TreeView tv;
    }
}

