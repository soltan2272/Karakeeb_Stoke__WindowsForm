using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalPrpject
{
    public partial class FormRepostriys : Form
    {
        Context Data = new Context();

        public FormRepostriys()
        {
            InitializeComponent();
            FillListview();
        }

        void FillListview()
        {
            dataGridView1.DataSource = Data.Reposotries.ToList();

        }
        private void Addbutton_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                Reposotry reposotry = new Reposotry
                {
                    Name = textBox2.Text
                };

                Data.Reposotries.Add(reposotry);
                Data.SaveChanges();
                textBox1.Text = "";
                textBox2.Text = "";
                FillListview();

            }
            else
            {
                MessageBox.Show("Please Enter Valid Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Editbutton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox1.Text != "") { 
            int id = Convert.ToInt32(textBox1.Text);
            var Editreposotry = Data.Reposotries.Where(r => r.ID == id).FirstOrDefault();
            Editreposotry.ID = id;
            Editreposotry.Name = textBox2.Text;
            Data.SaveChanges();
            textBox1.Text = "";
            textBox2.Text = "";
            FillListview();
            Addbtn.Enabled = true;
           }
            else
            {
                MessageBox.Show("Please Select from DataGridView", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Deletebutton_Click(object sender, EventArgs e)
        {
            if (Data.Reposotries.Count() > 0) {
                if (textBox1.Text != "") { 
            int id = Convert.ToInt32(textBox1.Text);
            var ReposotryDelete = Data.Reposotries.Where(r => r.ID == id).FirstOrDefault();
            var itemDelete = Data.ReposotryItem.Where(r => r.Reposotry.ID == id);
            if (itemDelete!=null)
             {
              Data.ReposotryItem.RemoveRange(itemDelete);
              }
            Data.Reposotries.Remove(ReposotryDelete);
            Data.SaveChanges();
            textBox1.Text = "";
            textBox2.Text = "";
            FillListview();
            Addbtn.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Please Select from DataGridView", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Database is Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                int num = Convert.ToInt32(textBox1.Text);
                var Check = Data.Reposotries.Where(r => r.ID == num).FirstOrDefault();
                if (Check != null)
                {
                    Addbtn.Enabled = false;
                    Updaatebtn.Enabled = true;
                    Deletebtn.Enabled = true;

                }
                else
                {

                    Addbtn.Enabled = true;
                    Updaatebtn.Enabled = false;
                    Deletebtn.Enabled = false;
                }

            }
            else
            {
                Addbtn.Enabled = false;
                Updaatebtn.Enabled = false;
                Deletebtn.Enabled = false;
                textBox2.Text = "";
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                textBox1.Text = row.Cells[0].Value.ToString();
                textBox2.Text = row.Cells[1].Value.ToString();
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (Data.Reposotries.Count() > 0)
            {
                dataGridView1.Rows[0].Selected = false;
                textBox1.Text = "";
                textBox2.Text = "";
            }

        }

        private void FormRepostriys_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 f = new Form1();
            f.Refresh();
            f.Show();
        
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Addbtn.Enabled = false;
        }
    }
}
