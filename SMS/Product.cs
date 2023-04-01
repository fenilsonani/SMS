using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMS
{
    public partial class Product : Form
    {
        public Product()
        {
            InitializeComponent();
        }

        private void Product_Load(object sender, EventArgs e)
        {
            button2.Enabled=storage.addProduct;
            button3.Enabled=storage.updateProduct;
            button4.Enabled=storage.deleteProduct;
            button5.Enabled=storage.viewProduct;
        }
    }
}
