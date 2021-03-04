using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreProject_Raihan.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace CoreProject_Raihan.Controllers
{
    public class StudentController : Controller
    {
        private IStudentRepository db;
        private readonly IHostingEnvironment appEnvironment;

        public StudentController(IStudentRepository db , IHostingEnvironment appEnvironment)
        {
            this.db = db;
            this.appEnvironment = appEnvironment;
        }
        public IActionResult Index()
        {
            return View(db.GetAll());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                string UrlImage = "";

                var files = HttpContext.Request.Form.Files;
                foreach (var Image in files)
                {
                    if (Image != null && Image.Length > 0)
                    {
                        var file = Image;

                        var uploads = Path.Combine(appEnvironment.WebRootPath, "images");
                        if (file.Length > 0)
                        {
                            // var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                            var fileName = Guid.NewGuid().ToString().Replace("-", "") + file.FileName;
                            using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                                UrlImage = fileName;

                            }

                        }
                    }
                }

                var data = new Student()
                {
                    Name = student.Name,
                    Address = student.Address,
                    Email = student.Email,
                    PhoneNo = student.PhoneNo,
                    DOB = student.DOB,
                    Age = student.Age,
                    UrlImage = UrlImage,
                   
                };

                db.Add(data);
                return RedirectToAction(nameof(Index));

            }

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            db.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            return View(db.GetStudent(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit( int id,Student student)
        {
            if (ModelState.IsValid)
            {
                string UrlImage = "";
                var files = HttpContext.Request.Form.Files;
                foreach (var Image in files)
                {
                    if (Image != null && Image.Length > 0)
                    {
                        var file = Image;

                        var uploads = Path.Combine(appEnvironment.WebRootPath, "images");
                        if (file.Length > 0)
                        {
                            // var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                            var fileName = Guid.NewGuid().ToString().Replace("-", "") + file.FileName;
                            using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                                UrlImage = fileName;
                            }

                        }
                    }
                }
                var data = db.GetStudent(id);
                data.Name = student.Name;
                data.Address = student.Address;
                data.Email = student.Email;
                data.PhoneNo = student.PhoneNo;
                data.DOB = student.DOB;
                data.Age = student.Age;
                data.UrlImage = UrlImage;

                db.Update(data);
                return RedirectToAction(nameof(Index));

            }
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            return View(db.GetStudent(id));
        }
    }
}