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
    public partial class Adduser : Form
    {
        Context Data = new Context();
        public Adduser()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool admin;
            if (checkBox1.Checked == true)
            {
                admin = true;
            }
            else {
                admin = false;
            }
                
            var user = new User
            {
                Name = usertb.Text,
                Password = passtb.Text,
                IsAdmin=admin
                
            };
            Data.Users.Add(user);
            Data.SaveChanges();

            usertb.Text = "";
            passtb.Text = "";
            checkBox1.Checked = false;
        }
    }
}
