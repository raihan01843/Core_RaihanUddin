using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProject_Raihan.Models
{
    public class Course
    {
        [Key]
        public int CourseID { get; set; }


        [Display(Name = "Course Name")]
        [Required(ErrorMessage = "Must Be Filled")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Name must be 3-30 char")]
        public string CourseName { get; set; }

        [Display(Name = "Course Duration")]
        [Required(ErrorMessage = "Must Be Filled")]
        public string CourseDuration { get; set; }

       
        public virtual ICollection<Trainee> Trainees { get; set; }
    }
}
