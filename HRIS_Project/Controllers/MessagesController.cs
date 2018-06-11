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
    public class MessagesController : Controller
    {
        private HumanResourceEntities db = new HumanResourceEntities();

        // GET: Messages
        public ActionResult Index(int? id, string mode)
        {
            if (@Session["UserID"] == null)
            {
                return RedirectToAction("../Login/Index");
            }
            else
            {
                MessagesCl model = new MessagesCl();
                HumanResourceEntities dbcon = new HumanResourceEntities();

                if (id == null)
                {
                    id = 0;
                    model.idMessage = 0;
                    model.SelectedMessage = null;
                }

                else
                {
                    model.idMessage = (int)id;
                    model.SelectedMessage = dbcon.Messages.Find(id);
                    model.DisplayMode = mode;
                }

                model.DateSend = @DateTime.Now.ToString("MM/dd/yyyy"); 
                model.MessageList = dbcon.Messages.OrderBy(m => m.DateSend).ToList();
                int selectedId = (int)Session["UserID"];
                //model.NameSendBy = new SelectList(db.Recruitments, "idUser", "Name"+" "+"Paternal", selectedId);
                //ViewBag.idSendBy = new SelectList(db.Recruitments, "idUser_", "Name", selectedId); 
                //ViewBag.idSendTo = new SelectList(db.Employees, "IdEmployee", "Name", selectedId);

                ViewBag.idSendBy = new SelectList((from s in db.Recruitments.OrderBy(x => x.Name) select new { ID = s.idUser_, FullName = s.Name + " " + s.Paternal }), "ID", "FullName", selectedId);

                ViewBag.idEmployee = new SelectList((from s in db.Employees.OrderBy(x => x.Name) select new { ID = s.IdEmployee, FullName = s.Name + " " + s.LastName }), "ID", "FullName", 0);

                return View(model);
            }
        }

        public ActionResult Delete(int? id)
        {
            HumanResourceEntities dbcon = new HumanResourceEntities();
            Message idMessage = dbcon.Messages.Find(id);

            dbcon.Messages.Remove(idMessage);
            dbcon.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Messages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // GET: Messages/Create
        public ActionResult Create()
        {
            ViewBag.IdEmployee = new SelectList(db.Employees, "IdEmployee", "Name");
            ViewBag.IdSendBy = new SelectList(db.Recruitments, "IdSendBy", "Name");
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idMessage,Subject,Message1,IdSendBy,IdEmployee,DateSend")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Messages.Add(message);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdEmployee = new SelectList(db.Employees, "IdEmployee", "Name", message.IdEmployee);
            ViewBag.IdSendBy = new SelectList(db.Recruitments, "idUser_", "Name", message.IdSendBy);
            return View(message);
        }

        // GET: Messages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdEmployee = new SelectList(db.Employees, "IdEmployee", "Name", message.IdEmployee);
            ViewBag.IdSendBy = new SelectList(db.Recruitments, "idUser_", "Name", message.IdSendBy);
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idMessage,Subject,Message1,IdSendBy,IdEmployee,DateSend")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdEmployee = new SelectList(db.Employees, "IdEmployee", "Name", message.IdEmployee);
            ViewBag.IdSendBy = new SelectList(db.Recruitments, "idUser_", "Name", message.IdSendBy);
            return View(message);
        }

        

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Message message = db.Messages.Find(id);
            db.Messages.Remove(message);
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
