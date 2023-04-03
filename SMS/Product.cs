using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SMS
{
    public partial class Product : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-0A790N8\\SQLEXPRESS01;Initial Catalog=sms;Integrated Security=True");
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
            catBind();
            if (storage.getAddProduct())
            {
                textBox1.Enabled = false;
                con.Close();
                con.Open();
                SqlCommand cmd1 = new SqlCommand("select max(p_id) from product;", con);
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

        void catBind()
        {
            //this will bind categories into combobox
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("select name from categorey", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0]);
            }
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //code that will fetch the selected cat id from the tabel
            con.Close();
            con.Open();
            int selectedcatid;
            SqlCommand cmdcat=new SqlCommand("select c_id from categorey where name='"+comboBox1.SelectedItem+"'",con);
            SqlDataReader drcat=cmdcat.ExecuteReader();
            if(drcat.Read())
            {
                selectedcatid = Convert.ToInt32(drcat[0]);
            }
            else
            {
                selectedcatid = 0;
            }
            con.Close();
            con.Close();
            con.Open();

            SqlCommand cmd = new SqlCommand("insert into product(p_name,p_desc,price,quantity,c_id) values('" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + selectedcatid + "')", con);

            cmd.ExecuteNonQuery();

            con.Close();

            if(storage.updateFromBill)
            {
                //call ComBinfMy
                generateBill gb = new generateBill();
                gb.ComboBindaMy();
                this.Hide();
                return;
            }

            MessageBox.Show("Product Added");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //code for update
            con.Close();
            con.Open();
            int selectedcatid;
            SqlCommand cmdcat = new SqlCommand("select c_id from categorey where name='" + comboBox1.SelectedItem + "'", con);
            SqlDataReader drcat = cmdcat.ExecuteReader();
            if (drcat.Read())
            {
                selectedcatid = Convert.ToInt32(drcat[0]);
            }
            else
            {
                selectedcatid = 0;
            }
            con.Close();
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("update product set p_name='" + textBox2.Text + "',p_desc='" + textBox3.Text + "',price='" + textBox4.Text + "',quantity='" + textBox5.Text + "',c_id='" + selectedcatid + "' where p_id='" + textBox1.Text + "'", con);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Updated");
            con.Close();

        }


        private void button5_Click(object sender, EventArgs e)
        {
            //code will fetch all the product from the tabel and fill in the appropriate textboxes and combobox
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from product", con);
            SqlDataReader dr = cmd.ExecuteReader();
            int selectedCatid=0;
            if (dr.Read())
            {
                textBox1.Text = dr[0].ToString();
                textBox2.Text = dr[1].ToString();
                textBox3.Text = dr[2].ToString();
                textBox4.Text = dr[3].ToString();
                textBox5.Text = dr[4].ToString();
                selectedCatid = Convert.ToInt32(dr[5]);
                //image will be shown on a picturebox
                //System.DBNull' to type 'System.Byte[]'.'
                if (dr[6] != DBNull.Value)
                {
                    byte[] img = (byte[])dr[6];
                    MemoryStream ms = new MemoryStream(img);
                    pictureBox1.Image = Image.FromStream(ms);
                }
                else
                {
                    pictureBox1.Image = null;
                }
            }
            else
            {
                MessageBox.Show("No Product Found");
            }
            dr.Close();
            SqlCommand cmdcat = new SqlCommand("select name from categorey where c_id='" + selectedCatid + "'", con);
            SqlDataReader drcat = cmdcat.ExecuteReader();
            if (drcat.Read())
            {
                comboBox1.SelectedItem = drcat[0].ToString();
            }
            else
            {
                MessageBox.Show("No Category Found");
            }
            dr.Close();
            con.Close();

            button3.Enabled = true;
            button4.Enabled = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            admin admin = new admin();
            admin.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //code for delete
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from product where p_id='" + textBox1.Text + "'", con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Deleted");
            con.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Choose Image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";
            if (op.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(op.FileName);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //it will upload a image into the database 
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            byte[] img = ms.ToArray();
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("update product set image=@img where p_id='" + textBox1.Text + "'", con);
            cmd.Parameters.AddWithValue("@img", img);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Image Uploaded");
            con.Close();

            //what is the data type of image column in your database?
            //ans is varbinary(max)
            //it can image datatype in database
            //ans is yes
        }
    }
}
