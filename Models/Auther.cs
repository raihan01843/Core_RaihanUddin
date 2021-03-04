using CoreProject_Raihan.Attirbutes.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProject_Raihan.Models
{
    public class Auther
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Name")]
        public string AutherName { get; set; }

        [Required]
        [StringLength(100)]
        public string Qualification { get; set; }

        [Required]
        [StringLength(100)]
        public int Experience { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Written Date")]
        [TodayAttribute]
        public DateTime WrittenDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Publish Date")]
        public DateTime PublishDate { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        [Required]
        [Display(Name = "Image")]
        public string ProfilePicture { get; set; }
    }
}
