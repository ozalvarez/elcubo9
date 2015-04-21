using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace elcubo9.web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Route("Intern")]
        public ActionResult Intern()
        {
            return View();
        }
        [Route("AuthComplete")]
        public ActionResult AuthComplete()
        {
            return View();
        }
    }
}