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
    public partial class supplier : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-0A790N8\\SQLEXPRESS01;Initial Catalog=sms;Integrated Security=True");
        public supplier()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //it will add supplier
            //CREATE TABLE[dbo].[supplier]
            //(

            //    [Id] INT NOT NULL PRIMARY KEY IDENTITY,
            //    [name] VARCHAR(50) NULL, 
            //    [desc] VARCHAR(50) NULL, 
            //)

            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into supplier values('" + textBox2.Text + "','" + textBox3.Text + "');", con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Supplier Added");

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //it will delete the supplier
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from supplier where Id='" + textBox1.Text + "';", con);
            cmd.ExecuteNonQuery();
            con.Close();
    MessageBox.Show("Supplier Deleted");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //it will update supplier 
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("update supplier set name='" + textBox2.Text + "' desc='" + textBox3.Text + "' where Id='" + textBox1.Text + "';", con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Supplier Updated");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //it will fill the supplier details in the textboxes
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from supplier where Id='" + textBox1.Text + "';", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                textBox2.Text = dr[1].ToString();
                textBox3.Text = dr[2].ToString();
            }
        }

        private void supplier_Load(object sender, EventArgs e)
        {
            storage storage = new storage();
            button2.Enabled = storage.addSupplier;
            button3.Enabled = storage.updateSupplier;
            button4.Enabled = storage.deleteSupplier;
            button5.Enabled = storage.viewSupplier;


            //in the textBox1 shows the maximum id + 1
            if (storage.getAddSupplier())
            {
                textBox1.Enabled = false;
                con.Close();
                con.Open();
                SqlCommand cmd1 = new SqlCommand("select max(Id) from supplier;", con);
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
        }
    }
}
