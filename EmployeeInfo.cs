using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Headers;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace EmployeeDetails
{
    public class EmployeeInfo
    {
        
        public int id { get; set; }
        public string name { get; set; }
        
        public string email { get; set; }
       

        public string gender { get; set; }
       
        public string status { get; set; }
        
    }
}