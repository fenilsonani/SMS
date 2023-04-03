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
    public partial class generateBill : Form
    {
        SqlConnection con=new SqlConnection("Data Source=DESKTOP-0A790N8\\SQLEXPRESS01;Initial Catalog=sms;Integrated Security=True");
        public generateBill()
        {
            InitializeComponent();
        }

        private void generateBill_Load(object sender, EventArgs e)
        {
            myListInit();
            ComboBindaMy();
            //generate uniuqe id and store display in textbox9 i can also delete the recoed from the database so that is not affect to the next id
            newLoad();
            //on load check connection
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
        }

        void Myloading()
        {
            //this will change the cursor to the loading
            Cursor.Current = Cursors.WaitCursor;
            //where this will shown on form
        }

        void newLoad()
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from bill", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            textBox9.Text = (dt.Rows.Count + 1).ToString();
            textBox9.Enabled = false;
            con.Close();
            //reload the form
            
        }

        void myListInit()
        {
            //it will add columns to the listview1
            listView1.View = View.Details;
            listView1.Columns.Add("Product ID", 80, HorizontalAlignment.Left);
            listView1.Columns.Add("Product Name", 150, HorizontalAlignment.Left);
            listView1.Columns.Add("Product Quantity", 80, HorizontalAlignment.Left);
            listView1.Columns.Add("Product Price", 80, HorizontalAlignment.Left);
            listView1.Columns.Add("Customer ID", 80, HorizontalAlignment.Left);
            listView1.Columns.Add("Customer Name", 150, HorizontalAlignment.Left);
            listView1.Columns.Add("Customer Contact", 80, HorizontalAlignment.Left);
            listView1.Columns.Add("Total", 80, HorizontalAlignment.Left);
        }



        public void ComboBindaMy()
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from product", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            //add p_name to combobox
            comboBox1.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBox1.Items.Add(dt.Rows[i][1].ToString());
            }
            con.Close();
            con.Open();
            SqlCommand cmd1 = new SqlCommand("select * from cutomer", con);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            //add c_name to combobox
            comboBox2.Items.Clear();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                comboBox2.Items.Add(dt1.Rows[i][1].ToString());
            }
            con.Close();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this will fetch the price of the selected product
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from product where p_name='" + comboBox1.Text + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            textBox1.Text = dt.Rows[0][3].ToString();
            textBox2.Text = dt.Rows[0][4].ToString();
            textBox3.Text = dt.Rows[0][0].ToString();
            textBox3.Enabled = false;
            textBox1.Enabled = false;
            textBox4.Enabled = true;
            textBox2.Enabled = false;

            con.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //code for adding new Product
            storage.addProduct = true;
            storage.updateProduct = false;
            storage.deleteProduct = false;
            storage.viewProduct = false;
            Product pm = new Product();
            storage.updateFromBill = true;
            pm.Show();
            //this.Hide();
            //if the product is added then it will update the combobox
            //pm.SendToBack();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //fill the detail in textBoxes

            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from cutomer where name='" + comboBox2.Text + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cu_id.Text = dt.Rows[0][0].ToString();
            cu_co_no.Text = dt.Rows[0][2].ToString();
            con.Close();
            cu_id.Enabled = false;
            cu_co_no.Enabled = false;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            //check if the quantity is available or not
            if (textBox4.Text != "")
            {
                if (Convert.ToInt32(textBox4.Text) > Convert.ToInt32(textBox2.Text))
                {
                    MessageBox.Show("Quantity not available");
                    textBox4.Text = "";
                }
                else
                {
                    //shows the price in textBox3
                    textBox5.Text=( Convert.ToString(Convert.ToInt32(textBox4.Text)*Convert.ToInt32(textBox1.Text)));
                    textBox3.Enabled = false; 
                }
            }
        }

        private void cu_co_no_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ComboBindaMy();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //it will add the value into the listview1
                    //add the value into the listview
            ListViewItem li = new ListViewItem(textBox3.Text);
            li.SubItems.Add(comboBox1.Text);
            li.SubItems.Add(textBox4.Text);
            li.SubItems.Add(textBox1.Text);
            li.SubItems.Add(cu_id.Text);
            li.SubItems.Add(comboBox2.Text);
            li.SubItems.Add(cu_co_no.Text);
            li.SubItems.Add(textBox5.Text);
            
            //code will also update the gradtotal of textbox6 and update quanity in a database
            //it will update the total price of the product
            //code for displayig total
            
            //code for updating the quantity of the product
            try
            {
                con.Close();
                con.Open();
                SqlCommand cmd = new SqlCommand("update product set quantity=quantity-'"+textBox4.Text+"' where p_id='"+textBox3.Text+"'", con);
                Myloading();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Updated");
                con.Close();
                listView1.Items.Add(li);
                int total = 0;
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    total += Convert.ToInt32(listView1.Items[i].SubItems[7].Text);
                }
                textBox6.Text = total.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            //it will give discount if the total is more than 10000 = 5%
            //if 20000>10%
            //if 30000>15%
            //if 40000>20%
            if (textBox6.Text != "")
            {
                if (Convert.ToInt32(textBox6.Text) > 10000 && Convert.ToInt32(textBox6.Text) < 20000)
                {
                    textBox7.Text = Convert.ToString((Convert.ToInt32(textBox6.Text) * 5) / 100);
                }
                else if (Convert.ToInt32(textBox6.Text) > 20000 && Convert.ToInt32(textBox6.Text) < 30000)
                {
                    textBox7.Text = Convert.ToString((Convert.ToInt32(textBox6.Text) * 10) / 100);
                }
                else if (Convert.ToInt32(textBox6.Text) > 30000 && Convert.ToInt32(textBox6.Text) < 40000)
                {
                    textBox7.Text = Convert.ToString((Convert.ToInt32(textBox6.Text) * 15) / 100);
                }
                else if (Convert.ToInt32(textBox6.Text) > 40000)
                {
                    textBox7.Text = Convert.ToString((Convert.ToInt32(textBox6.Text) * 20) / 100);
                }
                else
                {
                    textBox7.Text = Convert.ToString(0);
                }
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            //it will remove discount from the total
            if (textBox7.Text != "")
            {
                textBox8.Text = Convert.ToString(Convert.ToInt32(textBox6.Text) - Convert.ToInt32(textBox7.Text));
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //it will add new customer also check if the customer is already present or not
            //if present then it will not add the customer
            
            try
            {
                con.Close();
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into cutomer values('" + comboBox2.Text + "','" + cu_co_no.Text + "')", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer Added");
                con.Close();
                ComboBindaMy();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //how to store multiple items in database in a single row
            //CREATE TABLE[dbo].[bill_product]
            //(
            //    [bill_id] INT NOT NULL ,
            //    p_id INT,
            //    FOREIGN KEY(bill_id) REFERENCES bill(bill_id),
            //    FOREIGN KEY(p_id) REFERENCES product(p_id)
            //)
            //CREATE TABLE[dbo].[bill] (
            //    [bill_id] INT NOT NULL,
            //    [cu_id] INT NULL,
            //    [total_bill]     FLOAT(53) NULL,
            //    [dicount_amount] FLOAT(53) NULL,
            //    [net_amount] FLOAT(53) NULL,
            //    PRIMARY KEY CLUSTERED([bill_id] ASC),
            //    FOREIGN KEY([cu_id]) REFERENCES[dbo].[cutomer]([cu_id])
            //);
            //write a insert query for a bill_product and bill
            //it will add the value into the database
            try
            {
                con.Close();
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into bill values('" + textBox9.Text + "','" + cu_id.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "')", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Bill Added");
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //for the bill_product tabel it will add from the listview item price is on last index
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                try
                {
                    con.Close();
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into bill_product values('" + textBox9.Text + "','" + listView1.Items[i].SubItems[0].Text + "')", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bill Product Added");
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            //this will reload entire form and clear all the field and load new Id
            generateBill generateBill = new generateBill();
            generateBill.Show();
            this.Close();


        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            admin admin = new admin();
            admin.Show();
            this.Hide();
        }
    }
}
