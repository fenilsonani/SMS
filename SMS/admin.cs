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
    public partial class admin : Form
    {
        private Form1 form = new Form1();
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-0A790N8\\SQLEXPRESS01;Initial Catalog=sms;Integrated Security=True");

        public admin()
        {

            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ///THIS WILL SHOW THE FORGET PASSWORD form and hide the current form
            forgetpassword fg=new forgetpassword();
            fg.Show();
            fg.textBox1.Text=storage.getUsername();
            fg.textBox1.Enabled = false;
            fg.label4.Text = "Change Password!";


        }

        private void admin_Load(object sender, EventArgs e)
        {
            //SETS THE USERNAME IN THE LABEL
            label1.Text = "UserName : "+ storage.getUsername();
            label2.Text = "Role : "+storage.getRole();  
            
            if (storage.getRole() == "Admin")
            {
                manageStaffToolStripMenuItem.Visible = true;
            }
            else
            {
                manageStaffToolStripMenuItem.Visible=false;
                //also disable shortcut keys for adding new Supplier
                manageStaffToolStripMenuItem.ShortcutKeys = Keys.None;
                addSupplierToolStripMenuItem.Visible = false;
                addSupplierToolStripMenuItem.ShortcutKeys = Keys.None;

            }
            //that will change the type of the chart bar to a pie chart
            chart1.Series["Series1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;

            con.Close();
            con.Open();

            //CREATE TABLE[dbo].[bill] (
            //    [bill_id]        INT NOT NULL,
            //    [cu_id] INT NULL,
            //    [total_bill]     FLOAT(53) NULL,
            //    [dicount_amount] FLOAT(53) NULL,
            //    [net_amount] FLOAT(53) NULL,
            //     [date_of_creation] DATE NULL,
            //    PRIMARY KEY CLUSTERED([bill_id] ASC),
            //    FOREIGN KEY([cu_id]) REFERENCES[dbo].[cutomer]([cu_id])
            //);


            //CREATE TABLE[dbo].[bill_product] (
            //    [bill_id] INT NOT NULL,
            //    [p_id] INT NULL,
            //    FOREIGN KEY([p_id]) REFERENCES[dbo].[product]([p_id]),
            //    FOREIGN KEY([bill_id]) REFERENCES[dbo].[bill]([bill_id])
            //);

            //CREATE TABLE[dbo].[cutomer] (
            //    [cu_id]      INT IDENTITY(1, 1) NOT NULL,
            //    [name]       VARCHAR(50) NULL,
            //    [contact_no] VARCHAR(50) NULL,
            //    PRIMARY KEY CLUSTERED([cu_id] ASC)
            //);

            //CREATE TABLE[dbo].[product] (
            //    [p_id]     INT IDENTITY(1, 1) NOT NULL,
            //    [p_name]   VARCHAR(MAX) NULL,
            //    [p_desc] VARCHAR(MAX) NULL,
            //    [price] BIGINT NULL,
            //    [quantity] BIGINT NULL,
            //    [c_id]     INT NULL,
            //    PRIMARY KEY CLUSTERED([p_id] ASC),
            //    FOREIGN KEY([c_id]) REFERENCES[dbo].[Categorey]([c_Id])
            //);

            //this are tables in the database


            //code that will show the highest 5 selling products
            SqlCommand cmd = new SqlCommand("select top 5 p_name,sum(quantity) as total from bill_product inner join product on bill_product.p_id=product.p_id group by p_name order by total desc", con);
     
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                chart1.Series["Series1"].Points.AddXY(dr["p_name"].ToString(), dr["total"].ToString());
            }

            con.Close();

            con.Open();
            //code that will shows the number of product sales in chart2 according to date of creation columns and show indivisual day sales

            //CREATE TABLE[dbo].[bill] (
            //    [bill_id]        INT NOT NULL,
            //    [cu_id] INT NULL,
            //    [total_bill]     FLOAT(53) NULL,
            //    [dicount_amount] FLOAT(53) NULL,
            //    [net_amount] FLOAT(53) NULL,
            //     [date_of_creation] DATE NULL,
            //    PRIMARY KEY CLUSTERED([bill_id] ASC),
            //    FOREIGN KEY([cu_id]) REFERENCES[dbo].[cutomer]([cu_id])
            //);


            //CREATE TABLE[dbo].[bill_product] (
            //    [bill_id] INT NOT NULL,
            //    [p_id] INT NULL,
            //    FOREIGN KEY([p_id]) REFERENCES[dbo].[product]([p_id]),
            //    FOREIGN KEY([bill_id]) REFERENCES[dbo].[bill]([bill_id])
            //);

            //CREATE TABLE[dbo].[cutomer] (
            //    [cu_id]      INT IDENTITY(1, 1) NOT NULL,
            //    [name]       VARCHAR(50) NULL,
            //    [contact_no] VARCHAR(50) NULL,
            //    PRIMARY KEY CLUSTERED([cu_id] ASC)
            //);

            //CREATE TABLE[dbo].[product] (
            //    [p_id]     INT IDENTITY(1, 1) NOT NULL,
            //    [p_name]   VARCHAR(MAX) NULL,
            //    [p_desc] VARCHAR(MAX) NULL,
            //    [price] BIGINT NULL,
            //    [quantity] BIGINT NULL,
            //    [c_id]     INT NULL,
            //    PRIMARY KEY CLUSTERED([p_id] ASC),
            //    FOREIGN KEY([c_id]) REFERENCES[dbo].[Categorey]([c_Id])
            //);
//THIS are the tabels
            con.Close();
            con.Open();
            SqlCommand cmd1 = new SqlCommand("select date_of_creation,sum(total_bill) as total from bill group by date_of_creation", con);
            SqlDataReader dr1 = cmd1.ExecuteReader();

            while (dr1.Read())
            {
                chart2.Series["Series1"].Points.AddXY(dr1["date_of_creation"].ToString(), dr1["total"].ToString());
            }

            //change the name of the series1
            chart2.Series["Series1"].Name = "Sales";
            

        }

        private void addStaffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //not disable add button
            storage.addStaff = true;
            storage.updateStaff = false;
            storage.deleteStaff = false;
            storage.viewStaff = false;
            staffManage sm = new staffManage();
            sm.Show();

        }

        private void viewAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this will set the add,update,delete to false and view to true
            storage.addStaff = false;
            storage.updateStaff = false;
            storage.deleteStaff = false;
            storage.viewStaff = true;
            staffManage sm = new staffManage();
            sm.Show();

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //not disable delete
            storage.addStaff = false;
            storage.updateStaff = false;
            storage.deleteStaff = true;
            storage.viewStaff = true;
            staffManage sm = new staffManage();
            sm.Show();

        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //not disable update
            storage.addStaff = false;
            storage.updateStaff = true;
            storage.deleteStaff = false;
            storage.viewStaff = true;
            staffManage sm = new staffManage();
            sm.Show();

        }

        private void viewAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            staffview staffview = new staffview();
            staffview.Show();

        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            storage.addProduct = true;
            storage.updateProduct = false;
            storage.deleteProduct = false;
            storage.viewProduct = false;
            Product pm = new Product();
            pm.Show();

        }

        private void addToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            storage.addCategory = true;
            storage.updateCategory = false;
            storage.deleteCategory = false;
            storage.viewCategory = false;
            Categorey categorey = new Categorey();
            categorey.Show();

        }

        private void updateToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            storage.addCategory = false;
            storage.updateCategory = true;
            storage.deleteCategory = false;
            storage.viewCategory = true;
            Categorey categorey = new Categorey();
            categorey.Show();

        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //this is for delete
            storage.addCategory = false;
            storage.updateCategory = false;
            storage.deleteCategory = true;
            storage.viewCategory = true;
            Categorey categorey = new Categorey();
            categorey.Show();

        }

        private void viewToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //for the view
            storage.addCategory = false;
            storage.updateCategory = false;
            storage.deleteCategory = false;
            storage.viewCategory = true;
            Categorey categorey = new Categorey();
            categorey.Show();

        }

        private void viewAllToolStripMenuItem3_Click(object sender, EventArgs e)
        {

        }

        private void viewAllToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            //show the product view
            productView productView = new productView();
            productView.Show();


        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //fill product
            storage.addProduct = false;
            storage.updateProduct = false;
            storage.deleteProduct = false;
            storage.viewProduct = true;
            Product pm = new Product();
            pm.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //code for the exit button that will give the user a message box to confirm the exit
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void generateBillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void viewAllToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            report report = new report();
            report.Show();

        }

        private void addToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            generateBill generateBill = new generateBill();
            generateBill.Show();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void hELPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //help help = new help(); 
            //help.Show();
        }

        private void addToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            supplier supplier = new supplier();
            supplier.Show();
            storage storage = new storage();
            storage.addSupplier = true;
            storage.updateSupplier = false;
            storage.deleteSupplier = false;
            storage.viewSupplier = false;
        }

        private void viewupdatedeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            supplier supplier = new supplier();
            supplier.Show();
            storage storage = new storage();
            storage.addSupplier = false;
            storage.updateSupplier = true;
            storage.deleteSupplier = true;
            storage.viewSupplier = true;
        }

        private void viewAllToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            viewSupplier viewSupplier = new viewSupplier();
            viewSupplier.Show();

        }

        private void addSupplierToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }

        private void creditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Credit credit = new Credit();
            credit.Show();
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            help help= new help();
            help.Show();
        }
    }
}
