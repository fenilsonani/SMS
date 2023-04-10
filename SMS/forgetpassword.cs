using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMS
{
    public partial class forgetpassword : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-0A790N8\\SQLEXPRESS01;Initial Catalog=sms;Integrated Security=True");
        public forgetpassword()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //code for forget password
            con.Close();
            String newpassword=textBox2.Text;
            String confirmpassword=textBox3.Text;
            String username=textBox1.Text;
            if (newpassword == confirmpassword)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update users set password='" + newpassword + "' where username='" + username + "'", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Password Changed Successfully");
                Form1 form1
                    = new Form1();
                form1.Show();
                this.Hide();
                con.Close();
            }
            else
            {
                MessageBox.Show("Password Not Matched");
            }
            con.Open();
        }

        private void forgetpassword_Load(object sender, EventArgs e)
        {
            label4.Text = " Forgetten Password !";
        }
    }
}
