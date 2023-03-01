using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeDetails
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {
            toolTip1.ToolTipTitle = "Text to display on hover";
            toolTip1.Show("", pictureBox2, 0, pictureBox2.Height, 2000);
        }
        //private void pictureBox2_MouseHover(object sender, EventArgs e)
        //{
        //    toolTip1.ToolTipTitle = "Text to display on hover";
        //    toolTip1.Show("Show", pictureBox2, 0, pictureBox2.Height, 2000);
        //}

    }
}
