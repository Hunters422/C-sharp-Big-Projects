using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TechAcadamyStudentMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact Page - The Tech Academy";

            return View();
        }
        public ActionResult Instructor(int id)
        {
            ViewBag.Id = id;

            Instructor dayTimeInstructor = new Instructor
            {
                id = 1,
                FirstName = "Hunter",
                LastName = "Janko"
            };
            return View(dayTimeInstructor);
        }
        public ActionResult Instructors()
        {
            return View();
        }
        

    }
}