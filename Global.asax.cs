using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Swashbuckle.AspNetCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.Application;
using System.Web.Http;
using AnReshWebApp.Services;
using Ninject.Modules;
using Ninject.Web.Mvc;
using AnReshWebApp.Util;
using Ninject;

namespace AnReshWebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            NinjectModule registrations = new NinjectRegistrations();
            var kernel = new StandardKernel(registrations);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}
