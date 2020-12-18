
namespace ShippingEmergency
{
    partial class FrmDetail
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CUST_INFO_SEQ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LOGIC_SEQ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LZ_VIN_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CUST_PART_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PART_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PART_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PLAN_QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CUST_ORDER_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CUST_SORT_INFO_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SOURCE_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BARCODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALID_FLAG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.CUST_INFO_SEQ,
            this.LOGIC_SEQ,
            this.LZ_VIN_CODE,
            this.CUST_PART_NO,
            this.PART_NO,
            this.PART_NAME,
            this.PLAN_QTY,
            this.CUST_ORDER_CODE,
            this.CUST_SORT_INFO_ID,
            this.SOURCE_ID,
            this.BARCODE,
            this.STATUS,
            this.VALID_FLAG});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(1274, 648);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
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
            // CUST_INFO_SEQ
            // 
            this.CUST_INFO_SEQ.DataPropertyName = "CUST_INFO_SEQ";
            this.CUST_INFO_SEQ.HeaderText = "CUST_INFO_SEQ";
            this.CUST_INFO_SEQ.MinimumWidth = 6;
            this.CUST_INFO_SEQ.Name = "CUST_INFO_SEQ";
            this.CUST_INFO_SEQ.ReadOnly = true;
            this.CUST_INFO_SEQ.Width = 125;
            // 
            // LOGIC_SEQ
            // 
            this.LOGIC_SEQ.DataPropertyName = "LOGIC_SEQ";
            this.LOGIC_SEQ.HeaderText = "LOGIC_SEQ";
            this.LOGIC_SEQ.MinimumWidth = 6;
            this.LOGIC_SEQ.Name = "LOGIC_SEQ";
            this.LOGIC_SEQ.ReadOnly = true;
            this.LOGIC_SEQ.Width = 125;
            // 
            // LZ_VIN_CODE
            // 
            this.LZ_VIN_CODE.DataPropertyName = "LZ_VIN_CODE";
            this.LZ_VIN_CODE.HeaderText = "LZ_VIN_CODE";
            this.LZ_VIN_CODE.MinimumWidth = 6;
            this.LZ_VIN_CODE.Name = "LZ_VIN_CODE";
            this.LZ_VIN_CODE.ReadOnly = true;
            this.LZ_VIN_CODE.Width = 125;
            // 
            // CUST_PART_NO
            // 
            this.CUST_PART_NO.DataPropertyName = "CUST_PART_NO";
            this.CUST_PART_NO.HeaderText = "CUST_PART_NO";
            this.CUST_PART_NO.MinimumWidth = 6;
            this.CUST_PART_NO.Name = "CUST_PART_NO";
            this.CUST_PART_NO.ReadOnly = true;
            this.CUST_PART_NO.Width = 125;
            // 
            // PART_NO
            // 
            this.PART_NO.DataPropertyName = "PART_NO";
            this.PART_NO.HeaderText = "PART_NO";
            this.PART_NO.MinimumWidth = 6;
            this.PART_NO.Name = "PART_NO";
            this.PART_NO.ReadOnly = true;
            this.PART_NO.Width = 125;
            // 
            // PART_NAME
            // 
            this.PART_NAME.DataPropertyName = "PART_NAME";
            this.PART_NAME.HeaderText = "PART_NAME";
            this.PART_NAME.MinimumWidth = 6;
            this.PART_NAME.Name = "PART_NAME";
            this.PART_NAME.ReadOnly = true;
            this.PART_NAME.Width = 125;
            // 
            // PLAN_QTY
            // 
            this.PLAN_QTY.DataPropertyName = "PLAN_QTY";
            this.PLAN_QTY.HeaderText = "PLAN_QTY";
            this.PLAN_QTY.MinimumWidth = 6;
            this.PLAN_QTY.Name = "PLAN_QTY";
            this.PLAN_QTY.ReadOnly = true;
            this.PLAN_QTY.Width = 125;
            // 
            // CUST_ORDER_CODE
            // 
            this.CUST_ORDER_CODE.DataPropertyName = "CUST_ORDER_CODE";
            this.CUST_ORDER_CODE.HeaderText = "CUST_ORDER_CODE";
            this.CUST_ORDER_CODE.MinimumWidth = 6;
            this.CUST_ORDER_CODE.Name = "CUST_ORDER_CODE";
            this.CUST_ORDER_CODE.ReadOnly = true;
            this.CUST_ORDER_CODE.Width = 125;
            // 
            // CUST_SORT_INFO_ID
            // 
            this.CUST_SORT_INFO_ID.DataPropertyName = "CUST_SORT_INFO_ID";
            this.CUST_SORT_INFO_ID.HeaderText = "CUST_SORT_INFO_ID";
            this.CUST_SORT_INFO_ID.MinimumWidth = 6;
            this.CUST_SORT_INFO_ID.Name = "CUST_SORT_INFO_ID";
            this.CUST_SORT_INFO_ID.ReadOnly = true;
            this.CUST_SORT_INFO_ID.Width = 125;
            // 
            // SOURCE_ID
            // 
            this.SOURCE_ID.DataPropertyName = "SOURCE_ID";
            this.SOURCE_ID.HeaderText = "SOURCE_ID";
            this.SOURCE_ID.MinimumWidth = 6;
            this.SOURCE_ID.Name = "SOURCE_ID";
            this.SOURCE_ID.ReadOnly = true;
            this.SOURCE_ID.Width = 125;
            // 
            // BARCODE
            // 
            this.BARCODE.DataPropertyName = "BARCODE";
            this.BARCODE.HeaderText = "BARCODE";
            this.BARCODE.MinimumWidth = 6;
            this.BARCODE.Name = "BARCODE";
            this.BARCODE.ReadOnly = true;
            this.BARCODE.Width = 125;
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
            // FrmDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1274, 648);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FrmDetail";
            this.Text = "FrmDetail";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CUST_INFO_SEQ;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOGIC_SEQ;
        private System.Windows.Forms.DataGridViewTextBoxColumn LZ_VIN_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CUST_PART_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PART_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PART_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn PLAN_QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn CUST_ORDER_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CUST_SORT_INFO_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SOURCE_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn BARCODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn STATUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALID_FLAG;
    }
}