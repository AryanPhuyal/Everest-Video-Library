using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Everest_Video_Library.Models.VideoLibrary
{
    public class Lone
    {
        [Key]
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int DvdId { get; set; }
        [DataType(DataType.Date)]
        public DateTime LoneDate { get; set; }
        [DataType(DataType.Date)]
        public  Nullable<DateTime> ReturnDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ReturnedDate { get; set; }
        public decimal FineAmount { get; set; }
        [ForeignKey("MemberId")]
        public virtual Member Members { get; set; }
        [ForeignKey("DvdId")]
        public virtual Dvd Dvds { get; set; }
    }
}