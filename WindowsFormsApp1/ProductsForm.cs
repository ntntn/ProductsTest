using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class ProductsForm : Form
    {
        public ProductsForm()
        {
            InitializeComponent();

            FillStatusTable();
            PopulateGridView();
        }

        void FillStatusTable()
        {
            var db = new ProductContext();
            if (db.Statuses.ToList().Count > 0) return;

            var status = new Status();

            status.Name = "Получен";
            db.Statuses.Add(status);
            status = new Status();
            status.Name = "Склад";
            db.Statuses.Add(status);
            status = new Status();
            status.Name = "Продан";
            db.Statuses.Add(status);
            db.SaveChanges();
        }

        void PopulateGridView()
        {
            using (var db = new ProductContext())
            {
                var data = (from pr in db.ProductRecords
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


        private void button1_Click(object sender, EventArgs e)
        {         

            using (var dialog = new AddProductForm())
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

        private void button2_Click(object sender, EventArgs e)
        {
            using (var db = new ProductContext())
            {
                db.Database.Delete();
                db.SaveChanges();
            }

            PopulateGridView();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var recievedForm = new RecievedProductsForm();
            recievedForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var storageForm = new StorageProductsForm();
            storageForm.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var reportForm = new ReportForm();
            reportForm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var soldForm = new SoldProductsForm();
            soldForm.Show();
        }


        
    }
}
