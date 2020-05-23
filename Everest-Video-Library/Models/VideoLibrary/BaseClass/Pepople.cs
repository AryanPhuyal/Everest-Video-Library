using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Everest_Video_Library.Models.VideoLibrary.BaseClass
{
    public class People
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "First Name Length can't be more then 50")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Last Name Length can't be more then 50")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Email is not Valid")]
        public string Email { get; set; }
        [StringLength(100, ErrorMessage = "Address must be of length less than 100")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Gender must be selected")]
        [MaxLength(10)]
        public string Gender { get; set; }
    }
}