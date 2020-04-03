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

            using (var db = new RecievedProductsContext())
            {
                dataGridView1.DataSource = db.Products.ToList();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var dialog = new AddProductForm(Status.Recieved))
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    var product = dialog.Product;

                    using (var db = new RecievedProductsContext())
                    {
                        db.Products.Add(product);
                        db.SaveChanges();

                        dataGridView1.DataSource = db.Products.ToList();
                    }
                }
            }
        }
    }
}
