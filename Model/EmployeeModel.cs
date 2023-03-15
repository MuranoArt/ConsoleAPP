using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Model
{
    public class EmployeeModel
    {
        public int AutoID { get; set; }
        public int EmployeeNumber { get; set; }
        public int DepartmentNumber { get; set; }
        public string DepartmentName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ZipCode { get; set; }
    }
}
