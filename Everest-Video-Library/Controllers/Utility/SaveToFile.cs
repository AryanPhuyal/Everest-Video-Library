using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Everest_Video_Library.Controllers.Utility
{
    public class SaveToFile
    {
        public HttpPostedFileBase Image { get; set; }
        public string ServerPath { get; set; }
        public String SaveToServer()
        {
            string fileName = System.IO.Path.GetFileNameWithoutExtension(this.Image.FileName);
            string extension = System.IO.Path.GetExtension(this.Image.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            var ImageUrl = "Images/" + fileName;
            fileName = System.IO.Path.Combine(this.ServerPath + "/"+fileName);
            this.Image.SaveAs(fileName);
            return ImageUrl;
        } 
     
    }
}