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
    public partial class viewSupplier : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-0A790N8\\SQLEXPRESS01;Initial Catalog=sms;Integrated Security=True");

        public viewSupplier()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this will search the content from textbox and combobox
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from supplier where " + comboBox1.Text + " like '%" + textBox1.Text + "%'", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            gridBind();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        void gridBind()
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from supplier", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void viewSupplier_Load(object sender, EventArgs e)
        {
            gridBind();
            catBind();
        }

        void catBind()
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Categorey", con);
            SqlDataReader dr = cmd.ExecuteReader();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i) != "password")
                {
                    comboBox1.Items.Add(dr.GetName(i));
                }
            }
            con.Close();
        }
    }
}
