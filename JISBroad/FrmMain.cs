using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace JISBroad
{
    public partial class FrmMain : Form
    {
        private ShowModel[] showModels = new ShowModel[4];
        private IFreeSql fsql = null;
        public FrmMain()
        {
            InitializeComponent();

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            this.KeyPreview = true;
            this.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.F12)
                    Application.Exit();
            };
            dataGridView1.AutoGenerateColumns = false;

            fsql = new FreeSql.FreeSqlBuilder()
             .UseConnectionString(FreeSql.DataType.SqlServer, ConfigurationManager.AppSettings["db_connectionstring"])
             // .UseMonitorCommand(o => Console.WriteLine(o.CommandText), (o, p) => Console.WriteLine(o.CommandText))
             .Build();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            //this.dataGridView1.EnableHeadersVisualStyles = false;
            //DataGridViewColumnHeaderCell dgvc = dataGridView1.Columns[0].HeaderCell;
            //dgvc.Style.BackColor = Color.Black;
            //dgvc.Style.ForeColor = Color.White;
            //dgvc.Style.Font  = new Font(Font.Bold , new )

            DataTable table = new DataTable();
            table.Columns.Add("Col1");
            table.Columns.Add("Col2");
            table.Columns.Add("Col3");
            table.Columns.Add("Col4");
            table.Columns.Add("Col5");
            table.Columns.Add("Col6");

            foreach (var index in Enumerable.Range(0, 4))
            {
                DataRow row = table.NewRow();
                row[0] = "IP ASSY";
                row[1] = "9999999";
                row[2] = "10";
                row[3] = "9999999";
                row[4] = "100";
                row[5] = "100";
                table.Rows.Add(row);
            }

            dataGridView1.DataSource = table;
            dataGridView1.ClearSelection();
            showModels[0] = new ShowModel { Description = "IP ASSY", };
            showModels[1] = new ShowModel { Description = "CONSOLE", };
            showModels[2] = new ShowModel { Description = "DOORLEFT", };
            showModels[3] = new ShowModel { Description = "DOORRIGHT", };

            ShowModel();
        }

        public void ShowModel()
        {
            for (int i = 0; i < 4; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = showModels[i].Description;
                dataGridView1.Rows[i].Cells[1].Value = showModels[i].CurrentSequence;
                dataGridView1.Rows[i].Cells[2].Value = showModels[i].QueueCount;
                dataGridView1.Rows[i].Cells[3].Value = showModels[i].LastShipped;
                dataGridView1.Rows[i].Cells[4].Value = showModels[i].LocalBank;
                dataGridView1.Rows[i].Cells[5].Value = showModels[i].OEMBank;
            }
        }

        private void t_main_Tick(object sender, EventArgs e)
        {
            // IP

            // Console

            // Left DP

            // Right DP

            ShowModel();
        }
    }
}
