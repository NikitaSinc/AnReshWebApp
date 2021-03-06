using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using AnReshWebApp.Models;
using AnReshWebApp.JWT;

namespace AnReshWebApp.Controllers
{
    public class UserController : Controller
    {
        private JsonWebTokenService jsonWebTokenService = new JsonWebTokenService();
        private UsersRepository repository = new UsersRepository();

        [HttpPost]
        public async Task<JsonResult> LoginCheck(Users user)
        {
            try
            {
                await repository.LoginAsync(user);
            }
            catch(Exception)
            {
                return Json(new HttpStatusCodeResult(System.Net.HttpStatusCode.Unauthorized));
            }
            var JWT = jsonWebTokenService.GenerateToken(user.Id, user.Login);
            return Json(new {JWT, user.Login, user.Id}, JsonRequestBehavior.AllowGet);
        }
        public HttpStatusCodeResult JWTCheck()
        {
            return jsonWebTokenService.ValidateToken(HttpContext.Request.Cookies.Get("JWT").Value);
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
                await repository.AddAsync(user);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.Unauthorized);
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }
    }
}