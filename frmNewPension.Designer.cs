namespace WinAtt
{
    partial class frmNewPension
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
            this.button1 = new System.Windows.Forms.Button();
            this.panBG = new System.Windows.Forms.Panel();
            this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.dtpDateOfBirth = new System.Windows.Forms.DateTimePicker();
            this.cmbWorkUnit = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblNewPensionID = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.cmbSex = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtIdentityCard = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtLinkMan = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panBG.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(424, 240);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(192, 49);
            this.button1.TabIndex = 0;
            this.button1.Text = "录入指纹";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panBG
            // 
            this.panBG.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panBG.AutoSize = true;
            this.panBG.Controls.Add(this.label1);
            this.panBG.Controls.Add(this.dtpStartTime);
            this.panBG.Controls.Add(this.button1);
            this.panBG.Controls.Add(this.dtpDateOfBirth);
            this.panBG.Controls.Add(this.cmbWorkUnit);
            this.panBG.Controls.Add(this.groupBox1);
            this.panBG.Controls.Add(this.label3);
            this.panBG.Controls.Add(this.label12);
            this.panBG.Controls.Add(this.label15);
            this.panBG.Controls.Add(this.cmbSex);
            this.panBG.Controls.Add(this.label5);
            this.panBG.Controls.Add(this.txtName);
            this.panBG.Controls.Add(this.txtPhone);
            this.panBG.Controls.Add(this.txtIdentityCard);
            this.panBG.Controls.Add(this.label6);
            this.panBG.Controls.Add(this.label2);
            this.panBG.Controls.Add(this.label7);
            this.panBG.Controls.Add(this.txtLinkMan);
            this.panBG.Controls.Add(this.label10);
            this.panBG.Location = new System.Drawing.Point(12, 12);
            this.panBG.Name = "panBG";
            this.panBG.Size = new System.Drawing.Size(1019, 314);
            this.panBG.TabIndex = 40;
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.Location = new System.Drawing.Point(424, 183);
            this.dtpStartTime.MaxDate = new System.DateTime(2020, 12, 31, 0, 0, 0, 0);
            this.dtpStartTime.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.Size = new System.Drawing.Size(192, 21);
            this.dtpStartTime.TabIndex = 68;
            // 
            // dtpDateOfBirth
            // 
            this.dtpDateOfBirth.Location = new System.Drawing.Point(772, 108);
            this.dtpDateOfBirth.MaxDate = new System.DateTime(2020, 12, 31, 0, 0, 0, 0);
            this.dtpDateOfBirth.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtpDateOfBirth.Name = "dtpDateOfBirth";
            this.dtpDateOfBirth.Size = new System.Drawing.Size(192, 21);
            this.dtpDateOfBirth.TabIndex = 50;
            // 
            // cmbWorkUnit
            // 
            this.cmbWorkUnit.FormattingEnabled = true;
            this.cmbWorkUnit.Location = new System.Drawing.Point(103, 142);
            this.cmbWorkUnit.Name = "cmbWorkUnit";
            this.cmbWorkUnit.Size = new System.Drawing.Size(192, 20);
            this.cmbWorkUnit.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblNewPensionID);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Location = new System.Drawing.Point(16, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(992, 40);
            this.groupBox1.TabIndex = 49;
            this.groupBox1.TabStop = false;
            // 
            // lblNewPensionID
            // 
            this.lblNewPensionID.AutoSize = true;
            this.lblNewPensionID.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNewPensionID.Location = new System.Drawing.Point(107, 17);
            this.lblNewPensionID.Name = "lblNewPensionID";
            this.lblNewPensionID.Size = new System.Drawing.Size(0, 14);
            this.lblNewPensionID.TabIndex = 12;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(18, 17);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 11;
            this.label14.Text = "指纹录入";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 47;
            this.label3.Text = "工作单位:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(340, 183);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 12);
            this.label12.TabIndex = 43;
            this.label12.Text = "起始时间:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(62, 112);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(35, 12);
            this.label15.TabIndex = 10;
            this.label15.Text = "姓名:";
            // 
            // cmbSex
            // 
            this.cmbSex.FormattingEnabled = true;
            this.cmbSex.Items.AddRange(new object[] {
            "男",
            "女"});
            this.cmbSex.Location = new System.Drawing.Point(424, 108);
            this.cmbSex.Name = "cmbSex";
            this.cmbSex.Size = new System.Drawing.Size(192, 20);
            this.cmbSex.TabIndex = 1;
            this.cmbSex.Text = "男";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(364, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "性别:";
            // 
            // txtName
            // 
            this.txtName.AcceptsReturn = true;
            this.txtName.Location = new System.Drawing.Point(103, 108);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(192, 21);
            this.txtName.TabIndex = 0;
            // 
            // txtPhone
            // 
            this.txtPhone.AcceptsReturn = true;
            this.txtPhone.Location = new System.Drawing.Point(103, 180);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(192, 21);
            this.txtPhone.TabIndex = 9;
            // 
            // txtIdentityCard
            // 
            this.txtIdentityCard.AcceptsReturn = true;
            this.txtIdentityCard.Location = new System.Drawing.Point(424, 147);
            this.txtIdentityCard.Name = "txtIdentityCard";
            this.txtIdentityCard.Size = new System.Drawing.Size(192, 21);
            this.txtIdentityCard.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(340, 150);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "身份证号:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 180);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 25;
            this.label2.Text = "联系电话:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(707, 116);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 17;
            this.label7.Text = "出生年月:";
            // 
            // txtLinkMan
            // 
            this.txtLinkMan.AcceptsReturn = true;
            this.txtLinkMan.Location = new System.Drawing.Point(772, 147);
            this.txtLinkMan.Name = "txtLinkMan";
            this.txtLinkMan.Size = new System.Drawing.Size(192, 21);
            this.txtLinkMan.TabIndex = 8;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(713, 150);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 23;
            this.label10.Text = "邮　箱：";
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(12, 334);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(1019, 304);
            this.listBox1.TabIndex = 69;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(451, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 37);
            this.label1.TabIndex = 69;
            this.label1.Text = "指纹录入";
            // 
            // frmNewPension
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1043, 638);
            this.Controls.Add(this.panBG);
            this.Controls.Add(this.listBox1);
            this.Name = "frmNewPension";
            this.Text = "frmNewPension";
            this.Load += new System.EventHandler(this.frmNewPension_Load);
            this.panBG.ResumeLayout(false);
            this.panBG.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panBG;
        private System.Windows.Forms.DateTimePicker dtpStartTime;
        private System.Windows.Forms.DateTimePicker dtpDateOfBirth;
        private System.Windows.Forms.ComboBox cmbWorkUnit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblNewPensionID;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cmbSex;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtIdentityCard;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtLinkMan;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label1;
    }
}