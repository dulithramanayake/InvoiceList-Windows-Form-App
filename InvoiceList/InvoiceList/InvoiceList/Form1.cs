using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace InvoiceList
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns["Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["Qua"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["Value"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
        
        //Creating Data Validation Method to Avoid Null Data
        private bool DataValidation()
        {
            if (txtItemCode.Text == "")
            {
                MessageBox.Show("Please enter Item Code");
                //MessageBox.Show("");
                txtItemCode.Focus();
                return false;
            }

            if (txtDiscri.Text == "")
            {
                MessageBox.Show("Please enter Discription");
                txtDiscri.Focus();
                return false;
            }

            if (txtQua.Text == "")
            {
                MessageBox.Show("Please enter Quantity");
                txtQua.Focus();
                return false;
            }

            if (txtPrice.Text == "")
            {
                MessageBox.Show("Please enter Price");
                txtPrice.Focus();
                return false;
            }

            if (!Information.IsNumeric(txtQua.Text))
            {
                MessageBox.Show("Please Enter Numeric Value for Quantity");
                txtQua.Focus();
                return false;
            }

            if (!Information.IsNumeric(txtPrice.Text))
            {
                MessageBox.Show("Please Enter Numeric Value for Price");
                txtPrice.Focus();
                return false;
            }

            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Calling Data Validation Method in Add Button
            if (DataValidation())
            {
                int row;

                dataGridView1.Rows.Add();
                row = dataGridView1.Rows.Count - 2;

                dataGridView1[0, row].Value = txtItemCode.Text;
                dataGridView1[1, row].Value = txtDiscri.Text;
                dataGridView1[2, row].Value = Convert.ToInt32(txtQua.Text);
                dataGridView1[3, row].Value = Convert.ToDecimal(txtPrice.Text);
                dataGridView1[4, row].Value = Convert.ToInt32(txtQua.Text) * Convert.ToDecimal(txtPrice.Text);
                ClearField();
                FindTotal();
                //txtItemCode.Focus();
            }
        }

        //Method to Clear fields
        private void ClearField()
        {
            txtItemCode.Text = "";
            txtDiscri.Text = "";
            txtQua.Text = "";
            txtPrice.Text = "";

        }

        //Method to Find Total
        private void FindTotal()
        {
            decimal sum = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                sum += Convert.ToDecimal(dataGridView1.Rows[i].Cells[4].Value);
            }
            txtTotal.Text = sum.ToString("###,###.##");
        }

        private void txtItemCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtDiscri.Focus();
            }
        }

        private void txtDiscri_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtQua.Focus();
            }
        }

        private void txtQua_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtPrice.Focus();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to Delete?", "Delete", MessageBoxButtons.OKCancel, 
                MessageBoxIcon.Information);
            if (result.Equals(DialogResult.OK))
            {
                foreach(DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    dataGridView1.Rows.RemoveAt(row.Index);
                }
            }

            FindTotal();
        }
        public int CreateInvTrans()
        {
            List<InvTrans> invlist = new List<InvTrans>();
            for (int i = 0; i <= dataGridView1.Rows.Count-2; ++i)
            {
                InvTrans invTr = new InvTrans();
                invTr.ItemCode = Convert.ToString(dataGridView1.Rows[i].Cells[0].Value);
                invTr.Description = Convert.ToString(dataGridView1.Rows[i].Cells[1].Value);
                invTr.Quantity = Convert.ToDecimal(dataGridView1.Rows[i].Cells[2].Value);
                invTr.Price = Convert.ToDecimal(dataGridView1.Rows[i].Cells[3].Value);
                invTr.value = Convert.ToDecimal(dataGridView1.Rows[i].Cells[4].Value);
                invlist.Add(invTr);
            }
            int InvNo = InvTrans.SaveTranRec(invlist);
            return InvNo;
        }


        private void bntSave_Click(object sender, EventArgs e)
        {
            InvMaster invm = new InvMaster();
            invm.InvDate = DateTime.Now.ToString("yyyy-MM-dd");
            invm.Total = Convert.ToDecimal(txtTotal.Text);

            invm.SaveMasterRec(invm);

            int InvNo = CreateInvTrans();
            MessageBox.Show("Invoice No" + InvNo.ToString() + "Saved Succesfull");
        }
    }
}
