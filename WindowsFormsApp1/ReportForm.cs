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
    public partial class ReportForm : Form
    {
        int StatusId;
        public ReportForm()
        {
            InitializeComponent();

            using (var db = new ProductContext())
            {
                dataGridView1.DataSource = db.Products.ToList();
            }

            comboBox1.DataSource = Enum.GetValues(typeof(StatusType));
            StatusId = ProductContext.GetStatusIdByEnum(StatusType.Recieved);

            PopulateGridView();
        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {
            PopulateGridView();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            PopulateGridView();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            PopulateGridView();
        }

        void PopulateGridView()
        {
            using (var db = new ProductContext())
            {
                var statusId = ProductContext.GetStatusIdByEnum((StatusType)comboBox1.SelectedItem);

                var data = (from pr in db.ProductRecords.Where(pr => pr.StatusId == statusId && pr.Date >= dateTimePicker1.Value.Date && pr.Date <= dateTimePicker2.Value.Date)
                            from p in db.Products.Where(p => p.ProductId == pr.ProductId)
                            from s in db.Statuses.Where(s => s.StatusId == pr.StatusId)
                            select new
                            {
                                Id = p.ProductId,
                                Name = p.Name,
                                Status = s.Name,
                                Date = pr.Date
                            }).ToList();

                label2.Text = data.Count.ToString();

                dataGridView1.DataSource = data;
            }
        }
    }
}
