using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProject_Raihan.Models
{
    public interface IStudentRepository
    {
        Student GetStudent(int Id);
        IEnumerable<Student> GetAll();

        Student Add(Student student);
        Student Update(Student student);
        Student Delete(int Id);
    }
}
