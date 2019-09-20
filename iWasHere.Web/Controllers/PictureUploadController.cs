using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using iWasHere.Domain.DTOs;
using iWasHere.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace iWasHere.Web.Controllers
{
    public class PictureController : Controller
    {

        
        public IActionResult Index()
        {
            return View();
        }
        //public ActionResult Save(IEnumerable<Picture_DTO> AsyncDocumentsFiles)
        //{
        //    if (AsyncDocumentsFiles != null)
        //    {
        //        Picture dbcotext = new Picture();
        //        foreach (var file in AsyncDocumentsFiles)
        //        {
        //            FileStream fs = new FileStream(file.ImageId, FileMode.Open, FileAccess.Read);
        //            BinaryReader br = new BinaryReader(fs);
        //            Byte[] bytes = br.ReadBytes((Int32)fs.Length);
        //            //servosu
        //            var fileUpload = new Picture()
        //            {

        //                //CreatedOn = DateTime.Now,
        //                //ImgID = 
        //                //FileContent = bytes
        //            };
        //            br.Close();
        //            fs.Close();
        //            dbcotext.FileUploads.Add(fileUpload);
        //        }
        //    }
        //    // Return an empty string to signify success  
        //    return Content("");
        //}
    }

}