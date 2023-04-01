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
    public partial class staffview : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-0A790N8\\SQLEXPRESS01;Initial Catalog=sms;Integrated Security=True");
        public staffview()
        {
            InitializeComponent();
        }

        private void staffview_Load(object sender, EventArgs e)
        {

            userDataBind();
            comBoxDataBind();
            
        }

        void userDataBind()
        {
            //binding the data of user  into the datagridview1
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from users", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        void comBoxDataBind()
        {
            //this will fetch all comlumns name from the users tabel

            con.Close();
            con.Open();
            //command for fetching all comuns from tabel
            SqlCommand cmd = new SqlCommand("select * from users", con);
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this will add filter in a query and will show the result in the datagridview1
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from users where " + comboBox1.Text + "='" + textBox1.Text + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if they will click on any recoed it will show in the Messabox
            MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
        }
    }
}