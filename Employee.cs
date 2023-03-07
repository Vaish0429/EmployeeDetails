using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeDetails
{
    public partial class Employee : Form
    {
        string apiToken = ConfigurationManager.AppSettings["ApiToken"];
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
            
            AddAsync();
        }

       
            
        public bool InputValidation()
        {
            errorProvider1.Clear();

            bool isvalid = true;
            if(String.IsNullOrEmpty(textBox2.Text))
            {
                errorProvider1.SetError(textBox2, "Please enter employee name");
                isvalid = false;
            }
           
            if(comboBox1.SelectedIndex == -1)
            {
                errorProvider1.SetError(comboBox1, "Please select gender");
                isvalid = false;
            }

            if(comboBox2.SelectedIndex == -1)
            {
                errorProvider1.SetError(comboBox2, "Please select status");
                isvalid = false;
            }
            
            string Email = textBox3.Text.Trim();
           ;
            if (String.IsNullOrEmpty(textBox3.Text))
            {
                errorProvider1.SetError(textBox3, "Please enter  email address");
                isvalid = false;
            }
            else if (!Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                errorProvider1.SetError(textBox3, "Please enter valid email address");
                isvalid = false;

            }
           
           
            if (errorProvider1.GetError(textBox2)!="" || errorProvider1.GetError(textBox3) != "" || errorProvider1.GetError(comboBox1) != "" || errorProvider1.GetError(comboBox2) != "")
            {
                MessageBox.Show("Please fill all the inputs");
                isvalid = false;

            }
            return isvalid;
        }
       
        public async Task AddAsync()
        {
          
            string apiurl1 = "https://gorest.co.in/public/v2/users/?id";
            try

            {
                if (InputValidation())
                {
                    string Ename = textBox2.Text.Trim();

                    string Eid = textBox1.Text.Trim();
                    string Email = textBox3.Text.Trim();
                    string Egender = comboBox1.Text;
                    string Estatus = comboBox2.Text;
                    
                    
                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);
                        var parameters = new FormUrlEncodedContent(new[]
                        {
                    //new KeyValuePair<string, string>("id", Eid),
                    new KeyValuePair<string, string>("name", Ename),
                    new KeyValuePair<string, string>("email", Email),
                     new KeyValuePair<string, string>("gender", Egender),
                     
                       new KeyValuePair<string, string>("status", Estatus)
                });

                        var response = await client.PostAsync(apiurl1, parameters);

                        var responseString = await response.Content.ReadAsStringAsync();
                        MessageBox.Show(responseString);
                        var responseJson = JsonDocument.Parse(responseString).RootElement;
                        int id = responseJson.GetProperty("id").GetInt32();
                        MessageBox.Show("Employee Added Successfully" + $"New ID: { id}");
                        //string id1 = responseJson.GetProperty("id").GetString();
                        //MessageBox.Show($"ID:{id1 }");
                        await GetNewlyCreatedID(id, "fa114107311259f5f33e70a5d85de34a2499b4401da069af0b1d835cd5ec0d56");

                    }

                }
               
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured"+ex.Message);
            }
        
        }
        
       public static async Task<string> GetNewlyCreatedID(int id, string authToken)
        {
            using (var client = new HttpClient())
            {
        
                string apiUrl = "https://gorest.co.in/public/v2/users/"+id;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                   
                    string responseContent = await response.Content.ReadAsStringAsync();
                    return responseContent;
                }
                else
                {
                    throw new Exception("API request failed with status code " + response.StatusCode.ToString());
                }
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
        private void textBox1_Leave(object sender,EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                //MessageBox.Show("Please enter employee id");
                textBox1.Focus();
                //errorProvider1.SetError(this.textBox1, "Please enter employee id");
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
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);
                    
                    string param1 = textBox1.Text;
                   
                    var response = await client.DeleteAsync($"https://gorest.co.in/public/v2/users/{param1}");
                    response.EnsureSuccessStatusCode();

                    if (response.IsSuccessStatusCode)
                    {

                        MessageBox.Show("Deleted Successfuly");

                    }
                }
                else
                {
                    MessageBox.Show("Enter the employee ID");
                }               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //MessageBox.Show("Enter employee ID");
            }

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        

        

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox3_Leave(object sender,EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(textBox3.Text))
            {
                Regex reg = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                if (!reg.IsMatch(textBox3.Text))
                {
                    MessageBox.Show("Invalid Email ID");
                    textBox3.Focus();
                    //errorProvider3.SetError(this.textBox3, "Please enter employee id");

                }
            }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(textBox2.Text))
            {
                //MessageBox.Show("Please enter employee name");
                
            }
        }
        private void textBox2_Leave(object sender,EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox2.Text))
            {
                //MessageBox.Show("Please enter employee id");
                
                textBox2.Focus();
                //errorProvider2.SetError(this.textBox2, "Please enter employee name");
            }
        }

        private void Employee_Load(object sender, EventArgs e)
        {

        }
    }
}
