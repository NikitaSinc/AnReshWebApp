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
        public async Task<ActionResult> EmployeeForm()
        {
            return View(await employeeRepository.GetAllAsync());
        }
        public async Task<ActionResult> Delete(int id)
        {
            await employeeRepository.DeleteAsync(id);
            return RedirectToAction("EmployeeForm");
        }

        public async Task<ActionResult> Edit(Employee employee)
        {
            employee.Id_department = await departmentRepository.GetIdByNameAsync(employee.Department.Name);
            await employeeRepository.UpdateAsync(employee);
            return RedirectToAction("EmployeeForm");
        }

        public async Task<ActionResult> EditPage(int id)
        {
            ViewBag.Departments = await departmentRepository.GetAllNamesAsync();
            return View(await employeeRepository.GetByIdAsync(id));
        }

        public async Task<ActionResult> Create(Employee employee)
        {
            employee.Id_department = await departmentRepository.GetIdByNameAsync(employee.Department.Name);
            await employeeRepository.AddAsync(employee);
            return RedirectToAction("EmployeeForm");
        }

        public async Task<ActionResult> CreationPage()
        {
            ViewBag.Departments = await departmentRepository.GetAllNamesAsync();
            return View();
        }
    }
}