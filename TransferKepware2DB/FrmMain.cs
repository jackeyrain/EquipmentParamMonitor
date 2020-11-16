using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TransferKepware2DB
{
    public partial class FrmMain : Form
    {
        IFreeSql fSql = null;
        public FrmMain()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            fSql = new FreeSql.FreeSqlBuilder()
                .UseConnectionString(FreeSql.DataType.SqlServer, ConfigurationManager.AppSettings["db_connectionstring"])
                .Build();
        }
        private IList<string[]> content = null;
        private void btnChoose_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "CSV File|*.csv";
            fd.RestoreDirectory = true;
            if (fd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            this.txtFile.Text = fd.FileName;
            this.txtEquipName.Text = fd.SafeFileName.Replace(".csv", "").Replace(".CSV", "");
            CSVHelper csv = new CSVHelper(txtFile.Text);
            content = new List<string[]>();
            content = csv.Context;
            DataTable table = new DataTable();
            foreach (var index in Enumerable.Range(1, 17))
            {
                table.Columns.Add("Field " + index, typeof(string));
            }
            foreach (var data in content)
            {
                var row = table.NewRow();
                for (int i = 0; i < data.Length; i++)
                {
                    row[i] = data[i];
                }
                table.Rows.Add(row);
            }
            dataGridView1.DataSource = table;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Are you sure IMPORT the data?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }

            using (var uion = fSql.CreateUnitOfWork())
            {
                try
                {
                    fSql.Delete<MES_TP_Equipmentvariable>().Where("1=1").WithTransaction(uion.GetOrBeginTransaction()).ExecuteAffrows();

                    List<MES_TP_Equipmentvariable> data = new List<MES_TP_Equipmentvariable>();
                    data.AddRange(Array.ConvertAll(content.ToArray(), o => new MES_TP_Equipmentvariable
                    {
                        Tag_Name = o[0],
                        Address = o[1],
                        Data_Type = o[2],
                        Respect_Data_Type = string.IsNullOrEmpty(o[3]) ? 0 : Convert.ToDouble(o[3]),
                        Client_Access = o[4],
                        Scan_Rate = string.IsNullOrEmpty(o[5]) ? 0 : Convert.ToDouble(o[5]),
                        Scaling = o[6],
                        Raw_Low = o[7],
                        Raw_High = o[8],
                        Scaled_Low = o[9],
                        Scaled_High = o[10],
                        Scaled_Data_Type = o[11],
                        Clamp_Low = o[12],
                        Clamp_High = o[13],
                        Eng_Units = o[14],
                        Description = o[15],
                        Negate_Value = o[16],
                    }));
                    fSql.Insert<MES_TP_Equipmentvariable>(data).WithTransaction(uion.GetOrBeginTransaction()).ExecuteAffrows();

                    uion.Commit();
                }
                catch (Exception ex)
                {
                    uion.Rollback();
                    MessageBox.Show(ex.Message);
                }

                var info = fSql.Select<MES_TM_BAS_EQUIPMENT>().Where(o => o.NAME.Equals(txtEquipName.Text, StringComparison.OrdinalIgnoreCase)).ToList().FirstOrDefault();
                if (info == null)
                {
                    MessageBox.Show("This Equipment hasn't been maintained!");
                    return;
                }

                string sql = File.ReadAllText("sql.txt");
                sql = sql.Replace("{{EQUIP}}", info.ID.ToString());
                var result = fSql.Ado.ExecuteNonQuery(CommandType.Text, sql);
                if (result > 0)
                {
                    MessageBox.Show("Import Success.");
                }
            }
        }
    }
}
