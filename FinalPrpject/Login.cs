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
    public partial class Login : Form
    {
        Context Data = new Context();
        public static int puser ;

        public Login()
        {
            InitializeComponent();
            if(Data.Users.Count()==0)
            {
                var user = new User
                {
                    Name = "admin",
                    Password = "admin",
                    IsAdmin = true

                };
                Data.Users.Add(user);
                Data.SaveChanges();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                string User = textBox1.Text;
                string pass = textBox2.Text;

                var check = Data.Users.Where(u => u.Name == User && u.Password == pass).FirstOrDefault();

                if (check != null)
                {
                    puser=check.ID;
                    Form1 F = new Form1();
                    F.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invaled User or Password ");
                }


            }


        }
    }
}
