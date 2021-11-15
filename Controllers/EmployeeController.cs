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
        public async Task<ActionResult> Delete(int id)
        {
            await employeeRepository.DeleteAsync(id);
            return RedirectToAction("EmployeeForm");
        }

        public async Task<ActionResult> Edit(int id, string full_name, int departmentId, int salary)
        {
            Employee employee = new Employee();
            employee.Id = id; employee.Full_name = full_name; employee.Salary = salary; employee.Id_department = departmentId;
            await employeeRepository.UpdateAsync(employee);
            return RedirectToAction("EmployeeForm");
        }

        public async Task<ActionResult> Create(string full_name, int departmentId, int salary)
        {
            Employee employee = new Employee();
            employee.Full_name = full_name; employee.Salary = salary; employee.Id_department = departmentId;
            await employeeRepository.AddAsync(employee);
            return RedirectToAction("EmployeeForm");
        }
    }
}