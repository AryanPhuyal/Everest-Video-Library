using Everest_Video_Library.Models.VideoLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Everest_Video_Library.Controllers.VideoLibrary.ViewModel
{
    public class AlbumList:Album
    {
        public List<Artist> Artists { get; set; }
    }
}