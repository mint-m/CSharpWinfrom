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
    public partial class Form3 : Form
    {
        private string custid;
        private string name;
        private string address;
        private string phone;
        private int selectedRowIndex;

        public Form3()
        {
            InitializeComponent();
        }
        public Form3(int selectedRowIndex, string v1, string v2, string v3, string v4)
        {
            InitializeComponent();
            this.selectedRowIndex = selectedRowIndex;
            this.custid = v1;
            this.name = v2;
            this.address = v3;
            this.phone = v4;
        }

        Form1 mainForm;


        private void Form3_Load(object sender, EventArgs e)
        {
            txtId.Text = custid;
            txtName.Text = name;
            txtCountryCode.Text = address;
            txtDistrict.Text = phone;

            if (Owner != null)
            {
                mainForm = Owner as Form1;
            }
        }

        private void btnTextBoxClear_Click(object sender, EventArgs e)
        {
            txtId.Clear();
            txtName.Clear();
            txtCountryCode.Clear();
            txtDistrict.Clear();
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            mainForm.DeleteRow(custid);
            this.Close();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string[] rowDatas = {
                txtName.Text,
                txtCountryCode.Text,
                txtDistrict.Text};
            mainForm.InsertRow(rowDatas);
            this.Close();
        }
    }
}
