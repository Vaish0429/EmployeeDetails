using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Configuration;

namespace EmployeeDetails
{
    public partial class Search : Form
    {
        string apiToken = ConfigurationManager.AppSettings["ApiToken"];
        public Search()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetDetailsAsync();
        }

        public async Task<int> GetDetailsAsync()
        {
            int val=0;
            try
            {
                if (!String.IsNullOrEmpty(textBox1.Text))
                {

                    string id = textBox1.Text.Trim();                   
                    HttpClient clint = new HttpClient();
                    string BaseUrl = "https://gorest.co.in/public/v2/";
                    string endpoint = "/users/";
                    string param1 = id;
                    string url = $"{BaseUrl}{endpoint}?id={param1}";
                    clint.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);
                    HttpResponseMessage response = await clint.GetAsync(url);                 
                   
                    string result = await response.Content.ReadAsStringAsync();
                    DataTable dataTable = JsonConvert.DeserializeObject<DataTable>(result);
                    dataGridView1.DataSource = dataTable;
                                       
                }
                else
                {
                    MessageBox.Show("Enter valid Employee ID");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return val;
        }





        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.Text.Length>6)
            {
                MessageBox.Show("Please enter upto 6 numbers");
                textBox1.Focus();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Search_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GetAllAsync();
        }
        public async Task GetAllAsync()
        {
            string BaseUrl = "https://gorest.co.in/public/v2/";
            string endpoint = "/users/";
            string url = $"{ BaseUrl}{ endpoint}";
           
            HttpClient clint = new HttpClient();
            clint.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);
            HttpResponseMessage response = await clint.GetAsync(url);


            string result = await response.Content.ReadAsStringAsync();
            DataTable dataTable = JsonConvert.DeserializeObject<DataTable>(result);
            dataGridView1.DataSource = dataTable;
        }
    }
}


