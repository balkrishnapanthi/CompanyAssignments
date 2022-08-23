using System;
using System.ComponentModel.DataAnnotations;

namespace CRUD_UsingADO.Models
{
    public class Employee
    {
        [Key]
        public int EmpCode { get; set; }
        public int DeptCode { get; set; }
        public string EmpName { get; set; }
        [ValidateAge]
        public DateTime? DateOfBirth{ get; set; }
       
        public string Email { get; set; }

    }
    public class UEmployee
    {
        [Key]
        public int EmpCode { get; set; }
        public int DeptCode { get; set; }
        public string EmpName { get; set; }

        [ValidateAge]
        public DateTime? DateOfBirth { get; set; }

        public string Email { get; set; }

    }
}
