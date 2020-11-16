using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FrmMain : Form
    {
        IFreeSql fsql = null;
        public FrmMain()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
        }

        string sql = @"SELECT P.* from ( 
                        SELECT 'IP SHIPPING' 'GROUP', p.SHIPPING_CODE, p.STATUS, sum(p.COUNT) 'COUNT', p.SEQ, COUNT(1) 'QTY', MIN(p.CUST_INFO_SEQ) 'FROM', max(p.CUST_INFO_SEQ) 'TO', (max(p.CUST_INFO_SEQ) - MIN(p.CUST_INFO_SEQ)) / 10 + 1 'DELT'  from (
                        SELECT a.SHIPPING_CODE, a.SEQ, b.CUST_INFO_SEQ, a.STATUS, COUNT(b.CUST_INFO_SEQ) 'COUNT' From MES.TT_WM_SHIPPING a inner join MES.TT_WM_SHIPPING_DETAIL b on a.FID = b.SHIPPING_FID
                          where a.PART_SHIPPING_FID = '7b3a5c96-5f2e-428a-b8f7-42f02c83222d' and a.CREATE_DATE > DATEADD(DAY, -1, GETDATE()) 
                          group by a.SHIPPING_CODE, a.SEQ, b.CUST_INFO_SEQ, b.CUST_INFO_SEQ, a.STATUS ) as p
                          group by p.SHIPPING_CODE, p.SEQ, p.STATUS
                        UNION all
                        SELECT 'LEFT DP SHIPPING' 'GROUP', p.SHIPPING_CODE, p.STATUS, sum(p.COUNT) 'COUNT', p.SEQ, COUNT(1) 'QTY', MIN(p.CUST_INFO_SEQ) 'FROM', max(p.CUST_INFO_SEQ) 'TO', (max(p.CUST_INFO_SEQ) - MIN(p.CUST_INFO_SEQ)) / 10 + 1 'DELT' from (
                        SELECT a.SHIPPING_CODE, a.SEQ, b.CUST_INFO_SEQ, a.STATUS, COUNT(b.CUST_INFO_SEQ) 'COUNT' From MES.TT_WM_SHIPPING a inner join MES.TT_WM_SHIPPING_DETAIL b on a.FID = b.SHIPPING_FID
                          where a.PART_SHIPPING_FID = '0d985300-04b1-423c-b769-1ec765bc4430' and a.CREATE_DATE > DATEADD(DAY, -1, GETDATE()) 
                          group by a.SHIPPING_CODE, a.SEQ, b.CUST_INFO_SEQ, b.CUST_INFO_SEQ, a.STATUS ) as p
                          group by p.SHIPPING_CODE, p.SEQ, p.STATUS
                        UNION all
                        SELECT 'RIGHT DP SHIPPING' 'GROUP', p.SHIPPING_CODE, p.STATUS, sum(p.COUNT) 'COUNT', p.SEQ, COUNT(1) 'QTY', MIN(p.CUST_INFO_SEQ) 'FROM', max(p.CUST_INFO_SEQ) 'TO', (max(p.CUST_INFO_SEQ) - MIN(p.CUST_INFO_SEQ)) / 10 + 1 'DELT' from (
                        SELECT a.SHIPPING_CODE, a.SEQ, b.CUST_INFO_SEQ, a.STATUS, COUNT(b.CUST_INFO_SEQ) 'COUNT' From MES.TT_WM_SHIPPING a inner join MES.TT_WM_SHIPPING_DETAIL b on a.FID = b.SHIPPING_FID
                          where a.PART_SHIPPING_FID = '6fd55e7e-d41a-45c6-945a-78c847fa75ba' and a.CREATE_DATE > DATEADD(DAY, -1, GETDATE()) 
                          group by a.SHIPPING_CODE, a.SEQ, b.CUST_INFO_SEQ, b.CUST_INFO_SEQ, a.STATUS ) as p
                          group by p.SHIPPING_CODE, p.SEQ, p.STATUS
                        UNION all
                        SELECT 'CONSOLE SHIPPING DS' 'GROUP', p.SHIPPING_CODE, p.STATUS, sum(p.COUNT) 'COUNT', p.SEQ, COUNT(1) 'QTY', MIN(p.CUST_INFO_SEQ) 'FROM', max(p.CUST_INFO_SEQ) 'TO', (max(p.CUST_INFO_SEQ) - MIN(p.CUST_INFO_SEQ)) / 10 + 1'DELT' from (
                        SELECT a.SHIPPING_CODE, a.SEQ, b.CUST_INFO_SEQ, a.STATUS, COUNT(b.CUST_INFO_SEQ) 'COUNT' From MES.TT_WM_SHIPPING a inner join MES.TT_WM_SHIPPING_DETAIL b on a.FID = b.SHIPPING_FID
                          where a.PART_SHIPPING_FID = '8f522937-1733-46d5-8941-f79725ab9aaf' and a.CREATE_DATE > DATEADD(DAY, -1, GETDATE()) 
                          group by a.SHIPPING_CODE, a.SEQ, b.CUST_INFO_SEQ, b.CUST_INFO_SEQ, a.STATUS ) as p
                          group by p.SHIPPING_CODE, p.SEQ, p.STATUS
                        UNION all
                        SELECT 'CONSOLE SHIPPING WS' 'GROUP', p.SHIPPING_CODE, p.STATUS, sum(p.COUNT) 'COUNT', p.SEQ, COUNT(1) 'QTY', MIN(p.CUST_INFO_SEQ) 'FROM', max(p.CUST_INFO_SEQ) 'TO', (max(p.CUST_INFO_SEQ) - MIN(p.CUST_INFO_SEQ)) / 10 + 1'DELT' from (
                        SELECT a.SHIPPING_CODE, a.SEQ, b.CUST_INFO_SEQ, a.STATUS, COUNT(b.CUST_INFO_SEQ) 'COUNT' From MES.TT_WM_SHIPPING a inner join MES.TT_WM_SHIPPING_DETAIL b on a.FID = b.SHIPPING_FID
                          where a.PART_SHIPPING_FID = 'a79cc57d-011f-4890-9097-9aca05e91c5e' and a.CREATE_DATE > DATEADD(DAY, -1, GETDATE()) 
                          group by a.SHIPPING_CODE, a.SEQ, b.CUST_INFO_SEQ, b.CUST_INFO_SEQ, a.STATUS ) as p
                          group by p.SHIPPING_CODE, p.SEQ, p.STATUS
                          ) as P
                          order by P.[GROUP], P.SEQ";
        private void Form1_Load(object sender, EventArgs e)
        {
            fsql = new FreeSql.FreeSqlBuilder()
                .UseConnectionString(FreeSql.DataType.SqlServer, "Data Source=10.16.112.21;Initial Catalog=Pisces;User ID=pisces_user;Password=sahasnopassword")
                .Build();
            timer1.Enabled = true;
            timer1_Tick(null, null);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var table = fsql.Ado.ExecuteDataTable(sql);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = table;
            dataGridView1.Refresh();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Boolean result = true;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value.ToString().Equals("IP SHIPPING", StringComparison.OrdinalIgnoreCase))
                {
                    if (row.Cells["QTY"].Value.ToString().Equals("3") &&
                        row.Cells["DELT"].Value.ToString().Equals("3"))
                    {
                        row.DefaultCellStyle.BackColor = Color.GreenYellow;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.OrangeRed;
                        result = false;
                    }
                }
                else if (row.Cells[0].Value.ToString().Equals("LEFT DP SHIPPING", StringComparison.OrdinalIgnoreCase))
                {
                    if (row.Cells["QTY"].Value.ToString().Equals("18") &&
                       row.Cells["DELT"].Value.ToString().Equals("18") &&
                       row.Cells["COUNT"].Value.ToString().Equals("36"))
                    {
                        row.DefaultCellStyle.BackColor = Color.GreenYellow;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.OrangeRed;
                        result = false;
                    }
                }
                else if (row.Cells[0].Value.ToString().Equals("RIGHT DP SHIPPING", StringComparison.OrdinalIgnoreCase))
                {
                    if (row.Cells["QTY"].Value.ToString().Equals("18") &&
                       row.Cells["DELT"].Value.ToString().Equals("18") &&
                       row.Cells["COUNT"].Value.ToString().Equals("36"))
                    {
                        row.DefaultCellStyle.BackColor = Color.GreenYellow;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.OrangeRed;
                        result = false;
                    }
                }
            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value.ToString().Equals("IP SHIPPING") &&
                    i + 1 <= dataGridView1.Rows.Count - 1 &&
                    dataGridView1.Rows[i + 1].Cells[0].Value.ToString().Equals("IP SHIPPING"))
                {
                    if (int.Parse(dataGridView1.Rows[i + 1].Cells["FROM"].Value.ToString()) -
                        int.Parse(dataGridView1.Rows[i].Cells["TO"].Value.ToString()) == 10)
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.GreenYellow;
                    }
                    else
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.OrangeRed;
                        result = false;
                    }
                }

                if (dataGridView1.Rows[i].Cells[0].Value.ToString().Equals("LEFT DP SHIPPING") &&
                    i + 1 <= dataGridView1.Rows.Count - 1 &&
                    dataGridView1.Rows[i + 1].Cells[0].Value.ToString().Equals("LEFT DP SHIPPING"))
                {
                    if (int.Parse(dataGridView1.Rows[i + 1].Cells["FROM"].Value.ToString()) -
                        int.Parse(dataGridView1.Rows[i].Cells["TO"].Value.ToString()) == 10)
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.GreenYellow;
                    }
                    else
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.OrangeRed;
                        result = false;
                    }
                }

                if (dataGridView1.Rows[i].Cells[0].Value.ToString().Equals("RIGHT DP SHIPPING") &&
                  i + 1 <= dataGridView1.Rows.Count - 1 &&
                  dataGridView1.Rows[i + 1].Cells[0].Value.ToString().Equals("RIGHT DP SHIPPING"))
                {
                    if (int.Parse(dataGridView1.Rows[i + 1].Cells["FROM"].Value.ToString()) -
                        int.Parse(dataGridView1.Rows[i].Cells["TO"].Value.ToString()) == 10)
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.GreenYellow;
                    }
                    else
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.OrangeRed;
                        result = false;
                    }
                }
            }

            if (!result)
            {
                MessageBox.Show(this, "快点来看！快点来看！快点来看！", "有问题快修！", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
