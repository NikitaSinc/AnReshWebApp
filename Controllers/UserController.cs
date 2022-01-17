using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using AnReshWebApp.Models;
using AnReshWebApp.JWT;
using AnReshWebApp.Models.FilterEntity;
using AnReshWebApp.Repositories;

namespace AnReshWebApp.Controllers
{
    public class UserController : Controller
    {
        private JsonWebTokenService JsonWebTokenService = new JsonWebTokenService();
        private GenericRepository<BaseFilterEntity,Users> Repository;

        public UserController(GenericRepository<BaseFilterEntity, Users> repository)
        {
            Repository = repository;
        }

        [HttpPost]
        public async Task<JsonResult> LoginCheck(Users user)
        {
            try
            {
                await Repository.LoginAsync(user);
            }
            catch(Exception)
            {
                return Json(new HttpStatusCodeResult(System.Net.HttpStatusCode.Unauthorized));
            }
            var JWT = JsonWebTokenService.GenerateToken(user.Id, user.Login);
            return Json(new {JWT, user.Login, user.Id}, JsonRequestBehavior.AllowGet);
        }
        public HttpStatusCodeResult JWTCheck()
        {
            return JsonWebTokenService.ValidateToken(HttpContext.Request.Cookies.Get("JWT").Value);
        }
        [HttpGet]
        public JsonResult GetSecret()
        {
            return Json(ConfigurationManager.AppSettings.Get("secretJWT"), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Login()
        {
            return View("_Layout");
        }

        public ActionResult Registration()
        {
            return View("_Layout");
        }

        [HttpPost]
        public async Task<HttpStatusCodeResult> Registrate(Users user)
        {
            try 
            {
                await Repository.AddAsync(user);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.Unauthorized);
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }
    }
}