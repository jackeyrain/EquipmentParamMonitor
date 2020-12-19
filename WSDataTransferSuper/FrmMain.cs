using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WSDataTransferSuper
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }
        IFreeSql freeSqlFrom = null;
        IFreeSql freeSqlTo = null;
        private void button1_Click(object sender, EventArgs e)
        {
            if (freeSqlFrom == null)
            {
                freeSqlFrom = new FreeSql.FreeSqlBuilder()
                    .UseConnectionString(FreeSql.DataType.SqlServer, txtFrom.Text)
                    .UseMonitorCommand(null, (o, p) => Console.WriteLine(o.CommandText))
                    .Build();
            }
            if (freeSqlTo == null)
            {
                freeSqlTo = new FreeSql.FreeSqlBuilder()
                    .UseConnectionString(FreeSql.DataType.SqlServer, txtTo.Text)
                    .Build();
            }
            MessageBox.Show("通信已连接!");
            txtSeq.Enabled = true;
            txtFrom.Enabled = txtTo.Enabled = false;
        }

        private List<MES_TI_CIM_FCA_MPAB_SP> SPSet = null;
        private void btnLoad_Click(object sender, EventArgs e)
        {
            List<string> status_code = new List<string>();
            if (cD1.Checked) status_code.Add("D1");
            if (cE.Checked) status_code.Add("E");
            if (cG.Checked) status_code.Add("G");
            List<string> moudle_type = new List<string>();
            if (cX81.Checked) moudle_type.Add("X81");
            if (cX8J.Checked) moudle_type.Add("X8J");
            if (cX8S.Checked) moudle_type.Add("X8S");

            string[] sequence = this.txtSeq.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None).Where(o => !string.IsNullOrWhiteSpace(o)).ToArray();
            if (sequence.Length <= 0)
            {
                MessageBox.Show("开什么玩笑，还没填序列号呢！");
                return;
            }
            SPSet = new List<MES_TI_CIM_FCA_MPAB_SP>();
            SPSet = freeSqlFrom.Select<MES_TI_CIM_FCA_MPAB_SP>()
                .Where(o =>
                        sequence.Contains(o.SEQUENCE_NUMBER.ToString())
                        // &&
                        // o.STATUS_CODE.Equals("g", StringComparison.OrdinalIgnoreCase)
                        )
                .OrderBy(o => o.ID).ToList();
            SPSet = SPSet.Where(o => status_code.Any(p => p.Equals(o.STATUS_CODE, StringComparison.OrdinalIgnoreCase))).ToList();
            SPSet = SPSet.Where(o => moudle_type.Any(p => p.Equals(o.MODULE_TYPE, StringComparison.OrdinalIgnoreCase))).ToList();

            this.dataGridView.DataSource = SPSet;
            this.dataGridView.Refresh();
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            if (SPSet == null || SPSet.Count() <= 0)
            {
                MessageBox.Show("开什么玩笑，还没加载序列号呢！");
                return;
            }
            if (MessageBox.Show(this, "大王，请三思啊！", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK)
            {
                return;
            }

            foreach (var sp in SPSet)
            {
                var parts = freeSqlFrom.Select<MES_TI_CIM_FCA_MPAB_SP_PART>().Where(o => o.SP_ID == sp.ID).ToList();
                sp.STATUS = int.Parse(txtStatusCode.Text.Trim());
                var insertId = freeSqlTo.Insert<MES_TI_CIM_FCA_MPAB_SP>(sp).ExecuteIdentity();
                parts.ForEach(o => o.SP_ID = Convert.ToInt32(insertId));
                var result = freeSqlTo.Insert<MES_TI_CIM_FCA_MPAB_SP_PART>(parts).ExecuteAffrows();
            }
            MessageBox.Show("打完收工！");
        }
    }
}
