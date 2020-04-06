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
        ProductRecord productRecord;

        StatusType statusType;

        public Product Product
        {
            get
            {
                return product;
            }
            protected set { }
        }
        public ProductRecord ProductRecord
        {
            get
            {
                return productRecord;
            }
            protected set { }
        }

        public AddProductForm()
        {
            InitializeComponent();

            this.product = new Product();
            this.productRecord = new ProductRecord();

            comboBox1.DataSource = Enum.GetValues(typeof(StatusType));       
        }

        public AddProductForm(StatusType statusType)
        {
            InitializeComponent();

            this.product = new Product();
            this.productRecord = new ProductRecord();
            comboBox1.Hide();
            label2.Hide();

            this.statusType = statusType;
        }


        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {
            statusType = (StatusType)comboBox1.SelectedItem; 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            product.Name = textBox2.Text;
            productRecord.Date = DateTime.Now;
            productRecord.ProductId = product.ProductId;
            productRecord.StatusId = ProductContext.GetStatusIdByEnum(statusType);
        }

    }
}
