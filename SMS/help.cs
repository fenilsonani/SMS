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
    public partial class help : Form
    {
        public help()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //it will open the link in the default browser and mail to the given email address
            System.Diagnostics.Process.Start("mailto:21bmiit160@gmail.com ");
        }

        private void help_Load(object sender, EventArgs e)
        {
            //this will check the user is admin or not
            storage storage = new storage();
            if (storage.getUsername() == "Admin")
            {
                groupBox5.Visible = true;
                groupBox6.Visible = false;
            }
            else
            {
                groupBox5.Visible = false;
                groupBox6.Visible = true;
            }
        }
    }
}
