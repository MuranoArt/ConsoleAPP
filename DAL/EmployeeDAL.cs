using ConsoleApp.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ConsoleApp.DAL

{
    public class EmployeeDAL
    {
        private string _connectionString;
        public EmployeeDAL(IConfiguration iconfiguration)
        {
            _connectionString = iconfiguration.GetConnectionString("Default");
        }
        public List<EmployeeModel> GetList()
        {
            var listEmployeeModel = new List<EmployeeModel>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("USP_EMPLOYEE_GET_ALL", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        listEmployeeModel.Add(new EmployeeModel
                        {
                            AutoID = Convert.ToInt32(rdr[0]),
                            EmployeeNumber = Convert.ToInt32(rdr[1]),
                            DepartmentNumber = Convert.ToInt32(rdr[2]),
                            DepartmentName = rdr[3].ToString(),
                            FirstName = rdr[4].ToString(),
                            LastName = rdr[5].ToString(),
                            ZipCode = rdr[6].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listEmployeeModel;
        }
    }
}