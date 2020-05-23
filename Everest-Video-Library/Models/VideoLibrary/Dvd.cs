using Everest_Video_Library.Models.VideoLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Everest_Video_Library.Models
{
    public class Dvd
    {
        public int Id { get; set; }
        public bool OnStock { get; set; }
        [Required]
        public int AlbumId { get; set; }
        [ForeignKey("AlbumId")]
        public virtual Album  Album { get; set; }

    }
}