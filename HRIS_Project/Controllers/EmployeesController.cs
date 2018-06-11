using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HRIS_Project.Models;
using PagedList;

namespace HRIS_Project.Controllers
{
    public class EmployeesController : Controller
    {
        private HumanResourceEntities db = new HumanResourceEntities();

        // GET: Employees
        public ActionResult Index(int? id, string mode, string sortOrder, string searchString, int? page, string currentFilter)
        {
            if (@Session["UserID"] == null)
            {
                return RedirectToAction("../Login/Index");
            }
            else
            {
                EmployeeCl model = new EmployeeCl();
                HumanResourceEntities dbcon = new HumanResourceEntities();

                if (id == null)
                {
                    id = 0;
                    model.IdEmployee = 0;
                    model.SelectedEmployee = null;
                }

                else
                {
                    model.IdEmployee = (int)id;
                    model.SelectedEmployee = (Employee)dbcon.Employees.Find(id);
                    model.DisplayMode = mode;
                }

                model.IdUser = (int)Session["UserID"];
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewBag.LastNameSortParm = String.IsNullOrEmpty(sortOrder) ? "last_desc" : "";
                //ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

                /*if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }*/

                ViewBag.CurrentFilter = searchString;


                var data = from s in db.Employees select s;

                if (!String.IsNullOrEmpty(searchString))
                {
                    data = data.Where(s => s.Name.Contains(searchString) || s.LastName.Contains(searchString) || s.Email.Contains(searchString));
                }

                switch (sortOrder)
                {
                    case "name_desc":
                        data = data.OrderByDescending(s => s.Name);
                        break;
                    /*case "last_desc":
                        data = data.OrderByDescending(s => s.LastName);
                        break;*/
                    default:
                        data = data.OrderBy(s => s.Name);
                        break;
                }

                int pageSize = 3;
                int pageNumber = (page ?? 1);

                model.EmployeeList = data.ToList();
                //model.EmployeeList = dbcon.Employees.OrderBy(m => m.Name).ToList();
                //model.EmployeeList = data.ToPagedList(pageNumber, pageSize).ToList();

                int selectedId = 0;
                model.PositionName = new SelectList(db.Positions, "idPosition", "PositionName", selectedId);

                ViewBag.idPosition = new SelectList(db.Positions, "idPosition", "PositionName", selectedId);

                return View(model);
            }
        }

        public ActionResult Delete(int? id)
        {
            HumanResourceEntities dbcon = new HumanResourceEntities();
            Employee idEmployee = dbcon.Employees.Find(id);

            dbcon.Employees.Remove(idEmployee);
            dbcon.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.idCompany = new SelectList(db.Companies, "idCompany", "CompanyName");
            ViewBag.idUser = new SelectList(db.Recruitments, "idUser_", "Name");
            ViewBag.idPosition = new SelectList(db.Positions, "idPosition", "PositionName");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdEmployee,Name,LastName,Email,Birthday,Sex,Address,City,State,ZipCode,Country,HomePhone,CellPhone,idCompany,idUser,idPosition, IdUser")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCompany = new SelectList(db.Companies, "idCompany", "CompanyName", employee.idCompany);
            ViewBag.idUser = new SelectList(db.Recruitments, "idUser_", "Name", employee.idUser);
            ViewBag.idPosition = new SelectList(db.Positions, "idPosition", "PositionName", employee.idPosition);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCompany = new SelectList(db.Companies, "idCompany", "CompanyName", employee.idCompany);
            ViewBag.idUser = new SelectList(db.Recruitments, "idUser_", "Name", employee.idUser);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdEmployee,Name,LastName,Email,Birthday,Sex,Address,City,State,ZipCode,Country,HomePhone,CellPhone,idCompany,idUser,idPosition")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCompany = new SelectList(db.Companies, "idCompany", "CompanyName", employee.idCompany);
            ViewBag.idUser = new SelectList(db.Recruitments, "idUser_", "Name", employee.idUser);
            return View(employee);
        }

       

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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
