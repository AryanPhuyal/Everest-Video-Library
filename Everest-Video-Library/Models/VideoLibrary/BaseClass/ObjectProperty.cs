using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Everest_Video_Library.Models.VideoLibrary.BaseClass
{
    public class ObjectProperty
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Name should not greater tan 50 word")]
        [MinLength(3, ErrorMessage = "Name should not be less than 3")]
        [Display(Name = "Name")]
        public string Name { get; set; }
 
    }
}