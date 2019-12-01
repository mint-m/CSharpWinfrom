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
    public partial class Form2 : Form
    {
        private string orderid;
        private string cutsid;
        private string bookid;
        private string saleprice;
        private string orderdate;
        private int selectedRowIndex;
        public Form2()
        {
            InitializeComponent();
        }

        public Form2(int selectedRowIndex, string v1, string v2, string v3, string v4, string v5)
        {
            InitializeComponent();
            this.selectedRowIndex = selectedRowIndex;
            this.orderid = v1;
            this.cutsid = v2;
            this.bookid = v3;
            this.saleprice = v4;
            this.orderdate = v5.Substring(0, 10);
        }

        Form1 mainForm;

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string[] rowDatas = {
                txtId.Text,
                txtName.Text,
                txtCountryCode.Text,
                txtDistrict.Text,
                txtPopulation.Text };
            mainForm.UpdateRow(rowDatas);
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            mainForm.DeleteRow(orderid);
            this.Close();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string[] rowDatas = {
                txtName.Text,
                txtCountryCode.Text,
                txtDistrict.Text,
                txtPopulation.Text };
            mainForm.InsertRow(rowDatas);
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            txtId.Text = orderid;
            txtName.Text = cutsid;
            txtCountryCode.Text = bookid;
            txtDistrict.Text = saleprice;
            txtPopulation.Text = orderdate;

            if (Owner != null)
            {
                mainForm = Owner as Form1;
            }
        }

        private void btnTextBoxClear_Click_1(object sender, EventArgs e)
        {
            txtId.Clear();
            txtName.Clear();
            txtCountryCode.Clear();
            txtDistrict.Clear();
            txtPopulation.Clear();
        }
    }
}
