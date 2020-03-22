using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarInsurance.Models;

namespace CarInsurance.Controllers
{
    public class InsureeController : Controller
    {
        private InsuranceEntities db = new InsuranceEntities();

        // GET: Insuree
        public ActionResult Index()
        {
            return View(db.Insurees.ToList());
        }

        // GET: Insuree/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // GET: Insuree/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Insuree/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
        {
            if (string.IsNullOrEmpty(insuree.FirstName) || string.IsNullOrEmpty(insuree.LastName) || string.IsNullOrEmpty(insuree.EmailAddress) || string.IsNullOrEmpty(insuree.DateOfBirth.ToString()) ||
                string.IsNullOrEmpty(insuree.CarYear.ToString()) || string.IsNullOrEmpty(insuree.CarMake) || string.IsNullOrEmpty(insuree.CarModel) || string.IsNullOrEmpty(insuree.SpeedingTickets.ToString()))
            {
                return View("~/Views/Shared/Error.cshtml");

            }
            else
            {
                int age = DateTime.Now.Year - insuree.DateOfBirth.Year;
                insuree.Quote = 50.00m;

                //age

                if (age < 18)
                {
                    insuree.Quote = insuree.Quote + 100.00m;

                }
                else if (age < 25 || age > 100)
                {
                    insuree.Quote = insuree.Quote + 25.00m;

                }

                //year
                if (insuree.CarYear < 2000 || insuree.CarYear > 2015)
                {
                    insuree.Quote = insuree.Quote + 25.00m;
                }
                //porsche 
                if (insuree.CarMake.ToLower() == "porsche")
                {
                    insuree.Quote = insuree.Quote + 25.00m;
                    if (insuree.CarModel.ToLower() == "911 carrera")
                    {
                        insuree.Quote = insuree.Quote + 25.00m;
                    }
                }

                //tickets

                int numTickets = Convert.ToInt32(insuree.SpeedingTickets);
                if (numTickets < 0)
                {
                    return View("~/Views/Shared/Error.cshtml");
                }
                for (int i = 0; i < numTickets; i++)
                {
                    insuree.Quote = insuree.Quote + 10.00m;
                }

                //dui

                if (insuree.DUI == true)
                {
                    insuree.Quote = insuree.Quote + (insuree.Quote * .25m);
                }

                //coveragetype

                if (insuree.FullCoverage == true)
                {
                    insuree.Quote = insuree.Quote + (insuree.Quote * .50m);
                }


                db.Insurees.Add(insuree);
                db.SaveChanges();
                return RedirectToAction("Index");

            }

        }

        // GET: Insuree/Edit/5
         public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // POST: Insuree/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                db.Entry(insuree).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(insuree);
        }

        // GET: Insuree/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // POST: Insuree/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Insuree insuree = db.Insurees.Find(id);
            db.Insurees.Remove(insuree);
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
