using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpPersionerProject
{
    public partial class Form4 : Form
    {
        private string bookid;
        private string bookname;
        private string publisher;
        private string price;
        private int selectedRowIndex;

        public Form4()
        {
            InitializeComponent();
        }

        public Form4(int selectedRowIndex, string v1, string v2, string v3, string v4)
        {
            InitializeComponent();
            this.selectedRowIndex = selectedRowIndex;
            this.bookid = v1;
            this.bookname = v2;
            this.publisher = v3;
            this.price = v4;
        }

        Form1 mainForm;

        private void btnTextBoxClear_Click(object sender, EventArgs e)
        {
            txtId.Clear();
            txtName.Clear();
            txtPublisher.Clear();
            txtPrice.Clear();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            txtId.Text = bookid;
            txtName.Text = bookname;
            txtPublisher.Text = publisher;
            txtPrice.Text = price;

            if (Owner != null)
            {
                mainForm = Owner as Form1;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            mainForm.DeleteRow(bookid);
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string[] rowDatas = {
                // 가둬놓고...
                txtId.Text,
                txtName.Text,
                txtPublisher.Text,
                txtPrice.Text };
            mainForm.UpdateRow(rowDatas);
            this.Close();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string[] rowDatas = {
                txtName.Text,
                txtPublisher.Text,
                txtPrice.Text };
            mainForm.InsertRow(rowDatas);
            this.Close();
        }
    }
}
