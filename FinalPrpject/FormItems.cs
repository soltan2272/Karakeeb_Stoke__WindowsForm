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
    public partial class FormItems : Form
    {
        Context Data = new Context();
        public FormItems()
        {
            InitializeComponent();
            comboBox1.ValueMember = "ID";
            comboBox1.DisplayMember = "Name";
            comboBox1.DataSource = Data.Categories.ToList();
           


            if (Data.Items.Count() > 0)
            {
               // dataGridView1.Rows[0].Selected = true;
                FillListview();
            }

        }

        void FillListview()
        {

            dataGridView1.DataSource = Data.Items.Select(e => new { e.ID, e.Name, e.price, e.Cattegory }).ToList();

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                int num = Convert.ToInt32(textBox1.Text);
                var Check = Data.Items.Where(i => i.ID == num).FirstOrDefault();
                if (Check != null)
                {
                    Addbtn.Enabled = false;
                    updaatebtn.Enabled = true;
                    Deletebtn.Enabled = true;
                  

                }
                else
                {

                    Addbtn.Enabled = true;
                    updaatebtn.Enabled = false;
                    Deletebtn.Enabled = false;
                }

            }
            else
            {
                Addbtn.Enabled = false;
                updaatebtn.Enabled = false;
                Deletebtn.Enabled = false;
                textBox2.Text = "";
                textBox3.Text = "";
                comboBox1.Text = "";

            }
        }

      

        private void Addbtn_Click_1(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            if (textBox2.Text != "" && textBox3.Text != "")
            {
                double price;
              if(double.TryParse(textBox3.Text,out price))
                {
                    int catid = (int)comboBox1.SelectedValue;
                    var items = Data.Items;
                    var cat = Data.Categories.Where(c => c.ID == catid).FirstOrDefault();
                    var item = new Item()
                    {

                        Name = textBox2.Text,
                        price = price,
                        Cattegory = cat
                    };
                    items.Add(item);
                    Data.SaveChanges();

                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    comboBox1.Text = "";
                    FillListview();
                }
              else
           {

          MessageBox.Show("PLease Enter Valid Price", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

          }

            }
            else
             {
                    MessageBox.Show("PLease Enter Item Name and Price and select its category", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
             }
           
            
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            
        }

        private void updaatebtn_Click(object sender, EventArgs e)
        {
            if (textBox1.Text!=""&& textBox2.Text != "" && textBox3.Text != "")
            {
                int id = Convert.ToInt32(textBox1.Text);
                double price;
                if (double.TryParse(textBox3.Text, out price))
                {
                    int catid = (int)comboBox1.SelectedValue;
                    var cat = Data.Categories.Where(c => c.ID == catid).FirstOrDefault();
                    var items = Data.Items.Where(i => i.ID == id).FirstOrDefault();

                    items.ID = id;
                    items.Name = textBox2.Text;
                    items.price = price;
                    items.Cattegory = cat;
                    Data.SaveChanges();

                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    comboBox1.Text = "";
                    FillListview();
                    Addbtn.Enabled = true;
                }
                else
                {
                    MessageBox.Show("PLease Select From Data Grid View", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                MessageBox.Show("PLease Select From Data Grid View", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        

        private void Deletebtn_Click(object sender, EventArgs e)
        {
            if (Data.Items.Count() > 0) { 
            if (textBox1.Text!=""&& textBox2.Text != "" && textBox3.Text != "")
            {
                int id = Convert.ToInt32(textBox1.Text);
                int price = Convert.ToInt32(textBox3.Text);
                var item = Data.Items.Where(i => i.ID == id).FirstOrDefault();
                    var repot = Data.ReposotryItem.Where(i => i.Items.ID == id).ToList();
                    foreach (var itm in repot)
                    {
                        Data.ReposotryItem.Remove(itm);
                    }
                  
                Data.Items.Remove(item);
                Data.SaveChanges();

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                comboBox1.Text = "";
            }
                else
                {
                    MessageBox.Show("PLease Select From Data Grid View", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                FillListview();
            Addbtn.Enabled = true;
            }
            else
            {
                MessageBox.Show("Database is Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                textBox1.Text = row.Cells[0].Value.ToString();
                textBox2.Text = row.Cells[1].Value.ToString();
                textBox3.Text = row.Cells[2].Value.ToString();
                comboBox1.Text = row.Cells[3].Value.ToString();
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Addbtn.Enabled = true;
            if (Data.Items.Count() > 0) { 
            dataGridView1.Rows[0].Selected = false;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "";
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FormItems_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            Addbtn.Enabled = false;
        }

        private void FormItems_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 f = new Form1();
            f.Refresh();
            f.Show();
        }
    }
}
