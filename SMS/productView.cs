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
    public partial class productView : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-0A790N8\\SQLEXPRESS01;Initial Catalog=sms;Integrated Security=True");
        public productView()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Clear();

        }

        void comBoxDataBind()
        {
            //this will fetch all comlumns name from the users tabel

            con.Close();
            con.Open();
            //command for fetching all comuns from tabel
            SqlCommand cmd = new SqlCommand("select * from product", con);
            SqlDataReader dr = cmd.ExecuteReader();
            //this will fetch all comlumns name from the users tabel
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i) != "password")
                {
                    comboBox1.Items.Add(dr.GetName(i));
                }
            }
            con.Close();
        }

        void catDataBind()
        {
            //binding the data of user  into the datagridview1
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from product", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Close();
            con.Open();
            //SqlCommand cmd = new SqlCommand("select * from product where " + comboBox1.Text + "='" + textBox1.Text + "'", con);
            SqlCommand cmd= new SqlCommand("select * from product where " + comboBox1.Text + " like '%" + textBox1.Text + "%'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            catDataBind();
            textBox1.Clear();
            comboBox1.Text = "";

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void productView_Load(object sender, EventArgs e)
        {
            comBoxDataBind();
            catDataBind();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            admin admin = new admin();
            admin.Show();
            this.Hide();
        }
    }
}
