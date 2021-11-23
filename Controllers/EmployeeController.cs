using AnReshWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AnReshWebApp.Controllers
{
    public class EmployeeController : Controller
    {
        public EmployeeRepository employeeRepository = new EmployeeRepository();
        public DepartmentRepository departmentRepository = new DepartmentRepository();

        public async Task<JsonResult> SendData()
        {
            var employeeList = await employeeRepository.GetAllAsync();
            return Json(employeeList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EmployeeForm()
        {
            return View("_Layout");
        }
        public async Task<HttpStatusCodeResult> Delete(int id)
        {
            try
            {
                await employeeRepository.DeleteAsync(id);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }

        public async Task<HttpStatusCodeResult> Edit(Employee employee)
        {
            try
            {
                await employeeRepository.UpdateAsync(employee);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
           
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }

        public async Task<HttpStatusCodeResult> Create(Employee employee)
        {
            try
            {
                await employeeRepository.AddAsync(employee);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
            
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }
    }
}