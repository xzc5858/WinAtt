namespace WinAtt
{
    partial class frmOutline
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
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.machineAliasDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.connectTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iPDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.portDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.baudrateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.machineNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.enterOrOutDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dmachinesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.attDataSet = new WinAtt.attDataSet();
            this.d_machinesTableAdapter = new WinAtt.attDataSetTableAdapters.d_machinesTableAdapter();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dmachinesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.attDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.machineAliasDataGridViewTextBoxColumn,
            this.connectTypeDataGridViewTextBoxColumn,
            this.iPDataGridViewTextBoxColumn,
            this.portDataGridViewTextBoxColumn,
            this.baudrateDataGridViewTextBoxColumn,
            this.machineNumberDataGridViewTextBoxColumn,
            this.enterOrOutDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.dmachinesBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.RowTemplate.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1056, 200);
            this.dataGridView1.TabIndex = 0;
            // 
            // machineAliasDataGridViewTextBoxColumn
            // 
            this.machineAliasDataGridViewTextBoxColumn.DataPropertyName = "MachineAlias";
            this.machineAliasDataGridViewTextBoxColumn.HeaderText = "机器编码";
            this.machineAliasDataGridViewTextBoxColumn.Name = "machineAliasDataGridViewTextBoxColumn";
            this.machineAliasDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // connectTypeDataGridViewTextBoxColumn
            // 
            this.connectTypeDataGridViewTextBoxColumn.DataPropertyName = "ConnectType";
            this.connectTypeDataGridViewTextBoxColumn.HeaderText = "状态";
            this.connectTypeDataGridViewTextBoxColumn.Name = "connectTypeDataGridViewTextBoxColumn";
            this.connectTypeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iPDataGridViewTextBoxColumn
            // 
            this.iPDataGridViewTextBoxColumn.DataPropertyName = "IP";
            this.iPDataGridViewTextBoxColumn.HeaderText = "IP地址";
            this.iPDataGridViewTextBoxColumn.Name = "iPDataGridViewTextBoxColumn";
            this.iPDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // portDataGridViewTextBoxColumn
            // 
            this.portDataGridViewTextBoxColumn.DataPropertyName = "Port";
            this.portDataGridViewTextBoxColumn.HeaderText = "端口";
            this.portDataGridViewTextBoxColumn.Name = "portDataGridViewTextBoxColumn";
            this.portDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // baudrateDataGridViewTextBoxColumn
            // 
            this.baudrateDataGridViewTextBoxColumn.DataPropertyName = "Baudrate";
            this.baudrateDataGridViewTextBoxColumn.HeaderText = "波特率";
            this.baudrateDataGridViewTextBoxColumn.Name = "baudrateDataGridViewTextBoxColumn";
            this.baudrateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // machineNumberDataGridViewTextBoxColumn
            // 
            this.machineNumberDataGridViewTextBoxColumn.DataPropertyName = "MachineNumber";
            this.machineNumberDataGridViewTextBoxColumn.HeaderText = "机器码";
            this.machineNumberDataGridViewTextBoxColumn.Name = "machineNumberDataGridViewTextBoxColumn";
            this.machineNumberDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // enterOrOutDataGridViewTextBoxColumn
            // 
            this.enterOrOutDataGridViewTextBoxColumn.DataPropertyName = "EnterOrOut";
            this.enterOrOutDataGridViewTextBoxColumn.HeaderText = "进(出)";
            this.enterOrOutDataGridViewTextBoxColumn.Name = "enterOrOutDataGridViewTextBoxColumn";
            this.enterOrOutDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dmachinesBindingSource
            // 
            this.dmachinesBindingSource.DataMember = "d_machines";
            this.dmachinesBindingSource.DataSource = this.attDataSet;
            // 
            // attDataSet
            // 
            this.attDataSet.DataSetName = "attDataSet";
            this.attDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // d_machinesTableAdapter
            // 
            this.d_machinesTableAdapter.ClearBeforeFill = true;
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(0, 205);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(1056, 172);
            this.listBox1.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(468, 385);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(189, 59);
            this.button2.TabIndex = 6;
            this.button2.Text = "前循环下载";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(858, 385);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(189, 59);
            this.button1.TabIndex = 5;
            this.button1.Text = "指定下载";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(663, 385);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(189, 59);
            this.button3.TabIndex = 7;
            this.button3.Text = "后循环下载";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // frmOutline
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1056, 454);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.dataGridView1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOutline";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "数据操作";
            this.Load += new System.EventHandler(this.frmOutline_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dmachinesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.attDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private attDataSet attDataSet;
        private System.Windows.Forms.BindingSource dmachinesBindingSource;
        private attDataSetTableAdapters.d_machinesTableAdapter d_machinesTableAdapter;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridViewTextBoxColumn machineAliasDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn connectTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iPDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn portDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn baudrateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn machineNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn enterOrOutDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button button3;
    }
}