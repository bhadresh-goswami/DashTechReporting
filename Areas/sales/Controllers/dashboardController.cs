﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTRS.Areas.sales.Controllers
{
    public class dashboardController : Controller
    {
        // GET: sales/dashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}