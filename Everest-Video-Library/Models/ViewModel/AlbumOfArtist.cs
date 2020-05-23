using Everest_Video_Library.Models.VideoLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Everest_Video_Library.Controllers.ViewModel
{
    public class AlbumOfArtist:Artist
    {
        public List<Album> Albums { get; set; }
    }
}