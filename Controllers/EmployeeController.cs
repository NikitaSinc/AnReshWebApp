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
        public EmployeeRepository repository = new EmployeeRepository();
        public async Task<ActionResult> EmployeeForm()
        {
            return View(await repository.GetAllAsync());
        }
        public async Task<ActionResult> Delete(int id)
        {
            await repository.DeleteAsync(id);
            return RedirectToAction("EmployeeForm");
        }

        public async Task<ActionResult> Edit(Employee employee)
        {
            await repository.UpdateAsync(employee);
            return RedirectToAction("EmployeeForm");
        }

        public ActionResult EditPage(Employee employee)
        {
            return View(employee);
        }

        public async Task<ActionResult> Create(Employee employee)
        {
            await repository.AddAsync(employee);
            return RedirectToAction("EmployeeForm");
        }

        public ActionResult CreationPage()
        {
            return View();
        }
    }
}