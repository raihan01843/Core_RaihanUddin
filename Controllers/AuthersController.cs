using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreProject_Raihan.Data;
using CoreProject_Raihan.Models;
using CoreProject_Raihan.ViewModels;
using CoreProject_Raihan.PagedList;

namespace CoreProject_Raihan.Controllers
{
    public class AuthersController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IHostingEnvironment webHostEnvironment;
        public AuthersController(ApplicationDbContext context, IHostingEnvironment hostEnvironment)
        {
            db = context;
            webHostEnvironment = hostEnvironment;
        }

        //public async Task<IActionResult> Index()
        //{
        //    return View(await db.Authers.ToListAsync());
        //}

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            var authers = from s in db.Authers
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                authers = authers.Where(s => s.AutherName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    authers = authers.OrderByDescending(s => s.AutherName);
                    break;
                case "Date":
                    authers = authers.OrderBy(s => s.WrittenDate);
                    break;
                case "date_desc":
                    authers = authers.OrderByDescending(s => s.WrittenDate);
                    break;
                //default:
                //    authers = authers.OrderBy(s => s.LastName);
                //    break;
            }
            int pageSize = 3;
            return View(await PaginatedList<Auther>.CreateAsync(authers.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auther = await db.Authers
                .FirstOrDefaultAsync(m => m.Id == id);

            var autherViewModel = new AutherViewModel()
            {
                Id = auther.Id,
                AutherName = auther.AutherName,
                Qualification = auther.Qualification,
                Experience = auther.Experience,
                WrittenDate = auther.WrittenDate,
                PublishDate = auther.PublishDate,
                Venue = auther.Venue,
                ExistingImage = auther.ProfilePicture
            };

            if (auther == null)
            {
                return NotFound();
            }

            return View(auther);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AutherViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);
                Auther auther = new Auther
                {
                    AutherName = model.AutherName,
                    Qualification = model.Qualification,
                    Experience = model.Experience,
                    WrittenDate = model.WrittenDate,
                    PublishDate = model.PublishDate,
                    Venue = model.Venue,
                    ProfilePicture = uniqueFileName
                };

                db.Add(auther);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auther = await db.Authers.FindAsync(id);
            var autherViewModel = new AutherViewModel()
            {
                Id = auther.Id,
                AutherName = auther.AutherName,
                Qualification = auther.Qualification,
                Experience = auther.Experience,
                WrittenDate = auther.WrittenDate,
                PublishDate = auther.PublishDate,
                Venue = auther.Venue,
                ExistingImage = auther.ProfilePicture
            };

            if (auther == null)
            {
                return NotFound();
            }
            return View(autherViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AutherViewModel model)
        {
            if (ModelState.IsValid)
            {
                var auther = await db.Authers.FindAsync(model.Id);
                auther.AutherName = model.AutherName;
                auther.Qualification = model.Qualification;
                auther.Experience = model.Experience;
                auther.WrittenDate = model.WrittenDate;
                auther.PublishDate = model.PublishDate;
                auther.Venue = model.Venue;

                if (model.AutherPicture != null)
                {
                    if (model.ExistingImage != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "Uploads", model.ExistingImage);
                        System.IO.File.Delete(filePath);
                    }

                    auther.ProfilePicture = ProcessUploadedFile(model);
                }
                db.Update(auther);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auther = await db.Authers
                .FirstOrDefaultAsync(m => m.Id == id);

            var autherViewModel = new AutherViewModel()
            {
                Id = auther.Id,
                AutherName = auther.AutherName,
                Qualification = auther.Qualification,
                Experience = auther.Experience,
                WrittenDate = auther.WrittenDate,
                PublishDate = auther.PublishDate,
                Venue = auther.Venue,
                ExistingImage = auther.ProfilePicture
            };
            if (auther == null)
            {
                return NotFound();
            }

            return View(autherViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var auther = await db.Authers.FindAsync(id);
            var CurrentImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", auther.ProfilePicture);
            db.Authers.Remove(auther);
            if (await db.SaveChangesAsync() > 0)
            {
                if (System.IO.File.Exists(CurrentImage))
                {
                    System.IO.File.Delete(CurrentImage);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool SpeakerExists(int id)
        {
            return db.Authers.Any(e => e.Id == id);
        }

        private string ProcessUploadedFile(AutherViewModel model)
        {
            string uniqueFileName = null;

            if (model.AutherPicture != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Uploads");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.AutherPicture.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.AutherPicture.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
