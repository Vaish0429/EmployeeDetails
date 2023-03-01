using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeDetails
{
    public partial class Employee : Form
    {
        public Employee()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBox1.SelectedText))
            {
                MessageBox.Show("Please select the gender");
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBox2.SelectedText))
            {
                MessageBox.Show("Please select the status");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //AddEmpDetailsAsync();
            
            AddAsync();
        }

        public async Task AddEmpDetailsAsync()
        {
            if (!String.IsNullOrEmpty(textBox1.Text))
            {

                // string id = textBox1.Text.Trim();
                string Ename = textBox2.Text.Trim();
                string Eid = textBox1.Text.Trim();
                string Email = textBox3.Text.Trim();
                string Egender = comboBox1.Text;
                string Estatus = comboBox2.Text;
                //int Eid = Int32.Parse(id);
                HttpClient clint = new HttpClient();
                string BaseUrl = "https://gorest.co.in/public/v2/users";
                clint.DefaultRequestHeaders.Accept.Clear();
                clint.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "fa114107311259f5f33e70a5d85de34a2499b4401da069af0b1d835cd5ec0d56");

                
                var data = new { id = Eid, name = Ename, gender = Egender, status = Estatus };
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await clint.PostAsync(BaseUrl, content);
                var result = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                   
                    MessageBox.Show(result);
                    MessageBox.Show("Added ");

                }

                else
                {
                    MessageBox.Show("error");
                }
            }
        }
        public void InputValidation()
        {
            string Ename = textBox2.Text.Trim();

            string Eid = textBox1.Text.Trim();
           
            string Egender = comboBox1.Text;
            string Estatus = comboBox2.Text;
            if (Eid.Length<6)
            {
                MessageBox.Show("Please enter the 6 digit employee id");
            }
            else if(Eid.Length>6)
            {
                MessageBox.Show("Invalid ID");
            }
            else
            {
                MessageBox.Show("Enter the employee id");
            }
            string Email = textBox3.Text.Trim();
            if(!(Email.LastIndexOf("@")>-1))
            {
                MessageBox.Show("Invalid Email Address");
            }
        }
        public async Task AddAsync()
        {
            try
            {

                if (!String.IsNullOrEmpty(textBox1.Text) && !String.IsNullOrEmpty(textBox2.Text) && !String.IsNullOrEmpty(textBox3.Text) &&
                    comboBox1.SelectedIndex != -1 && comboBox2.SelectedIndex != -1)
                {

                    string Ename = textBox2.Text.Trim();

                    string Eid = textBox1.Text.Trim();
                    string Email = textBox3.Text.Trim();
                    string Egender = comboBox1.Text;
                    string Estatus = comboBox2.Text;

                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "fa114107311259f5f33e70a5d85de34a2499b4401da069af0b1d835cd5ec0d56");
                        var parameters = new FormUrlEncodedContent(new[]
                        {
                    //new KeyValuePair<string, string>("id", Eid),
                    new KeyValuePair<string, string>("name", Ename),
                    new KeyValuePair<string, string>("email", Email),
                     new KeyValuePair<string, string>("gender", Egender),

                       new KeyValuePair<string, string>("status", Estatus)
                });

                        var response = await client.PostAsync("https://gorest.co.in/public/v2/users/", parameters);

                        var responseString = await response.Content.ReadAsStringAsync();
                        MessageBox.Show(responseString);

                        MessageBox.Show("Employee Added Successfully");
                    }
                }
                else
                {
                    MessageBox.Show("Enter the employee details");  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured"+ex.Message);
            }
        
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Please enter employee id");
                textBox1.Focus();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DeleteEmployeeDetailsAsync();
        }
        public async Task DeleteEmployeeDetailsAsync()
        {
            try
            {
                if (!String.IsNullOrEmpty(textBox1.Text))


                {
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "fa114107311259f5f33e70a5d85de34a2499b4401da069af0b1d835cd5ec0d56");
                    string BaseUrl = "https://gorest.co.in/public/v2";
                    string endpoint = "/users";
                    string param1 = textBox1.Text;
                    string param2 = textBox2.Text;
                    string param3 = comboBox1.Text;
                    string param4 = textBox3.Text;
                    string param5 = comboBox2.Text;
                    string url = $"{BaseUrl}{endpoint}?id={param1}";

                    var response = await client.DeleteAsync($"https://gorest.co.in/public/v2/users/{param1}");
                    response.EnsureSuccessStatusCode();

                    if (response.IsSuccessStatusCode)
                    {

                        MessageBox.Show("Deleted Successfuly");

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Enter employee ID");
            }

        }


        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        

        

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string email = textBox3.Text;
            //bool isValid = Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!String.IsNullOrEmpty(email))
            {
                MessageBox.Show("enter valid email address");
            }
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Please enter employee name");
            }
        }
    }
}
