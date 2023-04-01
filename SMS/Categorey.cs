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
using System.Xml.Linq;

namespace SMS
{
    public partial class Categorey : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-0A790N8\\SQLEXPRESS01;Initial Catalog=sms;Integrated Security=True");
        public Categorey()
        {
            InitializeComponent();
        }

        private void Categorey_Load(object sender, EventArgs e)
        {
            button2.Enabled = storage.addCategory;
            button3.Enabled = storage.updateCategory;
            button4.Enabled = storage.deleteCategory;
            button5.Enabled = storage.viewCategory;

            if (button2.Enabled )
            {
                button3.Enabled = storage.viewCategory;
            }
            if (storage.viewCategory)
            {
                textBox1.Enabled = true;
            }
            else
            {
                textBox1.Enabled  =false;
                //that will load maximum c_id+1 in categorey tabel
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //redirect to admin
            admin ad = new admin();
            ad.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //query for insert
            // CREATE TABLE[dbo].[Categorey]
            // (
                //[Id] INT NOT NULL PRIMARY KEY IDENTITY,
                //[name] VARCHAR(MAX) NULL, 
               // [desc] VARBINARY(MAX) NULL
            //)
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into Categorey(name,desc) values('" + textBox2.Text + "','" + textBox3.Text + "');", con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Categorey Added");
            con.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("update Categorey set name='" + textBox2.Text + "',desc='" + textBox3.Text + "' where id='" + textBox1.Text + "'", con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Categorey Updated");
            con.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //fill the textBoxes
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Categorey where c_id='" + textBox1.Text + "'", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textBox2.Text = dr["name"].ToString();
                textBox3.Text = dr["desc"].ToString();
            }
            else
            {
                MessageBox.Show("No data found");
            }
            con.Close();
        }
    }
}
