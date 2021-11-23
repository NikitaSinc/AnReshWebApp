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
        public async Task<JsonResult> SendData()
        {
            var departmentsList = await repository.GetAllAsync();

            return Json(departmentsList,JsonRequestBehavior.AllowGet);
        }
       
        public ActionResult DepartmentForm()
        {
            return View("_Layout");
        }

        public async Task<HttpStatusCodeResult> Delete(int id)
        {
            try
            {
                await repository.DeleteAsync(id);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }

        public async Task<HttpStatusCodeResult> Edit(Department department)
        {
            try
            {
                await repository.UpdateAsync(department);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }
        [HttpPost]
        public async Task<HttpStatusCodeResult> Create(Department department)
        {
            try
            {
                await repository.AddAsync(department);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }
    }
}