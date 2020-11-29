using System;

namespace ADO.net
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            EmployeeRepository employeeRepository = new EmployeeRepository();
            employeeRepository.GetAllEmployees();
            EmployeeModel model = new EmployeeModel();
            model.EmpName = "Riya";
            model.BasicPay = 75000;
            model.StartDate = DateTime.Now;
            model.Gender = "F";
            model.PhnNo = "7852610130";
            model.Department = "IT";
            model.Address = "Lucknow";
            model.Deductions = 4540;
            model.TaxablePay = 3204;
            model.IncomeTax = 4500;
            model.NetPay = 52000;
            Console.WriteLine(employeeRepository.AddEmployee(model) ? "Record inserted successfully " : "Failed");
        }
    }
}
