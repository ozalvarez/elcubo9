﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace elcubo9.customer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [Route("intern")]
        public ActionResult Intern()
        {
            return View();
        }
        [Route("print")]
        public ActionResult Print()
        {
            return View();
        }
    }
}