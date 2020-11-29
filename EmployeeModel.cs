// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmployeeModel.cs" company="Capgemini">
//   Copyright © 2018 Company
// </copyright>
// <creator Name="Nikhil Kumar Yadav"/>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;

namespace ADO.net
{
    class EmployeeModel
    {
        public int EmpID { get; set; }
        public string EmpName { get; set; }
        public string PhnNo { get; set; }
        public string Address { get; set; }
        public string Department { get; set; }
        public char Gender { get; set; }
        public float BasicPay { get; set; }
        public float Deductions { get; set; }
        public float TaxablePay { get; set; }
        public float IncomeTax { get; set; }
        public float NetPay { get; set; }
        public DateTime StartDate { get; set; }
    }
}
