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
    public partial class StorageProductsForm : Form
    {
        public StorageProductsForm()
        {
            InitializeComponent();
            PopulateGridView();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var dialog = new AddProductForm(StatusType.Storage))
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

        int idToSell;

        private void dataGridView1_CellContextMenuStripNeeded(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1)
            {
                contextMenuStrip1.Show(Cursor.Position);
                idToSell = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["Id"].FormattedValue.ToString());
            }
        }

        private void sellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var db = new ProductContext())
            {
                var productRecord = db.ProductRecords.Where(p => p.ProductRecordId == idToSell).FirstOrDefault();
                productRecord.StatusId = ProductContext.GetStatusIdByEnum(StatusType.Sold);
                db.SaveChanges();
            }

            PopulateGridView();
        }

        void PopulateGridView()
        {
            using (var db = new ProductContext())
            {
                var statusId = ProductContext.GetStatusIdByEnum(StatusType.Storage);

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
