using Everest_Video_Library.Models.VideoLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Everest_Video_Library.Controllers.ViewModel
{
    public class AddLone
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Select Album")]
        public List<Album> Albums { get; set; }
        [Display(Name ="Select Members")]
        public List<Member> Members { get; set; }
    }
}