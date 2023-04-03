using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            this.Hide();

        }

        private void admin_Load(object sender, EventArgs e)
        {
            //SETS THE USERNAME IN THE LABEL
            label1.Text = "username : "+ storage.getUsername();
            label2.Text = "role : "+storage.getRole();  
            
            if (storage.getRole() == "Admin")
            {
                manageStaffToolStripMenuItem.Visible = true;
            }
            else
            {
                manageStaffToolStripMenuItem.Visible=false;
            }


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
            this.Hide();
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
            this.Hide();
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
            this.Hide();
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
            this.Hide();
        }

        private void viewAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            staffview staffview = new staffview();
            staffview.Show();
            this.Hide();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            storage.addProduct = true;
            storage.updateProduct = false;
            storage.deleteProduct = false;
            storage.viewProduct = false;
            Product pm = new Product();
            pm.Show();
            this.Hide();
        }

        private void addToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            storage.addCategory = true;
            storage.updateCategory = false;
            storage.deleteCategory = false;
            storage.viewCategory = false;
            Categorey categorey = new Categorey();
            categorey.Show();
            this.Hide();
        }

        private void updateToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            storage.addCategory = false;
            storage.updateCategory = true;
            storage.deleteCategory = false;
            storage.viewCategory = true;
            Categorey categorey = new Categorey();
            categorey.Show();
            this.Hide();
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
            this.Hide();
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
            this.Hide();
        }

        private void viewAllToolStripMenuItem3_Click(object sender, EventArgs e)
        {

        }

        private void viewAllToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            //show the product view
            productView productView = new productView();
            productView.Show();
            this.Hide();

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
            this.Hide();
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
            this.Hide();
        }

        private void addToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            generateBill generateBill = new generateBill();
            generateBill.Show();
            this.Hide();
        }
    }
}
