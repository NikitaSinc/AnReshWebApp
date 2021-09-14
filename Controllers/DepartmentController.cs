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

    }
}