using System;
using System.Collections.Generic;
using System.Text;

namespace ADO.net
{
    public class EmployeeModel
    {
        public int EmpID { get; set; }
        public string EmpName { get; set; }
        public double BasicPay { get; set; }
        public DateTime StartDate { get; set; }
        public string Gender { get; set; }
        public string PhnNo { get; set; }
        public string Department { get; set; }
        public string Address { get; set; }
        public decimal Deductions { get; set; }
        public decimal TaxablePay { get; set; }
        public decimal IncomeTax { get; set; }
        public decimal NetPay { get; set; }
    }
}
