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
    public partial class staffManage : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-0A790N8\\SQLEXPRESS01;Initial Catalog=sms;Integrated Security=True");
        public staffManage()
        {
            InitializeComponent();
        }

        private void staffManage_Load(object sender, EventArgs e)
        {
            //this will check the the add,update,delete,view to true or false
            button1.Enabled = storage.getAddStaff();
            button2.Enabled = storage.getUpdateStaff();
            button3.Enabled = storage.getDeleteStaff();
            button4.Enabled = storage.getViewStaff();
           
            if (storage.getAddStaff())
            {
                textBox1.Enabled = false;
            }
            else
            {
                textBox1.Enabled= true;
            }

            //if the add button is true then it will show maximum id + 1 in the textbox

            con.Close();
            con.Open();
            SqlCommand cmd1 = new SqlCommand("select max(id) from users;", con);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                textBox1.Text = (Convert.ToInt32(dr1[0]) + 1).ToString();
            }
            con.Close();

            //code that will fetch the data from country tabel and bind it to the combobox
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from country", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["country_name"]);
            }
            con.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //refirect to admin
            admin ad = new admin();
            ad.Show();
            this.Hide();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //it will shows only selected states city
            String selectedState = comboBox2.SelectedItem.ToString();

            con.Close();
            con.Open();

            SqlCommand cmd = new SqlCommand("select * from state s inner join city c on s.state_id=c.state_id where s.state_name='" + selectedState + "'", con);
            comboBox3.Items.Clear();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox3.Items.Add(dr["city_name"]);
            }
            con.Close();          
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            String selectedCountry = comboBox1.SelectedItem.ToString();

            con.Close();
            con.Open();

            SqlCommand cmd = new SqlCommand("select * from country c inner join state s on c.country_id=s.country_id where c.country_name='" + selectedCountry + "'", con);
            comboBox2.Items.Clear();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr["state_name"]);
            }
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this will add record into the database
            String fullname = textBox2.Text;
            String address= textBox3.Text;
            String c_no = textBox4.Text;
            String dob = dateTimePicker1.Text;
            String doj=dateTimePicker2.Text;
            String email= textBox5.Text;
            String salary= textBox6.Text;
            String username= textBox7.Text;
            String password= textBox8.Text;
            String country = comboBox1.Text;
            String state= comboBox2.Text;
            String city= comboBox3.Text;
            String adhar_crad_no=textBox9.Text;
            /*insert into users
            (name, email, role, dob, doj, salary, password, username, c_no, cdate, country_id, state_id, city_id, adhar_card_no) values
            ('Sonani Fenil', '21bmiit123@gmail.com', 'Staff', '1998-01-01', '2019-01-01', 100000, 'password', '21bmiit123', '1234567890', '2019-01-01', 50, 1, 1, '123456789012');*/

            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into users(name, email,role, dob ,doj,salary,password,username,c_no,cdate,country_id,state_id,city_id,adhar_card_no,address) values('" + fullname + "','" + email + "','Staff','" + dob + "','" + doj + "'," + salary + ",'" + password + "','" + username + "','" + c_no + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "',(select country_id from country where country_name='" + country + "'),(select state_id from state where state_name='" + state + "'),(select city_id from city where city_name='" + city + "'),'" + adhar_crad_no + "' ,'"+address+"' )", con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Record Inserted Successfully");

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int country_id=0;
            int state_id=0;
            int city_id=0;

            textBox1.Enabled = false;
            //this will fetch data from the database where id is equal to the textbox1 and fill data in respective textboxes
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from users where id=" + textBox1.Text, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                textBox2.Text = dr["name"].ToString();
                textBox3.Text = dr["address"].ToString();
                textBox4.Text = dr["c_no"].ToString();
                dateTimePicker1.Text = dr["dob"].ToString();
                dateTimePicker2.Text = dr["doj"].ToString();
                textBox5.Text = dr["email"].ToString();
                textBox6.Text = dr["salary"].ToString();
                textBox7.Text = dr["username"].ToString();
                textBox8.Text = dr["password"].ToString();
                textBox9.Text = dr["adhar_card_no"].ToString();
                //shows the country name in the label12
                country_id = Convert.ToInt32(dr["country_id"]);
                state_id = Convert.ToInt32(dr["state_id"]);
                city_id = Convert.ToInt32(dr["city_id"]);
            }
            dr.Close();

            con.Close();

            con.Open();
            SqlCommand cmd1 = new SqlCommand("select * from country where country_id="+country_id+";", con);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            //System.InvalidOperationException: 'Invalid attempt to call Read when reader is closed this error is coming here how to solve this error'
            while (dr1.Read())
            {
                label12.Text = dr1["country_name"].ToString();
            }
            dr1.Close();
            con.Close();

            con.Open();
            SqlCommand cmd2 = new SqlCommand("select * from state where state_id=" + state_id + ";", con);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                label13.Text = dr2["state_name"].ToString();
            }
            dr2.Close();
            con.Close();
            con.Open();
            SqlCommand cmd3 = new SqlCommand("select * from city where city_id=" + city_id + ";", con);
            SqlDataReader dr3 = cmd3.ExecuteReader();
            while (dr3.Read())
            {
                label14.Text = dr3["city_name"].ToString();
            }
            dr3.Close();
            con.Close();

            //set the visibility of the buttons update and remove to true

            storage.setUpdateStaff(true);
            storage.setDeleteStaff(true);


            button2.Enabled = true;
            button3.Enabled = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //write a query for a update the record
            String fullname = textBox2.Text;
            String address = textBox3.Text;
            String c_no = textBox4.Text;
            String dob = dateTimePicker1.Text;
            String doj = dateTimePicker2.Text;
            String email = textBox5.Text;
            String salary = textBox6.Text;
            String username = textBox7.Text;
            String password = textBox8.Text;
            String country = comboBox1.Text;
            String state = comboBox2.Text;
            String city = comboBox3.Text;
            String adhar_crad_no = textBox9.Text;
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("update users set name='" + fullname + "',address='" + address + "',c_no='" + c_no + "',dob='" + dob + "',doj='" + doj + "',email='" + email + "',salary='" + salary + "',username='" + username + "',password='" + password + "',country_id=(select country_id from country where country_name='" + country + "'),state_id=(select state_id from state where state_name='" + state + "'),city_id=(select city_id from city where city_name='" + city + "'),adhar_card_no='" + adhar_crad_no + "' where id=" + textBox1.Text, con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Record Updated Successfully");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //code for deletion
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from users where id=" + textBox1.Text, con);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Record Deleted Successfully");
        }
    }
}
