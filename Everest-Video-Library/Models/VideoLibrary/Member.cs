using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Everest_Video_Library.Models.VideoLibrary.BaseClass;
using System.Linq;
using System.Web;

namespace Everest_Video_Library.Models.VideoLibrary
{
    public class Member: People
    {
        public int CatagoryId { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [ForeignKey("CatagoryId")]
        public virtual MemberCatagory Catagory { get; set; }

        public IEnumerable<Lone> Lones { get; set; }

    }
}