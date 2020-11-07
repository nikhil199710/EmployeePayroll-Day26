using System;

namespace ADO.NetDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            EmployeeRepository repository = new EmployeeRepository();
            //repository.EnsureDataBaseConnection();
            repository.GetAllEmployees();



            //repository.UpdateSalary("Terisa");
            //repository.GetAllEmployees();
            //repository.UpdateEmployeeUsingStoredProcedure("Bill", 3000);
            //repository.GetAllEmployeesBeforeToday(Convert.ToDateTime("01-01-2020"));
            repository.GetTheDetailOfSalaryForPassedGender("F");
        }
    }
}