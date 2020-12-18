using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShippingEmergency
{
    public partial class FrmDetail : Form
    {
        private readonly MES_TT_WM_SHIPPING order;

        public FrmDetail(MES_TT_WM_SHIPPING order)
        {
            InitializeComponent();
            this.dataGridView1.AutoGenerateColumns = false;

            this.Load += FrmDetail_Load;
            this.order = order;
        }

        private void FrmDetail_Load(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = order.mES_TT_WM_SHIPPING_DETAILs;
            this.dataGridView1.Update();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (i == 0) continue;
                var _0 = Convert.ToInt64(dataGridView1.Rows[i - 1].Cells["CUST_INFO_SEQ"].Value);
                var _1 = Convert.ToInt64(dataGridView1.Rows[i].Cells["CUST_INFO_SEQ"].Value);
                if ((_1 - _0) >= 20)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Orange;
                }
            }
        }
    }
}
