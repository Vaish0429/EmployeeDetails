using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeDetails
{
    public partial class Export : Form
    {
        public Export()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SearchIDAsync();
        }
        public async Task SearchIDAsync()
        {

            try
            {
                if (!String.IsNullOrEmpty(textBox1.Text))
                {

                    string id = textBox1.Text.Trim();

                    int Eid = Int32.Parse(id);
                    HttpClient clint = new HttpClient();
                    string BaseUrl = "https://gorest.co.in/public/v2/";
                    string endpoint = "/users/";
                    string param1 = id;
                    string url = $"{BaseUrl}{endpoint}?id={param1}";

                    HttpResponseMessage response = await clint.GetAsync(url);

                    string result = await response.Content.ReadAsStringAsync();                   

                    List<EmployeeInfo> data = JsonConvert.DeserializeObject<List<EmployeeInfo>>(result);
                    textBox2.Text = data[0].name;
                    textBox3.Text = data[0].email;
                    comboBox1.DataSource = data;
                    comboBox1.Text = data[0].gender;
                    comboBox2.DataSource = data;
                    comboBox2.Text = data[0].status;
                  
                }
                else
                {
                    MessageBox.Show("Enter Employee ID");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Enter valid Employee ID");
                //MessageBox.Show(ex.Message);
            }
        
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

        private void printDocument1_PrintPage_1(object sender, PrintPageEventArgs e)
        {
           
            Bitmap bitmap = new Bitmap(this.Width, this.Height);
            this.DrawToBitmap(bitmap, new Rectangle(0, 0, this.Width, this.Height));

            // Draw the Bitmap on the printed page
            e.Graphics.DrawImage(bitmap, e.PageBounds);
        }
       
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 6)
            {
                MessageBox.Show("Please enter upto 6 numbers");
                textBox1.Focus();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
