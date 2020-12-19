
namespace WSDataTransferSuper
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.txtTo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSeq = new System.Windows.Forms.TextBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnTransfer = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cD1 = new System.Windows.Forms.CheckBox();
            this.cG = new System.Windows.Forms.CheckBox();
            this.cE = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cX8S = new System.Windows.Forms.CheckBox();
            this.cX8J = new System.Windows.Forms.CheckBox();
            this.cX81 = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtStatusCode = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "From";
            // 
            // txtFrom
            // 
            this.txtFrom.Location = new System.Drawing.Point(62, 6);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(984, 25);
            this.txtFrom.TabIndex = 1;
            this.txtFrom.Text = "Data Source=10.16.112.31;Initial Catalog=PiscesMPAB;User ID=pisces_user;Password=" +
    "sahasnopassword";
            // 
            // txtTo
            // 
            this.txtTo.Location = new System.Drawing.Point(62, 46);
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(984, 25);
            this.txtTo.TabIndex = 3;
            this.txtTo.Text = "Data Source=10.16.112.31;Initial Catalog=Pisces_LV;User ID=pisces_user;Password=s" +
    "ahasnopassword";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "To";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "SEQUENCE";
            // 
            // txtSeq
            // 
            this.txtSeq.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtSeq.Enabled = false;
            this.txtSeq.Location = new System.Drawing.Point(12, 145);
            this.txtSeq.Multiline = true;
            this.txtSeq.Name = "txtSeq";
            this.txtSeq.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSeq.Size = new System.Drawing.Size(157, 559);
            this.txtSeq.TabIndex = 5;
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(175, 145);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.RowTemplate.Height = 27;
            this.dataGridView.Size = new System.Drawing.Size(960, 559);
            this.dataGridView.TabIndex = 6;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(89, 77);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(117, 62);
            this.btnLoad.TabIndex = 7;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnTransfer
            // 
            this.btnTransfer.Location = new System.Drawing.Point(226, 76);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.Size = new System.Drawing.Size(104, 63);
            this.btnTransfer.TabIndex = 8;
            this.btnTransfer.Text = "Transfer";
            this.btnTransfer.UseVisualStyleBackColor = true;
            this.btnTransfer.Click += new System.EventHandler(this.btnTransfer_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1052, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 65);
            this.button1.TabIndex = 9;
            this.button1.Text = "DB Connect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cD1);
            this.groupBox1.Controls.Add(this.cG);
            this.groupBox1.Controls.Add(this.cE);
            this.groupBox1.Location = new System.Drawing.Point(348, 76);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 63);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Infor Point";
            // 
            // cD1
            // 
            this.cD1.AutoSize = true;
            this.cD1.Location = new System.Drawing.Point(10, 23);
            this.cD1.Name = "cD1";
            this.cD1.Size = new System.Drawing.Size(45, 19);
            this.cD1.TabIndex = 2;
            this.cD1.Text = "D1";
            this.cD1.UseVisualStyleBackColor = true;
            // 
            // cG
            // 
            this.cG.AutoSize = true;
            this.cG.Checked = true;
            this.cG.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cG.Location = new System.Drawing.Point(117, 24);
            this.cG.Name = "cG";
            this.cG.Size = new System.Drawing.Size(37, 19);
            this.cG.TabIndex = 1;
            this.cG.Text = "G";
            this.cG.UseVisualStyleBackColor = true;
            // 
            // cE
            // 
            this.cE.AutoSize = true;
            this.cE.Location = new System.Drawing.Point(74, 23);
            this.cE.Name = "cE";
            this.cE.Size = new System.Drawing.Size(37, 19);
            this.cE.TabIndex = 0;
            this.cE.Text = "E";
            this.cE.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cX8S);
            this.groupBox2.Controls.Add(this.cX8J);
            this.groupBox2.Controls.Add(this.cX81);
            this.groupBox2.Location = new System.Drawing.Point(554, 76);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 64);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Type";
            // 
            // cX8S
            // 
            this.cX8S.AutoSize = true;
            this.cX8S.Checked = true;
            this.cX8S.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cX8S.Location = new System.Drawing.Point(124, 24);
            this.cX8S.Name = "cX8S";
            this.cX8S.Size = new System.Drawing.Size(53, 19);
            this.cX8S.TabIndex = 2;
            this.cX8S.Text = "X8S";
            this.cX8S.UseVisualStyleBackColor = true;
            // 
            // cX8J
            // 
            this.cX8J.AutoSize = true;
            this.cX8J.Checked = true;
            this.cX8J.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cX8J.Location = new System.Drawing.Point(65, 24);
            this.cX8J.Name = "cX8J";
            this.cX8J.Size = new System.Drawing.Size(53, 19);
            this.cX8J.TabIndex = 1;
            this.cX8J.Text = "X8J";
            this.cX8J.UseVisualStyleBackColor = true;
            // 
            // cX81
            // 
            this.cX81.AutoSize = true;
            this.cX81.Checked = true;
            this.cX81.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cX81.Location = new System.Drawing.Point(6, 24);
            this.cX81.Name = "cX81";
            this.cX81.Size = new System.Drawing.Size(53, 19);
            this.cX81.TabIndex = 0;
            this.cX81.Text = "X81";
            this.cX81.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(778, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 15);
            this.label4.TabIndex = 11;
            this.label4.Text = "Status Code";
            // 
            // txtStatusCode
            // 
            this.txtStatusCode.Location = new System.Drawing.Point(879, 97);
            this.txtStatusCode.Name = "txtStatusCode";
            this.txtStatusCode.Size = new System.Drawing.Size(100, 25);
            this.txtStatusCode.TabIndex = 12;
            this.txtStatusCode.Text = "0";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1147, 716);
            this.Controls.Add(this.txtStatusCode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnTransfer);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.txtSeq);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtFrom);
            this.Controls.Add(this.label1);
            this.Name = "FrmMain";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFrom;
        private System.Windows.Forms.TextBox txtTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSeq;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnTransfer;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cD1;
        private System.Windows.Forms.CheckBox cG;
        private System.Windows.Forms.CheckBox cE;
        private System.Windows.Forms.CheckBox cX8S;
        private System.Windows.Forms.CheckBox cX8J;
        private System.Windows.Forms.CheckBox cX81;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtStatusCode;
    }
}

