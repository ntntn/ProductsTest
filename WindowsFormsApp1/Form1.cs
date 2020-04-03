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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            using(var db = new ProductContext())
            {
                dataGridView1.DataSource = db.Products.ToList();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var product = new Product();

            using (var dialog = new AddProductForm())
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    product = dialog.Product;

                    using (var db = new ProductContext())
                    {               
                        db.Products.Add(product);
                        db.SaveChanges();

                        dataGridView1.DataSource = db.Products.ToList();
                    }
                    
                    if (product.Status == Status.Recieved)
                    {
                        using (var db = new RecievedProductsContext())
                        {
                            db.Products.Add(product);
                            db.SaveChanges();
                        }
                    }

                    if (product.Status == Status.Storage)
                    {
                        using (var db = new StorageProductsContext())
                        {
                            db.Products.Add(product);
                            db.SaveChanges();
                        }
                    }

                    if (product.Status == Status.Sold)
                    {
                        using (var db = new SoldProductsContext())
                        {
                            db.Products.Add(product);
                            db.SaveChanges();
                        }
                    }

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

            using (var db = new RecievedProductsContext())
            {
                db.Database.Delete();
                db.SaveChanges();
            }

            using (var db = new StorageProductsContext())
            {
                db.Database.Delete();
                db.SaveChanges();
            }

            using (var db = new SoldProductsContext())
            {
                db.Database.Delete();
                db.SaveChanges();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var recievedForm = new RecievedProductsForm();
            recievedForm.Show();
        }
    }
}
