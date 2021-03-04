using CoreProject_Raihan.Attirbutes.ValidationAttributes;
using CoreProject_Raihan.ValidationAttributes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreProject_Raihan.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="This Field Must Be Fill!!!!")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Enter Minimum 2 or maximum 40 chacacter !!")]
        [Display(Name = "Student Name", Description = "Name of the Student")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This Field Must Be Fill!!!!")]
        public string Address { get; set; }
        [Required(ErrorMessage = "This Field Must Be Fill!!!!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "This Field Must Be Fill!!!!")]
        [MyAttribute]
        public string PhoneNo { get; set; }
        [Required(ErrorMessage = "This Field Must Be Fill!!!!")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: yyyy-MM-dd}")]
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        [TodayAttribute]
        public DateTime DOB { get; set; }
        [Required(ErrorMessage = "This Field Must Be Fill!!!!")]
        [Range(6, 60, ErrorMessage = "Age Must be between 6 to 60")]
        public int Age { get; set; }
        public string UrlImage { get; set; }

        [NotMapped]
        public IFormFile ImageUrl { get; set; }
    }
}
