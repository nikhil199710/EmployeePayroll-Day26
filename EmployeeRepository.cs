using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ADO.NetDemo
{
    public class EmployeeRepository
    {
        public static string connectionString = @"Server=NIKHIL-ACER\SQLEXPRESS; Initial Catalog =payroll_services; User ID =nikhil; Password=kumar";
        SqlConnection connection = new SqlConnection(connectionString);


        ///<summary>
        /// UC1--Checking for the validity of the connection
        /// </summary>
        public void EnsureDataBaseConnection()
        {
            connection.Open();
            using (connection)
            {
                Console.WriteLine("The Connection is created");
            }
            connection.Close();
        }


        ///<summary>
        /// UC2--GetAll Employees
        /// </summary>
        public void GetAllEmployees()
        {
            EmployeeModel model = new EmployeeModel();
            try
            {
                using (connection)
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
                            model.EmployeeName = reader.GetString(1);
                            model.StartDate = reader.GetDateTime(2);
                            model.Gender = reader.GetString(3);
                            model.PhoneNo = reader.GetInt64(4);
                            model.Address = reader.GetString(5);
                            model.Department = reader.GetString(6);
                            model.BasicPay = reader.GetDouble(7);
                            model.Deductions = reader.GetDouble(8);
                            model.TaxablePay = reader.GetDouble(9);
                            model.Tax = reader.GetDouble(10);
                            model.NetPay = reader.GetDouble(11);

                            Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", model.EmpID, model.EmployeeName, model.StartDate, model.Gender, model.PhoneNo, model.Address, model.Department, model.BasicPay, model.Deductions, model.TaxablePay, model.Tax, model.NetPay);
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found");
                    }
                    reader.Close();
                    //this.connection.Close();
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

        ///<summary>
        /// UC3--Update Salary Of Terissa
        /// </summary>
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
        /// UC4 -- Update the employee payroll data record using a stored procedure
        /// </summary>
        /// <param name="name"></param>
        /// <param name="newBasicPay"></param>
        /// <returns></returns>
        public bool UpdateEmployeeUsingStoredProcedure(string name, int newBasicPay)
        {
            try
            {
                /// Using the connection established
                using (this.connection)
                {
                    /// Implementing the stored procedure
                    SqlCommand command = new SqlCommand("dbo.spUpdateSalary", this.connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@salary", newBasicPay);
                    command.Parameters.AddWithValue("@name", name);
                    /// Opening the connection
                    this.connection.Open();
                    var result = command.ExecuteNonQuery();
                    this.connection.Close();
                    /// Return the result of the transaction i.e. the dml operation to update data
                    if (result != 0)
                    {
                        //Console.WriteLine("Success");
                        return true;
                    }
                    return false;
                }
            }
            /// Catching any type of exception generated during the run time
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }

        ///<summary>
        /// UC5--Get All Employees Before Today
        /// </summary>
        public void GetAllEmployeesBeforeToday(DateTime date)
        {
            EmployeeModel model = new EmployeeModel();
            try
            {
                using (connection)
                {
                    string query = @"select * from dbo.employee_payroll where StartDate between CAST(@parameter as date) and CAST(getdate() as date)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@parameter", date);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            model.EmpID = reader.GetInt32(0);
                            model.EmployeeName = reader.GetString(1);
                            model.StartDate = reader.GetDateTime(2);
                            model.Gender = reader.GetString(3);
                            model.PhoneNo = reader.GetInt64(4);
                            model.Address = reader.GetString(5);
                            model.Department = reader.GetString(6);
                            model.BasicPay = reader.GetDouble(7);
                            model.Deductions = reader.GetDouble(8);
                            model.TaxablePay = reader.GetDouble(9);
                            model.Tax = reader.GetDouble(10);
                            model.NetPay = reader.GetDouble(11);

                            Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", model.EmpID, model.EmployeeName, model.StartDate, model.Gender, model.PhoneNo, model.Address, model.Department, model.BasicPay, model.Deductions, model.TaxablePay, model.Tax, model.NetPay);
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found");
                    }
                    reader.Close();
                    //this.connection.Close();
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

        ///<summary>
        /// UC5--Find sum,avg,and other details
        /// </summary>

        public void GetTheDetailOfSalaryForPassedGender(string gender)
        {
            try
            {
                using (connection)
                {
                    connection.Open();
                    string query = @"select Gender,count(BasicPay) as EmpCount,min(BasicPay) as MinSalary,max(BasicPay) as MaxSalary,sum(BasicPay) as SalarySum,avg(BasicPay) as AvgSalary from dbo.employee_payroll where Gender=@parameter group by Gender";
                    SqlCommand command = new SqlCommand(query, this.connection);
                    command.Parameters.AddWithValue("@parameter", gender);
                    SqlDataReader reader = command.ExecuteReader();
                    connection.Close();
                    if (reader.HasRows)
                    {
                        /// Moving to the next record from the table
                        /// Mapping the data to the retrieved data from executing the query on the table
                        while (reader.Read())
                        {
                            int empCount = reader.GetInt32(1);
                            double minSalary = reader.GetDouble(2);
                            double maxSalary = reader.GetDouble(3);
                            double sumOfSalary = reader.GetDouble(4);
                            double avgSalary = reader.GetDouble(5);
                            Console.WriteLine($"Gender:{gender}\nEmployee Count:{empCount}\nMinimum Salary:{minSalary}\nMaximum Salary:{maxSalary}\n" +
                                $"Total Salary for {gender} :{sumOfSalary}\n" + $"Average Salary:{avgSalary}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data found");
                    }
                    reader.Close();
                }
            }
            /// Catching any type of exception generated during the run time
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