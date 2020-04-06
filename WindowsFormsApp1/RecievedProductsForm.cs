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
    public partial class RecievedProductsForm : Form
    {
        public RecievedProductsForm()
        {
            InitializeComponent();
            PopulateGridView();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var dialog = new AddProductForm(StatusType.Recieved))
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    var product = dialog.Product;
                    var productRecord = dialog.ProductRecord;

                    using (var db = new ProductContext())
                    {
                        db.Products.Add(product);
                        db.SaveChanges();

                        productRecord.ProductId = product.ProductId;
                        db.ProductRecords.Add(productRecord);
                        db.SaveChanges();
                    }

                    PopulateGridView();
                }
            }
        }

        void PopulateGridView()
        {
            using (var db = new ProductContext())
            {
                var statusId = ProductContext.GetStatusIdByEnum(StatusType.Recieved);

                var data = (from pr in db.ProductRecords.Where(pr => pr.StatusId == statusId)
                            from p in db.Products.Where(p => p.ProductId == pr.ProductId)
                            from s in db.Statuses.Where(s => s.StatusId == pr.StatusId)
                            select new
                            {
                                Id = p.ProductId,
                                Name = p.Name,
                                Status = s.Name,
                                Date = pr.Date
                            }).ToList();

                dataGridView1.DataSource = data;
            }
        }
    }
}
