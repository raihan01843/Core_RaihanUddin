using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreProject_Raihan.Data;
using CoreProject_Raihan.Models;

namespace CoreProject_Raihan.Models
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext db;
        public StudentRepository(ApplicationDbContext db)
        {
            this.db = db;
        }
        public Student Add(Student student)
        {
            db.Students.Add(student);
            db.SaveChanges();
            return student;
        }

        public Student Delete(int Id)
        {
            Student student = db.Students.Find(Id);
            if (student != null)
            {
                db.Students.Remove(student);
                db.SaveChanges();
            }
            return student;
        }

        public IEnumerable<Student> GetAll()
        {
            return db.Students;
        }

        public Student GetStudent(int Id)
        {
            return db.Students.Where(x => x.Id == Id).SingleOrDefault();
        }

        public Student Update(Student student)
        {
            db.Entry(student).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return student;
        }
    }
}
