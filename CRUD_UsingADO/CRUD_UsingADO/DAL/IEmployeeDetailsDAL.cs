using CRUD_UsingADO.Models;
using System.Collections.Generic;

namespace CRUD_UsingADO.DAL
{
    public interface IEmployeeDetailsDAL<Employee>
    {
        bool AddEmployeeDetails(Employee empDetails);

        bool DeleteEmployeeDetails(int empCode);

        bool UpdateEmployeeDetails(Employee empDetails);

        Employee GetEmployeeDetailsByCode(int empCode);
        List<Employee> GetAllEmployee();
    }
}
