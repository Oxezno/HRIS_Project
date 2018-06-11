using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCEntityLogin.Models;
using HRIS_Project.Models;

namespace MVCEntityLogin.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            //return Content("<script language='javascript' type='text/javascript'>alert('Thanks for Feedback!2222');</script>");
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            
            if (ModelState.IsValid)
            {
                HumanResourceEntities db = new HumanResourceEntities();

                var user = (from r in db.Recruitments
                            where r.UserName == login.UserName && r.Password == login.Password
                            select new
                            {
                                r.idUser_,
                                r.Name,
                                r.Paternal
                            }).ToList();
                if (user.FirstOrDefault() != null)
                {
                    Session["UserName"] = user.FirstOrDefault().Name+" "+user.FirstOrDefault().Paternal;
                    Session["UserID"] = user.FirstOrDefault().idUser_;
                    return RedirectToAction("../Home/Index");
                    //return View(login);
                }
                else
                {
                    //return Content("<script language='javascript' type='text/javascript'>alert('Invalid login credentials');</script>");
                    //return RedirectToAction("../Home/Index");
                    ModelState.AddModelError("", "Invalid login credentials.");
                }
            }
            return View("Index");
        }

        /*public ActionResult WelcomePage()
        {
            return View();
        }*/
    }
}