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
    public partial class AddProductForm : Form
    {       

        Product product;
        public Product Product
        {
            get
            {
                return product;
            }
            protected set { }
        }

        public AddProductForm()
        {
            InitializeComponent();

            this.product = new Product();
            comboBox1.DataSource = Enum.GetValues(typeof(Status));       
        }

        public AddProductForm(Status status)
        {
            InitializeComponent();

            this.product = new Product();
            comboBox1.Hide();
            label2.Hide();

            product.status = status;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //product.ProductId = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            product.Name = textBox2.Text;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
                
        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {
            product.Status = (Status)comboBox1.SelectedItem;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            product.Date = DateTime.Now;
        }

    }
}
