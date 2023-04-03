using iTextSharp.text;
using iTextSharp.text.pdf;
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
    public partial class report : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-0A790N8\\SQLEXPRESS01;Initial Catalog=sms;Integrated Security=True");
        public report()
        {
            InitializeComponent();
        }

        private void report_Load(object sender, EventArgs e)
        {
            MyReportBind();
            comboBoxBinding();
        }
        void MyReportBind()
        {

            //CREATE TABLE[dbo].[bill] (
            //    [bill_id]        INT NOT NULL,
            //    [cu_id] INT NULL,
            //    [total_bill]     FLOAT(53) NULL,
            //    [dicount_amount] FLOAT(53) NULL,
            //    [net_amount] FLOAT(53) NULL,
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

            //using join query to get data from two table that will not show dual time id and also shows the bill_prodct table data
            SqlCommand command = new SqlCommand(
             "SELECT b.bill_id, c.name AS customer_name, b.total_bill, b.dicount_amount, b.net_amount, " +
             "p.p_name AS product_name, p.p_desc, p.price, p.quantity " +
             "FROM bill b " +
             "INNER JOIN cutomer c ON b.cu_id = c.cu_id " +
             "INNER JOIN bill_product bp ON b.bill_id = bp.bill_id " +
             "INNER JOIN product p ON bp.p_id = p.p_id", con);

            // create a data adapter and fill a new dataset with the query results
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet dataset = new DataSet();
            adapter.Fill(dataset);

            // set the DataGridView1 control's DataSource property to the dataset's first table
            dataGridView1.DataSource = dataset.Tables[0];


            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this is a for a search query it can perform search any query
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from bill where bill_id='" + textBox1.Text + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
            
        }

        void comboBoxBinding()
        {
            //bind the combobox comlums 
            //con.Close();
            //con.Open();
            //SqlCommand cmd=new SqlCommand("select * from bill",con);
            //SqlDataReader dr = cmd.ExecuteReader();
            //DataTable dt = new DataTable();
            //dt.Columns.Add("bill_id", typeof(int));
            //dt.Load(dr);
            //comboBox1.ValueMember = "bill_id";
            //comboBox1.DataSource = dt;
            //con.Close();

            //con.Close();
            //con.Open();
            //SqlCommand cmd = new SqlCommand(
            // "SELECT b.bill_id, c.name AS customer_name, b.total_bill, b.dicount_amount, b.net_amount, " +
            // "p.p_name AS product_name, p.p_desc, p.price, p.quantity " +
            // "FROM bill b " +
            // "INNER JOIN cutomer c ON b.cu_id = c.cu_id " +
            // "INNER JOIN bill_product bp ON b.bill_id = bp.bill_id " +
            // "INNER JOIN product p ON bp.p_id = p.p_id", con);
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //SqlDataReader dr = cmd.ExecuteReader();
            //for (int i = 0; i < dr.FieldCount; i++)
            //{
            //    if (dr.GetName(i) != "password")
            //    {
            //        comboBox1.Items.Add(dr.GetName(i));
            //    }
            //}
            //con.Close();
            comboBox1.Items.Add("bill_id");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MyReportBind();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            admin admin = new admin();
            admin.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //code will shows the savefileDialogbox and export the data to excel file
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel files (*.xls)|*.xls";
            saveFileDialog1.FilterIndex = 0;
            //saveFileDialog1.RestoreDirectory = true;
            //saveFileDialog1.CreatePrompt = true;
            saveFileDialog1.Title = "Export Excel File To";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ExportToExcel(dataGridView1, saveFileDialog1.FileName);
            }


        }

        private void ExportToExcel(DataGridView dataGridView1, string filename)
        {
            //write a method ExportToe

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //function that will save the in pdf file
            //code will shows the savefileDialogbox and export the data to excel file
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "PDF files (*.pdf)|*.pdf";
            saveFileDialog1.FilterIndex = 0;
            //saveFileDialog1.RestoreDirectory = true;
            //saveFileDialog1.CreatePrompt = true;
            saveFileDialog1.Title = "Export PDF File To";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ExportToPDF(dataGridView1, saveFileDialog1.FileName);
            }

        }

        private void ExportToPDF(DataGridView dataGridView1, string fileName)
        {
           //method will export but not a corrupt ExportToPDF method
            Document doc = new Document(PageSize.A4, 10, 10, 42, 35);
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(fileName, FileMode.Create));
            doc.Open();
            PdfPTable datatable = new PdfPTable(dataGridView1.ColumnCount);
            datatable.DefaultCell.Padding = 3;
            float[] headerwidths = GetTabelWidth(dataGridView1);
            datatable.SetWidths(headerwidths);
            datatable.WidthPercentage = 100;
            datatable.DefaultCell.BorderWidth = 2;
            datatable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            //write the data in the new line
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                datatable.AddCell(dataGridView1.Columns[i].HeaderText);
            }
            datatable.HeaderRows = 1;
            datatable.DefaultCell.BorderWidth = 1;
            //write the data in the new line
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    if (dataGridView1[j, i].Value != null)
                    {
                        datatable.AddCell(dataGridView1[j, i].Value.ToString());
                    }
                }
                datatable.CompleteRow();
            }
            doc.Add(datatable);
            doc.Close();

        }

        private float[] GetTabelWidth(DataGridView dataGridView1)
        {
            //write with methid GetTabelWidth 
            float[] values = new float[dataGridView1.Columns.Count];
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                values[i] = (float)dataGridView1.Columns[i].Width;
            }
            return values;
        }
    }
}
