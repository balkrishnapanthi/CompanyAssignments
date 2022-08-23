using CRUD_UsingADO.DAL;
using CRUD_UsingADO.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CRUD_UsingADO
{
    
    public class HomeController : Controller
    {
        private readonly IEmployeeDetailsDAL<Employee> employeeDetailsDAL;
        public HomeController(IEmployeeDetailsDAL<Employee> employeeDetailsDAL)
        {
            this.employeeDetailsDAL = employeeDetailsDAL;

        }
       

        Employee employee = new Employee();
        [Route("")] //[Route("")]  and  [Route("/")] are same                  
       // [Route("/")]
        [Route("Home" , Name = "Home")]
        [Route("Index")]
       // [Route("Home/List",Name = "Home")]
        [HttpGet]
       
        public IActionResult EmployeeList()
        {
            List<Employee> employeeList = new List<Employee>();
            try
            {
                employeeList = employeeDetailsDAL.GetAllEmployee();
                if (employeeList != null)
                {

                    return View(employeeList);
                }
                else
                {
                    return View("NoRecordsFound");
                }
            }
            catch(NullReferenceException ex)
            {
                ViewBag.Message = ex.Message;
                return View("ExceptionView");
            }
           
           
           
           
        }
        public IActionResult AddEmployeeDetails()
        {

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEmployeeDetails(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = employeeDetailsDAL.AddEmployeeDetails(employee);

                    return RedirectToAction("EmployeeList");
                }
                return View();
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("ExceptionView");
            }
          
          
        }

        [Route("Delete/{id}", Name ="Delete")]
        public IActionResult DeleteEmployeeDetails(int id)
        {
            try
            {
                var result = employeeDetailsDAL.DeleteEmployeeDetails(id);

                return RedirectToAction("EmployeeList");
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("ExceptionView");
            }
              
           
        }


       [Route("Update/{id}", Name = "Update")]       
        public IActionResult UpdateEmployeeDetails(int id)
        {
            try
            {
                var employee = employeeDetailsDAL.GetEmployeeDetailsByCode(id);

                return View(employee);
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("ExceptionView");
            }
               
          
        }
        [HttpPost]
        [Route("Update", Name = "UpdateEmployee")]
        [ValidateAntiForgeryToken]

        public IActionResult UpdateEmployeeDetails(Employee employee)

        {

            if (ModelState.IsValid)
            {
                var id = employee.EmpCode;
                try
                {
                    var result = employeeDetailsDAL.UpdateEmployeeDetails(employee);
                    return RedirectToAction("EmployeeList");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View("ExceptionView");
                }
            }
            return View("UpdateEmployeeDetails",employee);
                
       
           

        }
       


        [Route("ViewByCode/{id}", Name = "ViewByCode")]
        public IActionResult ViewEmployeeDetails(int id)
        {
            var employee = employeeDetailsDAL.GetEmployeeDetailsByCode(id);
            try
            {
                if (employee != null)
                {

                    if (employee.EmpName == "Exception")
                    {
                        ViewBag.Message = employee.Email;// Email carries exception message
                        return View("ExceptionView");
                    }
                    return View(employee);
                }
                else
                {
                    return View("NoRecordsFound");
                }

            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message; 
                return View("ExceptionView");
            }

          
           

           
        }

    }
}
