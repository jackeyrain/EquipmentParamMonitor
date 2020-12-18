using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShippingEmergency
{
    public partial class FrmMain : Form
    {
        private IFreeSql freeSql = null;
        private string Flag;

        public FrmMain()
        {
            InitializeComponent();
            dataResult.AutoGenerateColumns = false;
            this.DoubleBuffered = true;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.richTextBox1.ForeColor = Color.Blue;
            this.richTextBox1.AppendText(File.ReadAllText("Readme.txt"));
            freeSql = new FreeSql.FreeSqlBuilder()
                .UseConnectionString(FreeSql.DataType.SqlServer, ConfigurationManager.AppSettings["db_connectionstring"])
                .UseMonitorCommand(o => Console.WriteLine(o.CommandText), null)
                .Build();
        }

        private void btnIP_Click(object sender, EventArgs e)
        {
            Flag = "IP";
            btnIPExec.Enabled = true;
            btnConsoleExec.Enabled = btnLDExec.Enabled = btnRDExec.Enabled = false;
            Query("7b3a5c96-5f2e-428a-b8f7-42f02c83222d");
        }

        private void Query(string groupID)
        {
            var data = freeSql.Select<MES_TT_WM_SHIPPING>()
                            .Where(o => o.PART_SHIPPING_FID == Guid.Parse(groupID))
                            .InnerJoin<MES_TT_WM_SHIPPING_DETAIL>((o, p) =>
                            o.FID == p.SHIPPING_FID &&
                                (
                                    p.CUST_INFO_SEQ >= Convert.ToInt64(txtFrom.Text.Trim()) &&
                                    p.CUST_INFO_SEQ <= Convert.ToInt64(txtTo.Text.Trim()))
                                )
                            .IncludeMany(o => o.mES_TT_WM_SHIPPING_DETAILs.Where(p => p.SHIPPING_FID == o.FID))
                            .Distinct()
                            .ToList();
            data.ForEach(o =>
            {
                o.FromSeq = o.mES_TT_WM_SHIPPING_DETAILs.Min(p => p.CUST_INFO_SEQ).ToString();
                o.ToSeq = o.mES_TT_WM_SHIPPING_DETAILs.Max(p => p.CUST_INFO_SEQ).ToString();
                o.VehicleCount = o.mES_TT_WM_SHIPPING_DETAILs.GroupBy(p => p.LZ_VIN_CODE).Count();
                o.ItemCount = o.mES_TT_WM_SHIPPING_DETAILs.Count();
            });
            this.dataResult.DataSource = data;
            this.dataResult.Update();
        }

        private void dataResult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 2) return;

            var orderNumber = dataResult.Rows[e.RowIndex].Cells[e.ColumnIndex].Value as string;
            var order = (this.dataResult.DataSource as List<MES_TT_WM_SHIPPING>).FirstOrDefault(o => o.SHIPPING_CODE.Equals(orderNumber));
            FrmDetail frmDetail = new FrmDetail(order);
            frmDetail.ShowDialog(this);
        }

        private void btnConsole_Click(object sender, EventArgs e)
        {
            Flag = "CONSOLE";
            btnConsoleExec.Enabled = true;
            btnIPExec.Enabled = btnLDExec.Enabled = btnRDExec.Enabled = false;
            Query("8f522937-1733-46d5-8941-f79725ab9aaf");
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            Flag = "LD";
            btnLDExec.Enabled = true;
            btnIPExec.Enabled = btnConsoleExec.Enabled = btnRDExec.Enabled = false;
            Query("0d985300-04b1-423c-b769-1ec765bc4430");
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            Flag = "RD";
            btnRDExec.Enabled = true;
            btnIPExec.Enabled = btnConsoleExec.Enabled = btnLDExec.Enabled = false;
            Query("6fd55e7e-d41a-45c6-945a-78c847fa75ba");
        }

        private void dataResult_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < dataResult.Rows.Count; i++)
            {
                var vehicleCount = Convert.ToInt32(dataResult.Rows[i].Cells["VehicleCount"].Value);
                var itemCount = Convert.ToInt32(dataResult.Rows[i].Cells["ItemCount"].Value);
                var fromSeq = Convert.ToInt32(dataResult.Rows[i].Cells["FromSeq"].Value);
                var toSeq = Convert.ToInt32(dataResult.Rows[i].Cells["ToSeq"].Value);

                switch (Flag)
                {
                    case "IP":
                        if (vehicleCount != 3)
                            dataResult.Rows[i].DefaultCellStyle.BackColor = Color.Orange;
                        if ((toSeq - fromSeq) / 10 + 1 != 3)
                            dataResult.Rows[i].DefaultCellStyle.BackColor = Color.Orange;
                        break;
                    case "LD":
                        if (vehicleCount != 15)
                            dataResult.Rows[i].DefaultCellStyle.BackColor = Color.Orange;
                        if (itemCount < 30)
                            dataResult.Rows[i].DefaultCellStyle.BackColor = Color.Orange;
                        if ((toSeq - fromSeq) / 10 + 1 != 15)
                            dataResult.Rows[i].DefaultCellStyle.BackColor = Color.Orange;
                        break;
                    case "RD":
                        if (vehicleCount != 15)
                            dataResult.Rows[i].DefaultCellStyle.BackColor = Color.Orange;
                        if (itemCount < 30)
                            dataResult.Rows[i].DefaultCellStyle.BackColor = Color.Orange;
                        if ((toSeq - fromSeq) / 10 + 1 != 15)
                            dataResult.Rows[i].DefaultCellStyle.BackColor = Color.Orange;
                        break;
                    case "CONSOLE":
                        if (vehicleCount != 10)
                            dataResult.Rows[i].DefaultCellStyle.BackColor = Color.Orange;
                        break;
                }

                if (i + 1 < dataResult.Rows.Count)
                {
                    var diff = Convert.ToInt32(dataResult.Rows[i + 1].Cells["FromSeq"].Value) - toSeq;
                    if (diff != 10)
                        dataResult.Rows[i + 1].DefaultCellStyle.BackColor = Color.Orange;
                }
            }
        }

        private void btnIPExec_Click(object sender, EventArgs e)
        {
            Execute("G100");
        }

        private void Execute(string pointCode)
        {
            List<long> ids = new List<long>();
            foreach (DataGridViewRow row in dataResult.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value))
                    ids.Add(Convert.ToInt64(row.Cells["Id"].Value));
            }
            if (ids.Count <= 0)
            {
                MessageBox.Show("There isn't any data.");
                return;
            }
            if (MessageBox.Show("Make sure create shipping orders application is closed.", "Confirm", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                return;
            }

            var shippings = freeSql.Select<MES_TT_WM_SHIPPING>().Where(o => ids.Contains(o.ID)).IncludeMany(o => o.mES_TT_WM_SHIPPING_DETAILs.Where(p => p.SHIPPING_FID == o.FID)).ToList();
            List<MES_TT_WM_SHIPPING_DETAIL> shippingItems = new List<MES_TT_WM_SHIPPING_DETAIL>();
            shippings.ForEach(o => shippingItems.AddRange(Array.ConvertAll(o.mES_TT_WM_SHIPPING_DETAILs.ToArray(), p => p).ToArray()));
            var seqList = shippingItems.Select(p => p.CUST_INFO_SEQ).ToList();
            List<MES_TT_CI_CUST_SORT_INFO> custSortInfos = freeSql
                .Select<MES_TT_CI_CUST_SORT_INFO>()
                .Where(o => seqList.Contains(o.CUST_INFO_SEQ))
                .Where(o => o.INFO_POINT_CODE.Equals(pointCode, StringComparison.OrdinalIgnoreCase))
                .ToList();

            using (var union = freeSql.CreateUnitOfWork())
            {
                // 删除发运单
                var step1 = freeSql.Delete<MES_TT_WM_SHIPPING>(shippings).WithTransaction(union.GetOrBeginTransaction()).ExecuteAffrows();
                if (step1 < 0) union.Rollback();
                var step2 = freeSql.Delete<MES_TT_WM_SHIPPING_DETAIL>(shippingItems).WithTransaction(union.GetOrBeginTransaction()).ExecuteAffrows();
                if (step2 < 0) union.Rollback();
                // 更改开户排序信息的发运单创建状态
                var step3 = freeSql.Update<MES_TT_CI_CUST_SORT_INFO>()
                    .WithTransaction(union.GetOrBeginTransaction())
                    .SetSource(custSortInfos)
                    .Set(o => o.SHIPPING_ORDER_STATUS, 10)
                    .ExecuteAffrows();
                if (step3 < 0) union.Rollback();

                union.Commit();
                MessageBox.Show("Finish Done.");
            }
        }

        private void btnConsoleExec_Click(object sender, EventArgs e)
        {
            Execute("G300");
        }

        private void btnLDExec_Click(object sender, EventArgs e)
        {
            Execute("G200");
        }

        private void btnRDExec_Click(object sender, EventArgs e)
        {
            Execute("G200");
        }
    }
}