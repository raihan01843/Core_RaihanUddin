using CoreProject_Raihan.Attirbutes.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProject_Raihan.ViewModels
{
    public class AutherViewModel:EditImageViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string AutherName { get; set; }

        [Required]
        public string Qualification { get; set; }

        [Required]
        public int Experience { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Written Date")]
        public DateTime WrittenDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Publish Date")]
        [TodayAttribute]
        public DateTime PublishDate { get; set; }

        [Required]
        public string Venue { get; set; }
    }
}
