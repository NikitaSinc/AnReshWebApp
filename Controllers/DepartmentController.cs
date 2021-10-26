using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AnReshWebApp.Cors;
using AnReshWebApp.Models;

namespace AnReshWebApp.Controllers
{
    [AllowCrossSiteJson]
    public class DepartmentController : Controller
    {

        public DepartmentRepository repository = new DepartmentRepository();
        public async Task<JsonResult> SendData()
        {
            var departmentsList = await repository.GetAllAsync();

            return Json(departmentsList,JsonRequestBehavior.AllowGet);
        }
       
        public ActionResult DepartmentForm()
        {
            return View();
        }

        public async Task<ActionResult> Delete(int id)
        {
            await repository.DeleteAsync(id);
            return RedirectToAction("DepartmentForm");
        }

        public async Task<ActionResult> Edit(int id, string name)
        {
            Department department = new Department();
            department.Id = id; department.Name = name;
            await repository.UpdateAsync(department);
            return RedirectToAction("DepartmentForm");
        }

        public async Task<ActionResult> Create(string name)
        {
            Department department = new Department();
            department.Name = name;
            await repository.AddAsync(department);
            return RedirectToAction("DepartmentForm");
        }
    }
}