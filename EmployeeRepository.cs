using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace ADO.net
{
    public class EmployeeRepository
    {
        public static string connectionString = @"Server=NIKHIL-ACER\SQLEXPRESS; Initial Catalog =payroll_service; User ID =nikhil; Password=kumar";
        SqlConnection connection = new SqlConnection(connectionString);
        public void GetAllEmployees()
        {
            EmployeeModel model = new EmployeeModel();
            try
            {
                using(connection)
                {
                    string query = @"select * from dbo.employee_payroll";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            model.EmpID = reader.GetInt32(0);
                            model.EmpName = reader.GetString(1);
                            model.PhnNo = reader.GetString(2);
                            model.Address = reader.GetString(3);
                            model.Department = reader.GetString(4);
                            model.Gender = reader.GetChar(5);
                            model.BasicPay = reader.GetFloat(6);
                            model.Deductions = reader.GetFloat(7);
                            model.TaxablePay = reader.GetFloat(8);
                            model.IncomeTax = reader.GetFloat(9);
                            model.NetPay = reader.GetFloat(10);
                            model.StartDate = reader.GetDateTime(11);
                            Console.WriteLine("{0},{1}", model.EmpID,model.EmpName);
                        }
                    }
                    else
                        Console.WriteLine("No data found");
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
