using MySql.Data.MySqlClient;
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
    public partial class Form1 : Form
    {
        MySqlConnection conn;
        MySqlDataAdapter dataAdapter;
        DataSet dataSet;
        int selectedRowIndex;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string connStr = "server=localhost;port=3306;database=mydb;uid=root;pwd=1252";
            conn = new MySqlConnection(connStr);
            dataSet = new DataSet();

            dataAdapter = new MySqlDataAdapter("SELECT * FROM orders", conn);
            dataAdapter.Fill(dataSet, "orders");
            dataGridView1.DataSource = dataSet.Tables["orders"];

            dataAdapter = new MySqlDataAdapter("SELECT * FROM customer", conn);
            dataAdapter.Fill(dataSet, "customer");
            dataGridView2.DataSource = dataSet.Tables["customer"];

            dataAdapter = new MySqlDataAdapter("SELECT * FROM book", conn);
            dataAdapter.Fill(dataSet, "book");
            dataGridView3.DataSource = dataSet.Tables["book"];

            //
            string sql = "SELECT distinct publisher FROM book";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            try
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())  // 다음 레코드가 있으면 true
                {
                    comboBox1.Items.Add(reader.GetString("publisher"));
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        private void button6_Click(object sender, EventArgs e)//검색 버튼인데 생성하고 이름을 바꿨더니 ?!
        {
            string queryStr;
            if (tabControl1.SelectedTab == tabControl1.TabPages[0])
            {
                //텝 컨트롤 현제 탭이 orders일때
                string[] conditions = new string[5];
                conditions[0] = (textBox1.Text != "") ? "orderid=@orderid" : null;
                conditions[1] = (textBox2.Text != "") ? "custid=@custid" : null;
                conditions[2] = (textBox3.Text != "") ? "bookid=@bookid" : null;
                conditions[3] = (textBox4.Text != "") ? "saleprice=@salesprice" : null;
                conditions[4] = (textBox5.Text != "") ? "orderdate=@orderdate" : null;
                if (conditions[0] != null || conditions[1] != null || conditions[2] != null || conditions[3] != null || conditions[4] != null)
                {
                    queryStr = $"SELECT * FROM orders WHERE ";
                    bool firstCondition = true;
                    for (int i = 0; i < conditions.Length; i++)
                    {
                        if (conditions[i] != null)
                            if (firstCondition)
                            {
                                queryStr += conditions[i];
                                firstCondition = false;
                            }
                            else
                            {
                                queryStr += " and " + conditions[i];
                            }
                    }
                }
                else
                {
                    queryStr = "SELECT * FROM orders";
                }

                dataAdapter.SelectCommand = new MySqlCommand(queryStr, conn);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@orderid", textBox1.Text);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@custid", textBox2.Text);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@bookid", textBox3.Text);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@salesprice", textBox4.Text);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@orderdate", textBox5.Text);

                try
                {
                    conn.Open();
                    dataSet.Clear();
                    if (dataAdapter.Fill(dataSet, "orders") > 0)
                        dataGridView1.DataSource = dataSet.Tables["orders"];
                    else
                        MessageBox.Show("찾는 데이터가 없습니다.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages[1])
            {
                //텝 컨트롤 현제 탭이 user(customer)일때
                string[] conditions = new string[4];
                conditions[0] = (textBox6.Text != "") ? "custid=@custid" : null;
                conditions[1] = (textBox7.Text != "") ? "name=@name" : null;
                conditions[2] = (textBox8.Text != "") ? "address=@address" : null;
                conditions[3] = (textBox9.Text != "") ? "phone=@phone" : null;
                if (conditions[0] != null || conditions[1] != null || conditions[2] != null || conditions[3] != null)
                {
                    queryStr = $"SELECT * FROM customer WHERE ";
                    bool firstCondition = true;
                    for (int i = 0; i < conditions.Length; i++)
                    {
                        if (conditions[i] != null)
                            if (firstCondition)
                            {
                                queryStr += conditions[i];
                                firstCondition = false;
                            }
                            else
                            {
                                queryStr += " and " + conditions[i];
                            }
                    }
                }
                else
                {
                    queryStr = "SELECT * FROM customer";
                }

                dataAdapter.SelectCommand = new MySqlCommand(queryStr, conn);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@custid", textBox6.Text);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@name", textBox7.Text);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@address", textBox8.Text);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@phone", textBox9.Text);

                try
                {
                    conn.Open();
                    dataSet.Clear();
                    if (dataAdapter.Fill(dataSet, "customer") > 0)
                        dataGridView1.DataSource = dataSet.Tables["customer"];
                    else
                        MessageBox.Show("찾는 데이터가 없습니다.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                //텝 컨트롤 현제 탭이 book일때
                string[] conditions = new string[4];
                conditions[0] = (textBox10.Text != "") ? "bookid=@bookid" : null;
                conditions[1] = (textBox11.Text != "") ? "bookname=@bookname" : null;
                conditions[2] = (comboBox1.Text != "") ? "publisher=@publisher" : null;
                conditions[3] = (textBox13.Text != "") ? "price=@price" : null;
                if (conditions[0] != null || conditions[1] != null || conditions[2] != null || conditions[3] != null)
                {
                    queryStr = $"SELECT * FROM book WHERE ";
                    bool firstCondition = true;
                    for (int i = 0; i < conditions.Length; i++)
                    {
                        if (conditions[i] != null)
                            if (firstCondition)
                            {
                                queryStr += conditions[i];
                                firstCondition = false;
                            }
                            else
                            {
                                queryStr += " and " + conditions[i];
                            }
                    }
                }
                else
                {
                    queryStr = "SELECT * FROM book";
                }

                dataAdapter.SelectCommand = new MySqlCommand(queryStr, conn);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@bookid", textBox10.Text);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@bookname", textBox11.Text);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@publisher", comboBox1.Text);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@price", textBox13.Text);

                try
                {
                    conn.Open();
                    dataSet.Clear();
                    if (dataAdapter.Fill(dataSet, "book") > 0)
                        dataGridView1.DataSource = dataSet.Tables["book"];
                    else
                        MessageBox.Show("찾는 데이터가 없습니다.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }


            }
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRowIndex = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[selectedRowIndex];

            // 새로운 폼에 선택된 row의 정보를 담아서 생성
            Form2 Dig = new Form2(
                selectedRowIndex,
                row.Cells[0].Value.ToString(),
                row.Cells[1].Value.ToString(),
                row.Cells[2].Value.ToString(),
                row.Cells[3].Value.ToString(),
                row.Cells[4].Value.ToString()
                );

            Dig.Owner = this;               // 새로운 폼의 부모가 Form1 인스턴스임을 지정
            Dig.ShowDialog();               // 폼 띄우기(Modal)
            Dig.Dispose();
        }

        private void dataGridView2_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            selectedRowIndex = e.RowIndex;
            DataGridViewRow row = dataGridView2.Rows[selectedRowIndex];

            // 새로운 폼에 선택된 row의 정보를 담아서 생성
            Form3 Dig = new Form3(
                selectedRowIndex,
                row.Cells[0].Value.ToString(),
                row.Cells[1].Value.ToString(),
                row.Cells[2].Value.ToString(),
                row.Cells[3].Value.ToString()
                );

            Dig.Owner = this;               // 새로운 폼의 부모가 Form1 인스턴스임을 지정
            Dig.ShowDialog();               // 폼 띄우기(Modal)
            Dig.Dispose();
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRowIndex = e.RowIndex;
            DataGridViewRow row = dataGridView3.Rows[selectedRowIndex];

            // 새로운 폼에 선택된 row의 정보를 담아서 생성
            Form4 Dig = new Form4(
                selectedRowIndex,
                row.Cells[0].Value.ToString(),
                row.Cells[1].Value.ToString(),
                row.Cells[2].Value.ToString(),
                row.Cells[3].Value.ToString()
                );

            Dig.Owner = this;               // 새로운 폼의 부모가 Form1 인스턴스임을 지정
            Dig.ShowDialog();               // 폼 띄우기(Modal)
            Dig.Dispose();
        }

        internal void DeleteRow(string id)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages[0])
            {
                string sql = "DELETE FROM orders WHERE orderid=@orderid";
                dataAdapter.DeleteCommand = new MySqlCommand(sql, conn);
                dataAdapter.DeleteCommand.Parameters.AddWithValue("@orderid", id);

                try
                {
                    conn.Open();
                    dataAdapter.DeleteCommand.ExecuteNonQuery();

                    dataSet.Clear();
                    dataAdapter = new MySqlDataAdapter("SELECT * FROM orders", conn);
                    dataAdapter.Fill(dataSet, "orders");
                    dataGridView1.DataSource = dataSet.Tables["orders"];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            else if(tabControl1.SelectedTab == tabControl1.TabPages[1])
            {
                string sql = "DELETE FROM customer WHERE custid=@custid";
                dataAdapter.DeleteCommand = new MySqlCommand(sql, conn);
                dataAdapter.DeleteCommand.Parameters.AddWithValue("@custid", id);

                try
                {
                    conn.Open();
                    dataAdapter.DeleteCommand.ExecuteNonQuery();

                    dataSet.Clear();
                    dataAdapter = new MySqlDataAdapter("SELECT * FROM customer", conn);
                    dataAdapter.Fill(dataSet, "customer");
                    dataGridView1.DataSource = dataSet.Tables["customer"];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                string sql = "DELETE FROM book WHERE bookid=@bookid";
                dataAdapter.DeleteCommand = new MySqlCommand(sql, conn);
                dataAdapter.DeleteCommand.Parameters.AddWithValue("@bookid", id);

                try
                {
                    conn.Open();
                    dataAdapter.DeleteCommand.ExecuteNonQuery();

                    dataSet.Clear();
                    dataAdapter = new MySqlDataAdapter("SELECT * FROM book", conn);
                    dataAdapter.Fill(dataSet, "book");
                    dataGridView1.DataSource = dataSet.Tables["book"];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void InsertRow(string[] rowDatas)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages[0])
            {
                string queryStr = "INSERT INTO orders (custid, bookid, saleprice, orderdate) " +
                    "VALUES(@custid, @bookid, @saleprice, @orderdate)";
                dataAdapter.InsertCommand = new MySqlCommand(queryStr, conn);
                dataAdapter.InsertCommand.Parameters.Add("@custid", MySqlDbType.Int32);
                dataAdapter.InsertCommand.Parameters.Add("@bookid", MySqlDbType.Int32);
                dataAdapter.InsertCommand.Parameters.Add("@saleprice", MySqlDbType.Int32);
                dataAdapter.InsertCommand.Parameters.Add("@orderdate", MySqlDbType.Date);

                #region Parameter를 이용한 처리
                dataAdapter.InsertCommand.Parameters["@custid"].Value = rowDatas[0];
                dataAdapter.InsertCommand.Parameters["@bookid"].Value = rowDatas[1];
                dataAdapter.InsertCommand.Parameters["@saleprice"].Value = rowDatas[2];
                dataAdapter.InsertCommand.Parameters["@orderdate"].Value = rowDatas[3];

                try
                {
                    conn.Open();
                    dataAdapter.InsertCommand.ExecuteNonQuery();

                    dataSet.Clear();
                    dataAdapter = new MySqlDataAdapter("SELECT * FROM orders", conn);
                    dataAdapter.Fill(dataSet, "orders");                      // DB -> DataSet
                    dataGridView1.DataSource = dataSet.Tables["orders"];      // dataGridView에 테이블 표시                                     // 텍스트 박스 내용 지우기
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                #endregion
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages[1])
            {
                string queryStr = "INSERT INTO customer (name, address, phone) VALUES(@name, @address, @phone)";
                dataAdapter.InsertCommand = new MySqlCommand(queryStr, conn);
                dataAdapter.InsertCommand.Parameters.Add("@name", MySqlDbType.VarChar);
                dataAdapter.InsertCommand.Parameters.Add("@address", MySqlDbType.VarChar);
                dataAdapter.InsertCommand.Parameters.Add("@phone", MySqlDbType.VarChar);

                #region Parameter를 이용한 처리
                dataAdapter.InsertCommand.Parameters["@name"].Value = rowDatas[0];
                dataAdapter.InsertCommand.Parameters["@address"].Value = rowDatas[1];
                dataAdapter.InsertCommand.Parameters["@phone"].Value = rowDatas[2];

                try
                {
                    conn.Open();
                    dataAdapter.InsertCommand.ExecuteNonQuery();

                    dataSet.Clear(); // 이전 데이터 지우기
                    dataAdapter = new MySqlDataAdapter("SELECT * FROM customer", conn);                   
                    dataAdapter.Fill(dataSet, "customer");                      // DB -> DataSet
                    dataGridView1.DataSource = dataSet.Tables["customer"];      // dataGridView에 테이블 표시                                     // 텍스트 박스 내용 지우기
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                #endregion
            }
            else
            {
                string queryStr = "INSERT INTO book (bookname, publisher, price) " +
                                    "VALUES(@bookname, @publisher, @price)";
                dataAdapter.InsertCommand = new MySqlCommand(queryStr, conn);
                dataAdapter.InsertCommand.Parameters.Add("@bookname", MySqlDbType.VarChar);
                dataAdapter.InsertCommand.Parameters.Add("@publisher", MySqlDbType.VarChar);
                dataAdapter.InsertCommand.Parameters.Add("@price", MySqlDbType.Int32);

                #region Parameter를 이용한 처리
                dataAdapter.InsertCommand.Parameters["@bookname"].Value = rowDatas[0];
                dataAdapter.InsertCommand.Parameters["@publisher"].Value = rowDatas[1];
                dataAdapter.InsertCommand.Parameters["@price"].Value = rowDatas[2];

                try
                {
                    conn.Open();
                    dataAdapter.InsertCommand.ExecuteNonQuery();

                    dataSet.Clear();
                    dataAdapter = new MySqlDataAdapter("SELECT * FROM book", conn);
                    dataAdapter.Fill(dataSet, "book");                      // DB -> DataSet
                    dataGridView1.DataSource = dataSet.Tables["book"];      // dataGridView에 테이블 표시                                     // 텍스트 박스 내용 지우기
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                #endregion
            }
        }


        internal void UpdateRow(string[] rowDatas)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages[0])
            {
                string sql = "UPDATE orders SET custid=@custid, bookid=@bookid, saleprice=@saleprice, orderdate=@orderdate WHERE orderid=@orderid";
                dataAdapter.UpdateCommand = new MySqlCommand(sql, conn);
                dataAdapter.UpdateCommand.Parameters.AddWithValue("@orderid", rowDatas[0]);
                dataAdapter.UpdateCommand.Parameters.AddWithValue("@custid", rowDatas[1]);
                dataAdapter.UpdateCommand.Parameters.AddWithValue("@bookid", rowDatas[2]);
                dataAdapter.UpdateCommand.Parameters.AddWithValue("@saleprice", rowDatas[3]);
                dataAdapter.UpdateCommand.Parameters.AddWithValue("@orderdate", rowDatas[4]);

                try
                {
                    conn.Open();
                    dataAdapter.UpdateCommand.ExecuteNonQuery();

                    dataSet.Clear();  // 이전 데이터 지우기
                    dataAdapter = new MySqlDataAdapter("SELECT * FROM orders", conn);
                    dataAdapter.Fill(dataSet, "orders");
                    dataGridView1.DataSource = dataSet.Tables["orders"];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages[1])
            {
                string sql = "UPDATE customer SET name=@name, address=@address, phone=@phone WHERE custid=@custid";
                dataAdapter.UpdateCommand = new MySqlCommand(sql, conn);
                dataAdapter.UpdateCommand.Parameters.AddWithValue("@custid", rowDatas[0]);
                dataAdapter.UpdateCommand.Parameters.AddWithValue("@name", rowDatas[1]);
                dataAdapter.UpdateCommand.Parameters.AddWithValue("@address", rowDatas[2]);
                dataAdapter.UpdateCommand.Parameters.AddWithValue("@phone", rowDatas[3]);

                try
                {
                    conn.Open();
                    dataAdapter.UpdateCommand.ExecuteNonQuery();

                    dataSet.Clear();  // 이전 데이터 지우기
                    dataAdapter = new MySqlDataAdapter("SELECT * FROM customer", conn);
                    dataAdapter.Fill(dataSet, "custmer");
                    dataGridView1.DataSource = dataSet.Tables["custmer"];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                string sql = "UPDATE book SET bookname=@bookname, publisher=@publisher, price=@price WHERE bookid=@bookid";
                dataAdapter.UpdateCommand = new MySqlCommand(sql, conn);
                dataAdapter.UpdateCommand.Parameters.AddWithValue("@bookid", rowDatas[0]);
                dataAdapter.UpdateCommand.Parameters.AddWithValue("@bookname", rowDatas[1]);
                dataAdapter.UpdateCommand.Parameters.AddWithValue("@publisher", rowDatas[2]);
                dataAdapter.UpdateCommand.Parameters.AddWithValue("@price", rowDatas[3]);

                try
                {
                    conn.Open();
                    dataAdapter.UpdateCommand.ExecuteNonQuery();

                    dataSet.Clear();  // 이전 데이터 지우기
                    dataAdapter = new MySqlDataAdapter("SELECT * FROM book", conn);
                    dataAdapter.Fill(dataSet, "book");
                    dataGridView1.DataSource = dataSet.Tables["book"];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages[0])
            {
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
            }
            else if(tabControl1.SelectedTab == tabControl1.TabPages[1])
            {
                textBox6.Clear();
                textBox7.Clear();
                textBox8.Clear();
                textBox9.Clear();
            }
            else
            {
                textBox10.Clear();
                textBox11.Clear();
                comboBox1.Text = "";
                textBox13.Clear();
            }
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            dataSet.Clear();  // 이전 데이터 지우기

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            textBox11.Clear();
            comboBox1.Text = " ";
            textBox13.Clear();

            dataAdapter = new MySqlDataAdapter("SELECT * FROM orders", conn);
            dataAdapter.Fill(dataSet, "orders");
            dataGridView1.DataSource = dataSet.Tables["orders"];

            dataAdapter = new MySqlDataAdapter("SELECT * FROM customer", conn);
            dataAdapter.Fill(dataSet, "customer");
            dataGridView2.DataSource = dataSet.Tables["customer"];

            dataAdapter = new MySqlDataAdapter("SELECT * FROM book", conn);
            dataAdapter.Fill(dataSet, "book");
            dataGridView3.DataSource = dataSet.Tables["book"];
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages[0])
            {
                Form2 Dig = new Form2();
                Dig.Owner = this;               // 새로운 폼의 부모가 Form1 인스턴스임을 지정
                Dig.ShowDialog();               // 폼 띄우기(Modal)
                Dig.Dispose();
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages[1])
            {
                Form3 Dig = new Form3();
                Dig.Owner = this;               // 새로운 폼의 부모가 Form1 인스턴스임을 지정
                Dig.ShowDialog();               // 폼 띄우기(Modal)
                Dig.Dispose();
            }
            else
            {
                Form4 Dig = new Form4();
                Dig.Owner = this;               // 새로운 폼의 부모가 Form1 인스턴스임을 지정
                Dig.ShowDialog();               // 폼 띄우기(Modal)
                Dig.Dispose();
            }
        }

        //
    }
}
