using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AnReshWebApp.Models;

namespace AnReshWebApp.Controllers
{
    public class DepartmentController : Controller
    {
        public DepartmentRepository repository = new DepartmentRepository();

        public async Task<ActionResult> DepartmentForm()
        {
            return View(await repository.GetAllAsync());
        }

        public async Task<ActionResult> Delete(int id)
        {
            await repository.DeleteAsync(id);
            return RedirectToAction("DepartmentForm");
        }

        public async Task<ActionResult> Edit(Department department)
        {
            await repository.UpdateAsync(department);
            return RedirectToAction("DepartmentForm");
        }

        public ActionResult EditPage(Department department)
        {
            return View(department);
        }

        public async Task<ActionResult> Create(Department department)
        {
            await repository.AddAsync(department);
            return RedirectToAction("DepartmentForm");
        }

        public ActionResult CreationPage() 
        {
            return View();
        }
    }
}