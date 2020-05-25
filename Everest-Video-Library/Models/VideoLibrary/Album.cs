using Everest_Video_Library.Models.VideoLibrary.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Everest_Video_Library.Models.VideoLibrary
{
    public class Album:ObjectProperty
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public int NoOfCopies { get; set; }
        [Required]
        public int Length { get; set; }
        public int NoOfStock { get; set; }
        [NotMapped]
        public HttpPostedFileBase CoverImage { get; set; }
        [Display(Name = "Cover Image")]
        [DataType(DataType.Upload)]
        public string CoverImagePath { get; set; }
        public bool AgeContent { get; set; }
        [DataType(DataType.Currency)]
        public Decimal Price { get; set; }

        [Display(Name="Generic")]
        public int CatagoryId { get; set; }
        
        [Display(Name="Producer")]
        public int ProducerId { get; set; }
        [Display(Name="Studio")]
        public int StudioId { get; set; }
        [DataType(DataType.Text)]
        public string Description { get; set; }


        [ForeignKey("StudioId")]
        public virtual Studio  Studio { get; set; }
        [ForeignKey("CatagoryId")]
        public virtual Catagory Catagory { get; set; }
        [ForeignKey("ProducerId")]
        public virtual Producer Producer { get; set; }
        public virtual IEnumerable<ArtistAlbum> ArtistAlbums { get; set; }
        public virtual IEnumerator<Lone> Lones { get; set; }
    }
}