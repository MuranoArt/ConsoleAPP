using ConsoleApp.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ConsoleApp.DAL

{
    public class EmployeeDAL
    {
        private readonly string _connectionString;
        public EmployeeDAL(IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(configuration);

            _connectionString = configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("Connection string 'Default' is not configured.");
        }
        public List<EmployeeModel> GetList()
        {
            var listEmployeeModel = new List<EmployeeModel>();
            try
            {
                using SqlConnection con = new(_connectionString);
                using SqlCommand cmd = new("USP_EMPLOYEE_GET_ALL", con) { CommandType = CommandType.StoredProcedure };
                con.Open();
                using SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    listEmployeeModel.Add(new EmployeeModel
                    {
                        AutoID = rdr.IsDBNull(0) ? 0 : rdr.GetInt32(0),
                        EmployeeNumber = rdr.IsDBNull(1) ? 0 : rdr.GetInt32(1),
                        DepartmentNumber = rdr.IsDBNull(2) ? 0 : rdr.GetInt32(2),
                        DepartmentName = rdr.IsDBNull(3) ? string.Empty : rdr.GetString(3),
                        FirstName = rdr.IsDBNull(4) ? string.Empty : rdr.GetString(4),
                        LastName = rdr.IsDBNull(5) ? string.Empty : rdr.GetString(5),
                        ZipCode = rdr.IsDBNull(6) ? string.Empty : rdr.GetString(6)
                    });
                }
            }
            catch
            {
                // Preserve original stack trace
                throw;
            }
            return listEmployeeModel;
        }
    }
}