using ConsoleApp.DAL;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace ConsoleApp
{
    class Program
    {
        private static IConfiguration _iconfiguration;
        static void Main(string[] args)
        {
            String LoggedInUser = Environment.UserName;
            Console.WriteLine("Logon User: " + LoggedInUser);            

            DateTime nowDT = DateTime.Now;
            Console.WriteLine("Date: " + nowDT);
            Console.WriteLine("");

            GetAppSettingsFile();
            DisplayEmployees();
        }
        static void GetAppSettingsFile()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _iconfiguration = builder.Build();
        }
        static void DisplayEmployees()
        {
            var EmployeeDAL = new EmployeeDAL(_iconfiguration);
            var listEmployeeModel = EmployeeDAL.GetList();
            listEmployeeModel.ForEach(item =>
            {
                Console.WriteLine(item.AutoID + " | " + item.DepartmentNumber + " | " + item.EmployeeNumber + " | " + item.FirstName + " " + item.LastName);
            });

            //Console.WriteLine("Press any key to stop.");
            //Console.ReadKey();
        }
    }
}