namespace WindowsFormsApp1
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.GROUP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SHIPPING_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COUNT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SEQ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FROM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DELT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GROUP,
            this.SHIPPING_CODE,
            this.STATUS,
            this.COUNT,
            this.SEQ,
            this.QTY,
            this.FROM,
            this.TO,
            this.DELT});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(867, 498);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.Text = "dataGridView1";
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            // 
            // timer1
            // 
            this.timer1.Interval = 300000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // GROUP
            // 
            this.GROUP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.GROUP.DataPropertyName = "GROUP";
            this.GROUP.HeaderText = "GROUP";
            this.GROUP.MinimumWidth = 6;
            this.GROUP.Name = "GROUP";
            this.GROUP.ReadOnly = true;
            this.GROUP.Width = 91;
            // 
            // SHIPPING_CODE
            // 
            this.SHIPPING_CODE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SHIPPING_CODE.DataPropertyName = "SHIPPING_CODE";
            this.SHIPPING_CODE.HeaderText = "SHIPPING_CODE";
            this.SHIPPING_CODE.MinimumWidth = 6;
            this.SHIPPING_CODE.Name = "SHIPPING_CODE";
            this.SHIPPING_CODE.ReadOnly = true;
            this.SHIPPING_CODE.Width = 156;
            // 
            // STATUS
            // 
            this.STATUS.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.STATUS.DataPropertyName = "STATUS";
            this.STATUS.HeaderText = "STATUS";
            this.STATUS.MinimumWidth = 6;
            this.STATUS.Name = "STATUS";
            this.STATUS.ReadOnly = true;
            this.STATUS.Width = 96;
            // 
            // COUNT
            // 
            this.COUNT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.COUNT.DataPropertyName = "COUNT";
            this.COUNT.HeaderText = "COUNT";
            this.COUNT.MinimumWidth = 6;
            this.COUNT.Name = "COUNT";
            this.COUNT.ReadOnly = true;
            this.COUNT.Width = 92;
            // 
            // SEQ
            // 
            this.SEQ.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SEQ.DataPropertyName = "SEQ";
            this.SEQ.HeaderText = "SEQ";
            this.SEQ.MinimumWidth = 6;
            this.SEQ.Name = "SEQ";
            this.SEQ.ReadOnly = true;
            this.SEQ.Width = 67;
            // 
            // QTY
            // 
            this.QTY.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.QTY.DataPropertyName = "QTY";
            this.QTY.HeaderText = "QTY";
            this.QTY.MinimumWidth = 6;
            this.QTY.Name = "QTY";
            this.QTY.ReadOnly = true;
            this.QTY.Width = 68;
            // 
            // FROM
            // 
            this.FROM.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.FROM.DataPropertyName = "FROM";
            this.FROM.HeaderText = "FROM";
            this.FROM.MinimumWidth = 6;
            this.FROM.Name = "FROM";
            this.FROM.ReadOnly = true;
            this.FROM.Width = 83;
            // 
            // TO
            // 
            this.TO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TO.DataPropertyName = "TO";
            this.TO.HeaderText = "TO";
            this.TO.MinimumWidth = 6;
            this.TO.Name = "TO";
            this.TO.ReadOnly = true;
            this.TO.Width = 59;
            // 
            // DELT
            // 
            this.DELT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DELT.DataPropertyName = "DELT";
            this.DELT.HeaderText = "DELT";
            this.DELT.MinimumWidth = 6;
            this.DELT.Name = "DELT";
            this.DELT.ReadOnly = true;
            this.DELT.Width = 74;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(867, 498);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridViewTextBoxColumn GROUP;
        private System.Windows.Forms.DataGridViewTextBoxColumn SHIPPING_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn STATUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn COUNT;
        private System.Windows.Forms.DataGridViewTextBoxColumn SEQ;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn FROM;
        private System.Windows.Forms.DataGridViewTextBoxColumn TO;
        private System.Windows.Forms.DataGridViewTextBoxColumn DELT;
    }
}

