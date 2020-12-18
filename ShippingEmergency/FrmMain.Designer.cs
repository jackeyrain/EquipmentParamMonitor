
namespace ShippingEmergency
{
    partial class FrmMain
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnRDExec = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnLDExec = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnConsoleExec = new System.Windows.Forms.Button();
            this.btnConsole = new System.Windows.Forms.Button();
            this.btnIPExec = new System.Windows.Forms.Button();
            this.btnIP = new System.Windows.Forms.Button();
            this.txtTo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataResult = new System.Windows.Forms.DataGridView();
            this.Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Code = new System.Windows.Forms.DataGridViewLinkColumn();
            this.SHIPPING_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FromSeq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ToSeq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VehicleCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SEQ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CREATE_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALID_FLAG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PART_SHIPPING_FID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataResult)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.richTextBox1);
            this.groupBox1.Controls.Add(this.btnRDExec);
            this.groupBox1.Controls.Add(this.btnRight);
            this.groupBox1.Controls.Add(this.btnLDExec);
            this.groupBox1.Controls.Add(this.btnLeft);
            this.groupBox1.Controls.Add(this.btnConsoleExec);
            this.groupBox1.Controls.Add(this.btnConsole);
            this.groupBox1.Controls.Add(this.btnIPExec);
            this.groupBox1.Controls.Add(this.btnIP);
            this.groupBox1.Controls.Add(this.txtTo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtFrom);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1341, 158);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Condition";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(664, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(671, 140);
            this.richTextBox1.TabIndex = 8;
            this.richTextBox1.Text = "";
            // 
            // btnRDExec
            // 
            this.btnRDExec.Enabled = false;
            this.btnRDExec.Location = new System.Drawing.Point(486, 109);
            this.btnRDExec.Name = "btnRDExec";
            this.btnRDExec.Size = new System.Drawing.Size(172, 43);
            this.btnRDExec.TabIndex = 7;
            this.btnRDExec.Text = "Right Door Shipping Exec";
            this.btnRDExec.UseVisualStyleBackColor = true;
            this.btnRDExec.Click += new System.EventHandler(this.btnRDExec_Click);
            // 
            // btnRight
            // 
            this.btnRight.Location = new System.Drawing.Point(486, 61);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(172, 43);
            this.btnRight.TabIndex = 7;
            this.btnRight.Text = "Right Door Shipping";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // btnLDExec
            // 
            this.btnLDExec.Enabled = false;
            this.btnLDExec.Location = new System.Drawing.Point(319, 109);
            this.btnLDExec.Name = "btnLDExec";
            this.btnLDExec.Size = new System.Drawing.Size(161, 43);
            this.btnLDExec.TabIndex = 6;
            this.btnLDExec.Text = "Left Door Shipping Exec";
            this.btnLDExec.UseVisualStyleBackColor = true;
            this.btnLDExec.Click += new System.EventHandler(this.btnLDExec_Click);
            // 
            // btnLeft
            // 
            this.btnLeft.Location = new System.Drawing.Point(319, 61);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(161, 43);
            this.btnLeft.TabIndex = 6;
            this.btnLeft.Text = "Left Door Shipping";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnConsoleExec
            // 
            this.btnConsoleExec.Enabled = false;
            this.btnConsoleExec.Location = new System.Drawing.Point(152, 109);
            this.btnConsoleExec.Name = "btnConsoleExec";
            this.btnConsoleExec.Size = new System.Drawing.Size(161, 43);
            this.btnConsoleExec.TabIndex = 5;
            this.btnConsoleExec.Text = "Console Shipping Exec";
            this.btnConsoleExec.UseVisualStyleBackColor = true;
            this.btnConsoleExec.Click += new System.EventHandler(this.btnConsoleExec_Click);
            // 
            // btnConsole
            // 
            this.btnConsole.Location = new System.Drawing.Point(152, 61);
            this.btnConsole.Name = "btnConsole";
            this.btnConsole.Size = new System.Drawing.Size(161, 43);
            this.btnConsole.TabIndex = 5;
            this.btnConsole.Text = "Console Shipping";
            this.btnConsole.UseVisualStyleBackColor = true;
            this.btnConsole.Click += new System.EventHandler(this.btnConsole_Click);
            // 
            // btnIPExec
            // 
            this.btnIPExec.Enabled = false;
            this.btnIPExec.Location = new System.Drawing.Point(19, 109);
            this.btnIPExec.Name = "btnIPExec";
            this.btnIPExec.Size = new System.Drawing.Size(127, 43);
            this.btnIPExec.TabIndex = 4;
            this.btnIPExec.Text = "IP Shipping Exec";
            this.btnIPExec.UseVisualStyleBackColor = true;
            this.btnIPExec.Click += new System.EventHandler(this.btnIPExec_Click);
            // 
            // btnIP
            // 
            this.btnIP.Location = new System.Drawing.Point(19, 61);
            this.btnIP.Name = "btnIP";
            this.btnIP.Size = new System.Drawing.Size(127, 43);
            this.btnIP.TabIndex = 4;
            this.btnIP.Text = "IP Shipping";
            this.btnIP.UseVisualStyleBackColor = true;
            this.btnIP.Click += new System.EventHandler(this.btnIP_Click);
            // 
            // txtTo
            // 
            this.txtTo.Location = new System.Drawing.Point(435, 30);
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(223, 28);
            this.txtTo.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(383, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "To";
            // 
            // txtFrom
            // 
            this.txtFrom.Location = new System.Drawing.Point(147, 30);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(213, 28);
            this.txtFrom.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sequence From";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dataResult);
            this.groupBox2.Location = new System.Drawing.Point(12, 180);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1341, 508);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Result";
            // 
            // dataResult
            // 
            this.dataResult.AllowUserToAddRows = false;
            this.dataResult.AllowUserToDeleteRows = false;
            this.dataResult.BackgroundColor = System.Drawing.Color.White;
            this.dataResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Select,
            this.ID,
            this.Code,
            this.SHIPPING_CODE,
            this.FromSeq,
            this.ToSeq,
            this.VehicleCount,
            this.ItemCount,
            this.SEQ,
            this.CREATE_DATE,
            this.STATUS,
            this.VALID_FLAG,
            this.PART_SHIPPING_FID});
            this.dataResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataResult.Location = new System.Drawing.Point(3, 24);
            this.dataResult.Name = "dataResult";
            this.dataResult.RowHeadersWidth = 51;
            this.dataResult.RowTemplate.Height = 27;
            this.dataResult.Size = new System.Drawing.Size(1335, 481);
            this.dataResult.TabIndex = 0;
            this.dataResult.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataResult_CellContentClick);
            this.dataResult.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataResult_DataBindingComplete);
            // 
            // Select
            // 
            this.Select.HeaderText = "";
            this.Select.MinimumWidth = 6;
            this.Select.Name = "Select";
            this.Select.Width = 125;
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.MinimumWidth = 6;
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Width = 125;
            // 
            // Code
            // 
            this.Code.DataPropertyName = "SHIPPING_CODE";
            this.Code.HeaderText = "Code";
            this.Code.MinimumWidth = 6;
            this.Code.Name = "Code";
            this.Code.Width = 125;
            // 
            // SHIPPING_CODE
            // 
            this.SHIPPING_CODE.DataPropertyName = "SHIPPING_CODE";
            this.SHIPPING_CODE.HeaderText = "SHIPPING_CODE";
            this.SHIPPING_CODE.MinimumWidth = 6;
            this.SHIPPING_CODE.Name = "SHIPPING_CODE";
            this.SHIPPING_CODE.ReadOnly = true;
            this.SHIPPING_CODE.Visible = false;
            this.SHIPPING_CODE.Width = 125;
            // 
            // FromSeq
            // 
            this.FromSeq.DataPropertyName = "FromSeq";
            this.FromSeq.HeaderText = "FromSeq";
            this.FromSeq.MinimumWidth = 6;
            this.FromSeq.Name = "FromSeq";
            this.FromSeq.ReadOnly = true;
            this.FromSeq.Width = 125;
            // 
            // ToSeq
            // 
            this.ToSeq.DataPropertyName = "ToSeq";
            this.ToSeq.HeaderText = "ToSeq";
            this.ToSeq.MinimumWidth = 6;
            this.ToSeq.Name = "ToSeq";
            this.ToSeq.ReadOnly = true;
            this.ToSeq.Width = 125;
            // 
            // VehicleCount
            // 
            this.VehicleCount.DataPropertyName = "VehicleCount";
            this.VehicleCount.HeaderText = "VehicleCount";
            this.VehicleCount.MinimumWidth = 6;
            this.VehicleCount.Name = "VehicleCount";
            this.VehicleCount.ReadOnly = true;
            this.VehicleCount.Width = 125;
            // 
            // ItemCount
            // 
            this.ItemCount.DataPropertyName = "ItemCount";
            this.ItemCount.HeaderText = "ItemCount";
            this.ItemCount.MinimumWidth = 6;
            this.ItemCount.Name = "ItemCount";
            this.ItemCount.ReadOnly = true;
            this.ItemCount.Width = 125;
            // 
            // SEQ
            // 
            this.SEQ.DataPropertyName = "SEQ";
            this.SEQ.HeaderText = "SEQ";
            this.SEQ.MinimumWidth = 6;
            this.SEQ.Name = "SEQ";
            this.SEQ.ReadOnly = true;
            this.SEQ.Width = 125;
            // 
            // CREATE_DATE
            // 
            this.CREATE_DATE.DataPropertyName = "CREATE_DATE";
            this.CREATE_DATE.HeaderText = "CREATE_DATE";
            this.CREATE_DATE.MinimumWidth = 6;
            this.CREATE_DATE.Name = "CREATE_DATE";
            this.CREATE_DATE.ReadOnly = true;
            this.CREATE_DATE.Width = 125;
            // 
            // STATUS
            // 
            this.STATUS.DataPropertyName = "STATUS";
            this.STATUS.HeaderText = "STATUS";
            this.STATUS.MinimumWidth = 6;
            this.STATUS.Name = "STATUS";
            this.STATUS.ReadOnly = true;
            this.STATUS.Width = 125;
            // 
            // VALID_FLAG
            // 
            this.VALID_FLAG.DataPropertyName = "VALID_FLAG";
            this.VALID_FLAG.HeaderText = "VALID_FLAG";
            this.VALID_FLAG.MinimumWidth = 6;
            this.VALID_FLAG.Name = "VALID_FLAG";
            this.VALID_FLAG.ReadOnly = true;
            this.VALID_FLAG.Width = 125;
            // 
            // PART_SHIPPING_FID
            // 
            this.PART_SHIPPING_FID.DataPropertyName = "PART_SHIPPING_FID";
            this.PART_SHIPPING_FID.HeaderText = "PART_SHIPPING_FID";
            this.PART_SHIPPING_FID.MinimumWidth = 6;
            this.PART_SHIPPING_FID.Name = "PART_SHIPPING_FID";
            this.PART_SHIPPING_FID.ReadOnly = true;
            this.PART_SHIPPING_FID.Width = 125;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1365, 700);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmMain";
            this.Text = "Shipping Emergency";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFrom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnConsole;
        private System.Windows.Forms.Button btnIP;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.DataGridView dataResult;
        private System.Windows.Forms.Button btnRDExec;
        private System.Windows.Forms.Button btnLDExec;
        private System.Windows.Forms.Button btnConsoleExec;
        private System.Windows.Forms.Button btnIPExec;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Select;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewLinkColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn SHIPPING_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn FromSeq;
        private System.Windows.Forms.DataGridViewTextBoxColumn ToSeq;
        private System.Windows.Forms.DataGridViewTextBoxColumn VehicleCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn SEQ;
        private System.Windows.Forms.DataGridViewTextBoxColumn CREATE_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn STATUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALID_FLAG;
        private System.Windows.Forms.DataGridViewTextBoxColumn PART_SHIPPING_FID;
    }
}

