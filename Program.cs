using ConsoleApp.DAL;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace ConsoleApp
{
    class Program
    {
        private static IConfiguration _configuration;

        static void Main()
        {
            var loggedInUser = Environment.UserName;
            Console.WriteLine("Logon User: " + loggedInUser);

            var now = DateTime.Now;
            Console.WriteLine("Date: " + now);
            Console.WriteLine();

            GetAppSettingsFile();
            DisplayEmployees();
        }
        static void GetAppSettingsFile()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();
        }
        static void DisplayEmployees()
        {
            var employeeDal = new EmployeeDAL(_configuration);
            var listEmployeeModel = employeeDal.GetList();
            foreach (var item in listEmployeeModel)
            {
                Console.WriteLine(item.AutoID + " | " + item.DepartmentNumber + " | " + item.EmployeeNumber + " | " + item.FirstName + " " + item.LastName);
            }

        }
    }
}