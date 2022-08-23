using CRUD_UsingADO.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CRUD_UsingADO.DAL
{
    public class EmployeeDetailsDAL : IEmployeeDetailsDAL<Employee>
    {
        private readonly IConfiguration _configuration;
        SqlConnection SqlCon;
        SqlCommand cmd = new SqlCommand();
        DataSet dataSet =new DataSet();
        SqlDataAdapter sqlDataAdapter =new SqlDataAdapter();

        public EmployeeDetailsDAL(IConfiguration configuration)
        {
            _configuration = configuration;
            SqlCon = new SqlConnection(_configuration["ConnectionStrings:EmpCon"]);
        }
        public bool AddEmployeeDetails(Employee empDetails)
        {
            cmd.Connection = SqlCon;
            cmd.CommandText = "AddDetails";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@EmpCode", System.Data.SqlDbType.Int).Value = empDetails.EmpCode;
            cmd.Parameters.Add("@DeptCode", System.Data.SqlDbType.Int).Value = empDetails.DeptCode;
            cmd.Parameters.Add("@EmpName",SqlDbType.NVarChar,50).Value=empDetails.EmpName;
            cmd.Parameters.Add("@DateOfBirth", SqlDbType.DateTime).Value = empDetails.DateOfBirth;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = empDetails.Email;
            sqlDataAdapter.SelectCommand = cmd;
            dataSet.Reset();
          
          //  sqlDataAdapter.TableMappings.Add("EmpProfile","EmpProfile");//"TableNameInDb", "TableNameInTheDataSet"
            var result=  sqlDataAdapter.Fill(dataSet, "EmpProfile");
            //var result=sqlDataAdapter.Update(dataSet, "EmpProfile");
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public bool DeleteEmployeeDetails(int empCode)
        {
            try
            {
                cmd.Connection = SqlCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DeleteEmployee";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@EmpCode",SqlDbType.Int).Value=empCode;
                if (SqlCon.State == System.Data.ConnectionState.Closed)
                {
                    SqlCon.Open();
                }
                var rows = cmd.ExecuteNonQuery();
                SqlCon.Close();
                if (rows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
            
        }

        public List<Employee> GetAllEmployee()
        {
            List<Employee> EmployeeList = new List<Employee>();
            try
            {
                cmd.Connection = SqlCon;
                cmd.CommandText = "GetAllDetails";
                cmd.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand = cmd;
                cmd.Parameters.Clear();
               



                dataSet.Reset();
                sqlDataAdapter.Fill(dataSet, "EmpProfile");
                if (dataSet.Tables["EmpProfile"].Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataSet.Tables["EmpProfile"].Rows)
                    {
                        Employee employee = new Employee();
                        employee.EmpCode = Convert.ToInt32(dataRow["EmpCode"]);
                        employee.DeptCode = Convert.ToInt32(dataRow["DeptCode"]);
                        employee.EmpName = Convert.ToString(dataRow["EmpName"]);
                        
                        employee.DateOfBirth = Convert.ToDateTime(dataRow["DateOfBirth"]);
                        employee.Email = Convert.ToString(dataRow["Email"]);

                        EmployeeList.Add(employee);
                    }
                    return EmployeeList;
                }
                else
                {
                    EmployeeList = null;
                }


            }
            catch (Exception e)
            {
                Employee employee = new Employee();
                employee.EmpCode = 1;
                employee.EmpName = "Exception";

                employee.DateOfBirth = DateTime.Parse("12-12-2000");
                employee.Email = e.Message;

                EmployeeList.Add(employee);
                return EmployeeList;
            }
            return EmployeeList;
        }

        public Employee GetEmployeeDetailsByCode(int empCode)
        {
           Employee employee = new Employee();
            try
            {
                cmd.Connection = SqlCon;
                cmd.CommandText = "GetByCode";
                cmd.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand = cmd;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@EmpCode", SqlDbType.Int).Value = empCode;
                dataSet.Reset();
                sqlDataAdapter.Fill(dataSet, "EmpProfile");
                if (dataSet.Tables["EmpProfile"].Rows.Count > 0)
                {
                   
                       
                        employee.EmpCode = Convert.ToInt32(dataSet.Tables["EmpProfile"].Rows[0][0]);
                        employee.EmpName = Convert.ToString(dataSet.Tables["EmpProfile"].Rows[0][1]);

                        employee.DateOfBirth = Convert.ToDateTime(dataSet.Tables["EmpProfile"].Rows[0][2]);
                        employee.Email = Convert.ToString(dataSet.Tables["EmpProfile"].Rows[0][3]);
                    employee.DeptCode = Convert.ToInt32(dataSet.Tables["EmpProfile"].Rows[0][4]);



                    return employee;
                }
                else
                {
                    employee = null;
                }


            }
            catch (Exception e)
            {
               employee = new Employee();
                employee.EmpCode = 1;
                employee.EmpName = "Exception";

                employee.DateOfBirth = DateTime.Parse("12-12-2000");
                employee.Email = e.Message;

               
                return employee;
            }
            return employee;
        }

      

        public bool UpdateEmployeeDetails(Employee empDetails)
        {
            cmd.Connection = SqlCon;
            cmd.CommandText = "UpdateDetails";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@EmpCode", System.Data.SqlDbType.Int).Value = empDetails.EmpCode;
            cmd.Parameters.Add("@DeptCode", System.Data.SqlDbType.Int).Value = empDetails.DeptCode;
            cmd.Parameters.Add("@EmpName", SqlDbType.NVarChar, 50).Value = empDetails.EmpName;
            cmd.Parameters.Add("@DateOfBirth", SqlDbType.DateTime).Value = empDetails.DateOfBirth;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = empDetails.Email;
            sqlDataAdapter.SelectCommand = cmd;
            //dataSet.Reset();

            //sqlDataAdapter.TableMappings.Add("EmpProfile", "EmpProfile");//"TableNameInDb", "TableNameInTheDataSet"
            //var result = sqlDataAdapter.Fill(dataSet, "EmpProfile");
            //return true;
            // var result=sqlDataAdapter.Update(dataSet, "EmpProfile");
            if (SqlCon.State == System.Data.ConnectionState.Closed)
            {
                SqlCon.Open();
            }
            var rows = cmd.ExecuteNonQuery();
            SqlCon.Close();
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }




        }
    }
}
