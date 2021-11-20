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
    public partial class changePassword : Form
    {
        Context Data = new Context();
        public changePassword()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            var change = Data.Users.Where(u => u.ID == Login.puser).FirstOrDefault();
            if(change.Password==opass.Text)
            {
                change.Password = npass.Text;
                Data.SaveChanges();
                opass.Text = "";
                npass.Text = "";
                MessageBox.Show("Password is Change");
            }
            else
            {
                opass.Text = "";
                npass.Text = "";
                MessageBox.Show("Inaled Old Password");
            }

        }
    }
}
