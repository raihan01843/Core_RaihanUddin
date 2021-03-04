using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProject_Raihan.Models
{
    public class Trainee
    {
        [Key]
        public int TraineeID { get; set; }
        public int CourseID { get; set; }

        [Display(Name = "Trainee Name")]
        [Required(ErrorMessage = "Must Be Filled")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Name must be 3-30 char")]
        public string Name { get; set; }

        [Display(Name = "Permanent Address")]
        [Required(ErrorMessage = "Must Be Filled")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Must Be Filled")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Contact Number")]
        [Required(ErrorMessage = "Must Be Filled")]
        public string CellPhone { get; set; }

        public virtual Course Course { get; set; }
    }
}
