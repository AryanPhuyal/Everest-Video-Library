using Everest_Video_Library.Models.VideoLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Everest_Video_Library.Models.ViewModel
{
    public class LoneDate
    {
        public string AlbumName { get; set; }
        public List<Lone> Lones { get; set; }
        public List<Lone> LonesDates { get; set; }
        public void makeLoneDate(string AlbumName,List<Lone> lones)
        {
            var lone  = lones.GroupBy(X => X.LoneDate);
            foreach(Lone i in lone)
            {

            }

        }
    }
}