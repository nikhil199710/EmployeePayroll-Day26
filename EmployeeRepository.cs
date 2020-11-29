using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace ADO.net
{
    public class EmployeeRepository
    {
        /// <summary>
        /// UC 1:
        /// The connection string which creates the connection between our code and the databse
        /// </summary>
        public static string connectionString = @"Server=NIKHIL-ACER\SQLEXPRESS; Initial Catalog =payroll_service; User ID =nikhil; Password=kumar";
        SqlConnection connection = new SqlConnection(connectionString);
        /// <summary>
        /// UC 2:
        /// Gets all the employees from the database.
        /// </summary>
        /// <exception cref="Exception"></exception>
        public double GetAllEmployees()
        {
            EmployeeModel model = new EmployeeModel();
            try
            {
                using(this.connection)
                {
                    string query = @"select * from dbo.employee_payroll";
                    SqlCommand command = new SqlCommand(query, this.connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            model.EmpID = reader.GetInt32(0);
                            model.EmpName = reader.GetString(1);
                            model.BasicPay = reader.GetDouble(2);
                            model.StartDate = reader.GetDateTime(3);
                            model.Gender = reader.GetString(4);
                            model.PhnNo = reader.GetString(5);
                            model.Department = reader.GetString(6);
                            model.Address = reader.GetString(7);
                            model.Deductions = reader.GetDecimal(8);
                            model.TaxablePay = reader.GetDecimal(9);
                            model.IncomeTax = reader.GetDecimal(10);
                            model.NetPay = reader.GetDecimal(11);
                            Console.WriteLine("{0},{1}", model.EmpID,model.EmpName);
                            return model.BasicPay;
                        }
                    }
                    else
                        Console.WriteLine("No data found");
                    return 0;
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
        /// <summary>
        /// UC 2:
        /// Adds the employee.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool AddEmployee(EmployeeModel model)
        {
            try
            {
                using (this.connection)
                {
                    SqlCommand command = new SqlCommand("dbo.SpAddEmployeeDetails", this.connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmpName", model.EmpName);
                    command.Parameters.AddWithValue("@BasicPay", model.BasicPay);
                    command.Parameters.AddWithValue("@StartDate", model.StartDate);
                    command.Parameters.AddWithValue("@Gender", model.Gender);
                    command.Parameters.AddWithValue("@PhoneNo", model.PhnNo);
                    command.Parameters.AddWithValue("@Department", model.Department);
                    command.Parameters.AddWithValue("@Address", model.Address);
                    command.Parameters.AddWithValue("@Deductions", model.Deductions);
                    command.Parameters.AddWithValue("@TaxablePay", model.TaxablePay);
                    command.Parameters.AddWithValue("@IncomeTax", model.IncomeTax);
                    command.Parameters.AddWithValue("@NetPay", model.NetPay);
                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        
        //UC3
        public bool UpdateSalary(string empName)
        {
            try
            {
                using (connection)
                {
                    connection.Open();
                    string query = @"update dbo.employee_payroll set BasicPay=30000 where EmpName=@parameter";
                    SqlCommand command = new SqlCommand(query, this.connection);
                    command.Parameters.AddWithValue("@parameter", empName);
                    var result = command.ExecuteNonQuery();
                    this.connection.Close();
                    if (result != 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }


        /// <summary>
        /// UC 4:
        /// Updates the salary in data base.
        /// </summary>
        /// <param name="model">The employee model.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool UpdateSalary(EmployeeModel model)
        {
            try
            {
                using (this.connection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spUpdateSalary", this.connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@id", model.EmpID);
                    sqlCommand.Parameters.AddWithValue("@salary", model.BasicPay);
                    sqlCommand.Parameters.AddWithValue("@name", model.EmpName);
                    connection.Open();
                    int result = sqlCommand.ExecuteNonQuery();
                    connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Reads the updated salary.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">no data found</exception>
        public double ReadSalary()
        {
            public static string connectionString = @"Server=NIKHIL-ACER\SQLEXPRESS; Initial Catalog =payroll_service; User ID =nikhil; Password=kumar";
            SqlConnection connection = new SqlConnection(connectionString1);
            double salary;
            EmployeeModel model = new EmployeeModel();
            SqlCommand command = new SqlCommand("Select * from employee_payroll", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                salary = model.BasicPay;
            }
            else
            {
                throw new Exception("no data found");
            }
            reader.Close();
            connection.Close();
            return salary;
        }
        
        
        /// <summary>
        /// UC 5:
        /// Gets all employees from database with given date range.
        /// </summary>
        /// <exception cref="System.Exception"></exception>
        public void GetAllEmployeesFromDate()
        {
            EmployeeModel model = new EmployeeModel();
            try
            {
                using (this.connection)
                {
                    string query = @"select * from employee_payroll where StartDate between cast('2020-01-01' as date) and cast(getdate() as date)";
                    SqlCommand command = new SqlCommand(query, this.connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            model.EmpID = reader.GetInt32(0);
                            model.EmpName = reader.GetString(1);
                            model.BasicPay = reader.GetDouble(2);
                            model.StartDate = reader.GetDateTime(3);
                            model.Gender = reader.GetString(4);
                            model.PhnNo = reader.GetString(5);
                            model.Department = reader.GetString(6);
                            model.Address = reader.GetString(7);
                            model.Deductions = reader.GetDecimal(8);
                            model.TaxablePay = reader.GetDecimal(9);
                            model.IncomeTax = reader.GetDecimal(10);
                            model.NetPay = reader.GetDecimal(11);
                            Console.WriteLine("{0},{1}", model.EmpID, model.EmpName);
                        }
                    }
                    else
                        Console.WriteLine("No data found");
                }
            }
            catch (Exception ex)
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
