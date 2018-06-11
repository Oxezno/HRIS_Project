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
    public class CompaniesController : Controller
    {
        private HumanResourceEntities db = new HumanResourceEntities();

        // GET: Companies
        public ActionResult Index(int? id, string mode)
        {
            if(@Session["UserID"] == null)
            {
                return RedirectToAction("../Login/Index");
            }
            else
            {
                CompaniesCl model = new CompaniesCl();
                HumanResourceEntities dbcon = new HumanResourceEntities();

                if (id == null)
                {
                    id = 0;
                    model.idCompany = null;
                    model.SelectedCompany = null;
                }

                else
                {
                    model.idCompany = id.ToString();
                    model.SelectedCompany = dbcon.Companies.Find(id);
                    model.DisplayMode = mode;
                }

                model.CompaniesList = dbcon.Companies.OrderBy(m => m.CompanyName).ToList();

                return View(model);
            }
        }

        public ActionResult Delete(int? id)
        {
            HumanResourceEntities dbcon = new HumanResourceEntities();
            Company idCompany = dbcon.Companies.Find(id);

            dbcon.Companies.Remove(idCompany);
            dbcon.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Companies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // GET: Companies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCompany,CompanyName,CompanyAddress,City,State,ZipCode,Country,Phone,idDepartment,idPosition")] Company company)
        {
            if (ModelState.IsValid)
            {
                db.Companies.Add(company);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(company);
        }

        // GET: Companies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCompany,CompanyName,CompanyAddress,City,State,ZipCode,Country,Phone,idDepartment,idPosition")] Company company)
        {
            if (ModelState.IsValid)
            {
                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Company company = db.Companies.Find(id);
            db.Companies.Remove(company);
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
