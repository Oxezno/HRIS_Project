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
    public class PositionsController : Controller
    {
        private HumanResourceEntities db = new HumanResourceEntities();

        // GET: Positions
        public ActionResult Index(int? id, string mode)
        {
            if (@Session["UserID"] == null)
            {
                return RedirectToAction("../Login/Index");
            }
            else
            {
                PositionsCl model = new PositionsCl();
                HumanResourceEntities dbcon = new HumanResourceEntities();

                if (id == null)
                {
                    id = 0;
                    model.idPosition = 0;
                    model.SelectedPosition = null;
                }

                else
                {
                    model.idPosition = (int)id;
                    model.SelectedPosition = (Position)dbcon.Positions.Find(id);
                    model.DisplayMode = mode;
                }

                model.IdUser = (int)Session["UserID"];
                model.DateCreate = @DateTime.Now.ToString("MM/dd/yyyy");
                model.PositionList = dbcon.Positions.OrderBy(m => m.PositionName).ToList();
                int selectedId = 0;
                model.CompanyName = new SelectList(db.Companies, "idCompany", "CompanyName", selectedId);
                ViewBag.idCompany = new SelectList(db.Companies, "idCompany", "CompanyName", selectedId);

                return View(model);
            }
        }

        public ActionResult Delete(int? id)
        {
            HumanResourceEntities dbcon = new HumanResourceEntities();
            Position idPosition = dbcon.Positions.Find(id);

            dbcon.Positions.Remove(idPosition);
            dbcon.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Positions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Position position = db.Positions.Find(id);
            if (position == null)
            {
                return HttpNotFound();
            }
            return View(position);
        }

        // GET: Positions/Create
        public ActionResult Create()
        {
            ViewBag.idCompany = new SelectList(db.Companies, "idCompany", "CompanyName");
            return View();
        }

        // POST: Positions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idPosition,PositionName,PositionDescription,idCompany,DateCreate,Status,City,State,Country,idUser,IdUser")] Position position)
        {
            if (ModelState.IsValid)
            {
                db.Positions.Add(position);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCompany = new SelectList(db.Companies, "idCompany", "CompanyName", position.idCompany);
            return View(position);
        }

        // GET: Positions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Position position = db.Positions.Find(id);
            if (position == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCompany = new SelectList(db.Companies, "idCompany", "CompanyName", position.idCompany);
            return View(position);
        }

        // POST: Positions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idPosition,PositionName,PositionDescription,idCompany,DateCreate,Status,City,State,Country,idUser")] Position position)
        {
            if (ModelState.IsValid)
            {
                db.Entry(position).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCompany = new SelectList(db.Companies, "idCompany", "CompanyName", position.idCompany);
            return View(position);
        }

       

        // POST: Positions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Position position = db.Positions.Find(id);
            db.Positions.Remove(position);
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
