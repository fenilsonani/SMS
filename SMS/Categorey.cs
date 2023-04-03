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

            if (storage.addCategory)
            {
                textBox1.Enabled = false;
                con.Close();
                con.Open();
                SqlCommand cmd1 = new SqlCommand("select max(c_id) from Categorey;", con);
                SqlDataReader dr1 = cmd1.ExecuteReader();
                while (dr1.Read())
                {
                    textBox1.Text = (Convert.ToInt32(dr1[0]) + 1).ToString();
                }
                con.Close();
            }
            else
            {
                textBox1.Enabled = true;
            }

            //if the add button is true then it will show maximum id + 1 in the textbox

            

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
            SqlCommand cmd = new SqlCommand("insert into Categorey(name,description) values('" + textBox2.Text + "','" + textBox3.Text + "');", con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Categorey Added");
            con.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("update Categorey set name='" + textBox2.Text + "',description='" + textBox3.Text + "' where c_id='" + textBox1.Text + "'", con);
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
                textBox3.Text = dr["description"].ToString();
            }
            else
            {
                MessageBox.Show("No data found");
            }
            con.Close();

            button3.Enabled = true; button4.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //delete code
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from Categorey where c_id='" + textBox1.Text + "'", con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Categorey Deleted");
            con.Close();
        }
    }
}
