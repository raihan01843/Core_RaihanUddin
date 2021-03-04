using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreProject_Raihan.Data;
using CoreProject_Raihan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreProject_Raihan.Controllers
{
    public class TraineeController : Controller
    {
        private ITraineeRepository db;

        private ICourseRepository db2;

        private readonly ApplicationDbContext _context;


        public TraineeController(ITraineeRepository db, ICourseRepository db2, ApplicationDbContext _context)
        {
            this.db = db;
            this.db2 = db2;

            this._context = _context;
        }
        public IActionResult Index()
        {

            //ViewBag.CourseName = db2.GetAll();
            //var applicationDbContext = _context.Courses.Include(t => t.CourseName);
            var applicationDbContext = _context.Courses.ToList();

            return View(db.GetAll());
        }



        // GET:Create
        public IActionResult Create()
        {
            ViewBag.CourseID = db2.GetAll();

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Trainee trainee)
        {
            ViewBag.CourseID=db2.GetAll();
            db.Add(trainee);
            return RedirectToAction("Index");

        }

        public IActionResult Delete(int id)
        {
            db.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var applicationDbContext = _context.Courses.ToList();
            ViewBag.CourseID = db2.GetAll();

            return View(db.GetTrainee(id));
        }

        [HttpPost]
        public IActionResult Edit(Trainee trainee)
        {
            ViewBag.CourseID = db2.GetAll();
            db.Update(trainee);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            return View(db.GetTrainee(id));
        }
    }
}