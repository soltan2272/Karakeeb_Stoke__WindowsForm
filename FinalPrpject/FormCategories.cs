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
    public partial class FormCategories : Form
    {
        Context Data = new Context();
        public FormCategories()
        {
            InitializeComponent();

            if (Data.Categories.Count() > 0)
            {
                FillListview();
            }

        }

        void FillListview()
        {
            dataGridView1.DataSource = Data.Categories.ToList();
        }

        private void Addbutton_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" && textBox3.Text != "")
            {
              // int id = Convert.ToInt32(textBox1.Text);
                var category = Data.Categories;

                var cat = new Category()
                {
                    Name = textBox2.Text,
                    Description = textBox3.Text
                };
                category.Add(cat);
                Data.SaveChanges();

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                FillListview();
            }
            else
            {
                
                MessageBox.Show("Plese Enter Category Name And Description Name","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);

            }



        }

        private void Editbutton_Click(object sender, EventArgs e)
        {

            if (textBox1.Text!=""&&textBox2.Text != "" && textBox3.Text != "")
            {
                int id = Convert.ToInt32(textBox1.Text);
                var cat = Data.Categories.Where(c => c.ID == id).FirstOrDefault();
                cat.ID = id;
                cat.Name = textBox2.Text;
                cat.Description = textBox3.Text;
                Data.SaveChanges();

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                FillListview();
                Addbutton.Enabled = true;
            }
          else
            {
                MessageBox.Show("Plese Select Cateory from DataGridView ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }

        private void Deletebutton_Click(object sender, EventArgs e)
        {
            if (Data.Categories.Count() > 0) { 
            if (textBox1.Text != "") { 
            int id = Convert.ToInt32(textBox1.Text);
            var cat = Data.Categories.Where(c => c.ID == id).FirstOrDefault();

            var items = Data.Items.Where(i=>i.Cattegory.ID==cat.ID);
                    //   List<ReposotryItem> repsitm = new List<ReposotryItem>();

           var rpitm = Data.ReposotryItem.Where(r => r.Items.Cattegory.ID == id);
           


            Data.Items.RemoveRange(items);
            Data.ReposotryItem.RemoveRange(rpitm);
            Data.Categories.Remove(cat);
            Data.SaveChanges();

            FillListview();

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            Addbutton.Enabled = true;
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

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Addbutton.Enabled = true;
            if (Data.Categories.Count() > 0) { 
            dataGridView1.Rows[0].Selected = false;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
           }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                textBox1.Text = row.Cells[0].Value.ToString();
                textBox2.Text = row.Cells[1].Value.ToString();
                textBox3.Text = row.Cells[2].Value.ToString();
            }
        }

        private void FormCategories_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Addbutton.Enabled = false;
        }

        private void FormCategories_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 f = new Form1();
            f.Refresh();
            f.Show();
        }
    }
}
