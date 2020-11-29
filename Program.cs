using System;

namespace ADO.net
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            EmployeeRepository employeeRepository = new EmployeeRepository();

            ///Instance of the GetAllEmployees method
            employeeRepository.GetAllEmployees();
            EmployeeModel model = new EmployeeModel();

            ///Adding employee deatils into the database
            model.EmpName = "Riya";
            model.BasicPay = 75000;
            model.StartDate = DateTime.Now;
            model.Gender = "F";
            model.PhnNo = "7852149630";
            model.Department = "IT";
            model.Address = "Lucknow";
            model.Deductions = 4540;
            model.TaxablePay = 3204;
            model.IncomeTax = 4500;
            model.NetPay = 52000;
            Console.WriteLine(employeeRepository.AddEmployee(model) ? "Record inserted successfully " : "Failed");

            ///Updating the basic pay with given name and employee id
            model.EmpID = 4;
            model.BasicPay = 2000000;
            model.EmpName = "Nikhil";
            Console.WriteLine(employeeRepository.UpdateSalary(model) ? "Updated successfully" : "Failed to update");
        
            employeeRepository.GetAllEmployeesFromDate();
        
        }
    }
}
