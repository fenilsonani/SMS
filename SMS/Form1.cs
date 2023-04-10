using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Drawing.Drawing2D;

namespace SMS
{
    public partial class Form1 : Form
    {
        SqlConnection con=new SqlConnection("Data Source=DESKTOP-0A790N8\\SQLEXPRESS01;Initial Catalog=sms;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from users where username='" + textBox1.Text + "' and password='" + textBox2.Text + "' and role='" + comboBox1.Text + "'", con);
            
            //sets the username,password,role in the storage class
            storage.setUsername(textBox1.Text);
            storage.setPassword(textBox2.Text);
            storage.setRole(comboBox1.Text);

            SqlDataReader dr=cmd.ExecuteReader();

            if (dr.Read())
            {
                if (comboBox1.Text == "Admin")
                {
                    admin ad=new admin();
                    ad.Show();
                    this.Hide();
                    //MessageBox.Show("Admin");
                }
                else if (comboBox1.Text == "Staff")
                {
                    admin ad=new admin();
                    ad.Show();
                    this.Hide();
                    //MessageBox.Show("Staff");
                }
            }
            else
            {
                MessageBox.Show("Login Failed Please Enter Correct Username,Password and Role");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            //code that will hide this form and open forgetpassword form
            forgetpassword fp = new forgetpassword();
            fp.Show();
            //this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //round the textBox1 of border radious 10
            //textBox1.BorderStyle = BorderStyle.None;
            //GraphicsPath gp = new GraphicsPath();
            //gp.AddEllipse(0, 0, textBox1.Width - 3, textBox1.Height - 3);
            //textBox1.Region = new Region(gp);
        }
    }
}
