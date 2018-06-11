using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HRIS_Project.Models;

namespace HRIS_Project.Controllers
{
    public class RecruitmentsController : Controller
    {
        private HumanResourceEntities db = new HumanResourceEntities();

        // GET: Recruitments
        public ActionResult Index(int? id, string mode)
        {
            if (@Session["UserID"] == null)
            {
                return RedirectToAction("../Login/Index");
            }
            else
            {
                RecruiterCl model = new RecruiterCl();
                HumanResourceEntities dbcon = new HumanResourceEntities();

                if (id == null)
                {
                    id = 0;
                    model.idUser = 0;
                    model.SelectedRecruiter = null;
                }

                else
                {
                    model.idUser = (int)id;
                    model.SelectedRecruiter = dbcon.Recruitments.Find(id);
                    model.DisplayMode = mode;
                }

                model.DateCreate = @DateTime.Now.ToString("MM/dd/yyyy");
                model.RecruiterList = dbcon.Recruitments.OrderBy(m => m.Name).ToList();

                return View(model);
            }
        }

        public ActionResult Delete(int? id)
        {
            HumanResourceEntities dbcon = new HumanResourceEntities();
            Recruitment idRecruiter = dbcon.Recruitments.Find(id);

            dbcon.Recruitments.Remove(idRecruiter);
            dbcon.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Recruitments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recruitment recruitment = db.Recruitments.Find(id);
            if (recruitment == null)
            {
                return HttpNotFound();
            }
            return View(recruitment);
        }

        // GET: Recruitments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Recruitments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idUser_,Name,Paternal,Email,UserName,Password,DateCreate,isAdmin")] Recruitment recruitment)
        {
            if (ModelState.IsValid)
            {
                db.Recruitments.Add(recruitment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(recruitment);
        }

        // GET: Recruitments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recruitment recruitment = db.Recruitments.Find(id);
            if (recruitment == null)
            {
                return HttpNotFound();
            }
            return View(recruitment);
        }

        // POST: Recruitments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idUser_,Name,Paternal,Email,UserName,Password,DateCreate,isAdmin")] Recruitment recruitment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recruitment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(recruitment);
        }

       

        // POST: Recruitments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recruitment recruitment = db.Recruitments.Find(id);
            db.Recruitments.Remove(recruitment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
