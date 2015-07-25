using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsIndia.Controllers
{
    public class ErrorController : Controller
    {
        //Code Error
        public ActionResult Index()
        {
            return View();
        }

        //Page Not Found Error
        public ViewResult NotFound()
        {

            Response.StatusCode = 404; 
            return View();
        }
    }
}