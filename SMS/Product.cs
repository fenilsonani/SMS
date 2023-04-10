using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.AxHost;

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
            suppBind();
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

        void suppBind()
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("select name from supplier", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                comboBox2.Items.Add(dr[0]);
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
            con.Open();

            int selectSuppid;
            SqlCommand cmdsupp = new SqlCommand("select Id from supplier where name='"+comboBox2.SelectedItem+"'", con);
            SqlDataReader sqlDataReader = cmdsupp.ExecuteReader();

            if (sqlDataReader.Read())
            {
                selectSuppid = Convert.ToInt32(sqlDataReader[0]);
            }
            else
            {
                selectSuppid= 0;
            }

            con.Close();
            con.Open();

            //SqlCommand cmd = new SqlCommand("insert into product(p_name,p_desc,price,quantity,c_id) values('" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + selectedcatid + "')", con);
            SqlCommand cmd = new SqlCommand("insert into product(p_name,p_desc,price,quantity,c_id,s_id) values('" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + selectedcatid + "','" + selectSuppid + "')", con);

            cmd.ExecuteNonQuery();

            con.Close();


            //it will upload image in database

            //CREATE TABLE[dbo].[product] (
            //    [p_id]     INT NOT NULL,
            //    [p_name] VARCHAR(MAX) NULL,
            //    [p_desc] VARCHAR(MAX) NULL,
            //    [price] BIGINT NULL,
            //    [quantity] BIGINT NULL,
            //    [c_id]     INT NULL,
            //    [image]    IMAGE NULL,
            //    [sales]    BIGINT NULL,
            //    PRIMARY KEY CLUSTERED([p_id] ASC),
            //    FOREIGN KEY([c_id]) REFERENCES[dbo].[Categorey]([c_Id])
            //);

            //it will upload image in database in images columns
            con.Close();
            con.Open();
            SqlCommand cmd1 = new SqlCommand("update product set image=@img where p_id='" + textBox1.Text + "'", con);
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            byte[] img = ms.ToArray();
            cmd1.Parameters.Add(new SqlParameter("@img", img));
            cmd1.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Image Uploaded");

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
            SqlCommand cmd = new SqlCommand("select * from product where p_id='" + textBox1.Text + "'", con);
            SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
            {
                    textBox2.Text = dr[1].ToString();
                    textBox3.Text = dr[2].ToString();
                    textBox4.Text = dr[3].ToString();
                    textBox5.Text = dr[4].ToString();
                    comboBox1.Text = dr[5].ToString();
                byte[] img = (byte[])(dr[6]);
                    if (img == null)
                {
                        pictureBox1.Image = null;
                    }
                    else
                {
                        MemoryStream ms = new MemoryStream(img);
                        pictureBox1.Image = Image.FromStream(ms);
                    }
                }
                else
            {
                    MessageBox.Show("No Product Found");
                }


            

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
            //CREATE TABLE [dbo].[images]
//            (     
    //           [Id] INT NOT NULL PRIMARY KEY, 
            //    [img] IMAGE NULL
            //)

            //code for image upload
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into images values(@img)", con);
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            byte[] img = ms.ToArray();
            cmd.Parameters.Add(new SqlParameter("@img", img));
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Image Uploaded");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //this will fetch the image from the database and show it in the picturebox

            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from images where Id='" + textBox1.Text + "'", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                byte[] img = (byte[])dr[1];
                MemoryStream ms = new MemoryStream(img);
                pictureBox1.Image = Image.FromStream(ms);
            }
            else
            {
                MessageBox.Show("No Image Found");
            }

            dr.Close();

            con.Close();
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
