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
    public partial class Form1 : Form
    {
        Context Data = new Context();
        public Form1()
        {
            InitializeComponent();
            var user = Data.Users.Where(u => u.ID == Login.puser).FirstOrDefault();
            if (user.IsAdmin == false)
            {
                addToolStripMenuItem.Enabled = false;
                cToolStripMenuItem.Enabled = false;
                itemsToolStripMenuItem.Enabled = false;
                newAdminToolStripMenuItem.Enabled = false;
            }

            comborepo1.ValueMember = "ID";
            comborepo1.DisplayMember = "Name";
            comborepo1.DataSource = Data.Reposotries.ToList();

            combocat1.ValueMember = "ID";
            combocat1.DisplayMember = "Name";
            combocat1.DataSource = Data.Categories.ToList();
            if (Data.Reposotries.Count() > 0)
            {
                comboBox7.ValueMember = "ID";
                comboBox7.DisplayMember = "Name";
                comboBox7.DataSource = Data.Reposotries.ToList();
            }

            comboBox4.ValueMember = "ID";
            comboBox4.DisplayMember = "Name";
            comboBox4.DataSource = Data.Reposotries.ToList();





            Fillgrdview1();





        }

        //MenuBar
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormRepostriys fr = new FormRepostriys();
            fr.Show();
            this.Hide();
        }

        private void cToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCategories fc = new FormCategories();
            fc.Show();
            this.Hide();

        }

        private void itemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormItems fi = new FormItems();
            fi.Show();
            this.Hide();


        }

        private void newAdminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Adduser us = new Adduser();
            us.ShowDialog();

        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changePassword cp = new changePassword();
            cp.ShowDialog();

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();

        }


        private void closeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login l = new Login();
            l.Show();
            this.Hide();
        }


        private void combocat1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int catid = (int)combocat1.SelectedValue;
            comboitem1.ValueMember = "ID";
            comboitem1.DisplayMember = "Name";
            comboitem1.DataSource = Data.Items.Where(i => i.Cattegory.ID == catid).ToList();
        }

        void Fillgrdview1()
        {
            dataGridView1.DataSource = Data.ReposotryItem.Select(rt => new { rt.Reposotry, rt.date, rt.Items.Cattegory, rt.Items.Name, rt.Quantity }).ToList();
            dataGridView2.DataSource = Data.ReposotryItem.Select(rt => new { rt.Reposotry, rt.date, rt.Items.Cattegory, rt.Items.Name, rt.Quantity }).ToList();


        }
        void Fillgrdview3()
        {
            if (Data.Reposotries.Count() > 0) { 
            int repid = (int)comboBox7.SelectedValue;
            printDocument1.DataSource = Data.ReposotryItem
                .Where(r => r.Reposotry.ID == repid)
                .Select(rt => new { rt.ID, rt.Reposotry, rt.date, rt.Items.Cattegory, rt.Items.Name, rt.Items.price, rt.Quantity }).ToList();
            }
        }

        //Add Button
        private void button1_Click(object sender, EventArgs e)
        {
            if (comborepo1.Text != "" && combocat1.Text != "" && comboitem1.Text != "" && textBox2.Text != "")
            {
                int quantity;
                if (int.TryParse(textBox2.Text, out quantity))
                {

                    int repoid = (int)comborepo1.SelectedValue;
                    int itemid = (int)comboitem1.SelectedValue;
                    var reposotry = Data.Reposotries.Where(r => r.ID == repoid).FirstOrDefault();
                    var item = Data.Items.Where(i => i.ID == itemid).FirstOrDefault();


                    var it = Data.ReposotryItem.Where(r => r.Reposotry.ID == repoid && r.date == date1.Value.Date && r.Items.ID == itemid).ToList().FirstOrDefault();

                    if (it != null)
                    {
                        it.Quantity += quantity;
                    }
                    else
                    {

                        var reposotryItem = new ReposotryItem
                        {
                            date = date1.Value.Date,
                            Quantity = quantity,
                            Reposotry = reposotry,
                            Items = item

                        };
                        Data.ReposotryItem.Add(reposotryItem);

                    }



                    Data.SaveChanges();
                    Fillgrdview1();
                    comborepo1.Text = "";
                    date1.Text = "";
                    combocat1.Text = "";
                    comboitem1.Text = "";
                    textBox2.Text = "";
                }
                else
                {
                    MessageBox.Show("PLease Enter Valid Count Nymber", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                MessageBox.Show("PLease Enter Valid Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fillgrdview3();
        }



  

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            int repoid = (int)(comboBox4.SelectedValue);

            //int catid = Convert.ToInt32(comboBox6.SelectedValue);
            comboBox5.DataSource = Data.ReposotryItem
            .Where(r1 => r1.Items.Cattegory.Name == comboBox6.SelectedItem.ToString()
             && r1.Reposotry.ID == repoid &&r1.date==dateTimePicker1.Value.Date)
            .Select(r => r.Items.Name).Distinct().ToList();




        }
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

            int repoid = (int)(comboBox4.SelectedValue);

            textBox1.Text = Data.ReposotryItem
            .Where(r1 => r1.Reposotry.ID == repoid && r1.Items.Name == comboBox5.SelectedItem.ToString()
            && r1.date == dateTimePicker1.Value.Date)
            .Select(r => r.Quantity).FirstOrDefault().ToString();
        }


        float allprice = 0;
        private void button4_Click(object sender, EventArgs e)
        {
            
            int count;
            if (int.TryParse(textBox1.Text, out count))
            {
                if (Data.Reposotries.Count() > 0)
                {
                    int repoid = (int)(comboBox4.SelectedValue);
                    if (Data.ReposotryItem.Count() > 0) { 
                    string itmname = comboBox5.SelectedItem.ToString();
                    

                    float price = (float)Data.Items
                        .Where(p => p.Name == comboBox5.SelectedItem.ToString())
                        .Select(p => p.price).FirstOrDefault();

                    allprice += (price * count);


                    var rpitm = Data.ReposotryItem
                     .Where(r1 => r1.Reposotry.ID == repoid && r1.Items.Name == itmname)
                        .FirstOrDefault();
                    if (count > 0)
                    {
                        int sub = rpitm.Quantity - count;
                        if (sub > 0)
                        {
                            rpitm.Quantity = sub;
                            checkedListBox1.Items.Add(count + "\t" + itmname + "\t" + count + " x " + price);

                        }
                        else if (sub == 0)
                        {
                            Data.ReposotryItem.Remove(rpitm);
                            checkedListBox1.Items.Add(count + "\t" + itmname + "\t" + count + " x " + price);

                        }
                        else
                        {
                            MessageBox.Show("There is not enough number of items");
                        }
                        Data.SaveChanges();
                        textBox1.Text = rpitm.Quantity.ToString();
                        textBox3.Text = allprice.ToString();

                    }
                    }
                }
            }
            else
            {
                    MessageBox.Show("PLease Enter Valid Count Nymber", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (Data.Reposotries.Count() > 0)
            {
                int repoid = (int)(comboBox4.SelectedValue);
                var cat = Data.ReposotryItem
                .Where(r1 => r1.Reposotry.ID == repoid && r1.date == dateTimePicker1.Value.Date)
                .Select(r => r.Items.Cattegory.Name).Distinct().ToList();
                if (cat.Count != 0)
                {
                    comboBox6.DataSource = cat;

                }
                else
                {
                    comboBox5.Text = "";
                    comboBox6.Text = "";
                    textBox1.Text = "";

                }
            }
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            Fillgrdview1();
            Fillgrdview3();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            int repoid = (int)(comboBox4.SelectedValue);

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
          
           
        }

        private void dataGridView2_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView2.SelectedRows)
            {
                comboBox4.Text = row.Cells[0].Value.ToString();
                dateTimePicker1.Text = row.Cells[1].Value.ToString();
                comboBox6.Text = row.Cells[2].Value.ToString();
                comboBox5.Text = row.Cells[3].Value.ToString();
                 textBox1.Text= row.Cells[4].Value.ToString();

            }
        }
    }
}

