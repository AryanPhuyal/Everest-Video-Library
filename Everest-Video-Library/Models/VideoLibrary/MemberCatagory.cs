using Everest_Video_Library.Models.VideoLibrary.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Everest_Video_Library.Models.VideoLibrary
{
    public class MemberCatagory:ObjectProperty
    {
        [Required]
        [Display(Name="Borrow DVD for (Days)")]
        public int LoneDays { get; set; }
        [Display(Name ="Fine after time expired (per days)")]
        [Required]
        public int FinePerDays { get; set; }
        [Required]
        [Display(Name="No of DVDs he can rent at a time")]
        public int NoOfDvdRent { get; set; }
    }
}