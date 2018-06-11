using HRIS_Project.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRIS_Project.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (@Session["UserID"] == null)
            {
                return RedirectToAction("../Login/Index");
            }
            else
            {
                return View();
            }
        }
    }
}