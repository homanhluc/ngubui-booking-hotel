using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ngubui_Project.Models;

namespace Ngubui_Project.Controllers
{
    public class BecomeHostController : Controller
    {
        // GET: BecomeHost
        NgubuiEntities dc = new NgubuiEntities();
        public ActionResult UploadRoom()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadRoom(ROOM_NAME_DETAIL rnd, HttpPostedFileBase RoomImage, HttpPostedFileBase Image1,
            HttpPostedFileBase Image2, HttpPostedFileBase Image3, HttpPostedFileBase Image4, HttpPostedFileBase Image5,
            HttpPostedFileBase Image6, HttpPostedFileBase Image7, HttpPostedFileBase Image8, HttpPostedFileBase Image9,
            ROOM_NAME u, ROOM_IMAGE ri)
        {
   
                    if (RoomImage != null)
                    {
                        rnd.AnhDaiDien = new byte[RoomImage.ContentLength];
                        RoomImage.InputStream.Read(rnd.AnhDaiDien, 0, RoomImage.ContentLength);
                    }
                    if (Image1 != null)
                    {
                        rnd.Image1 = new byte[Image1.ContentLength];
                        Image1.InputStream.Read(rnd.Image1, 0, Image1.ContentLength);
                        ri.Image1 = rnd.Image1;
                    }
                    if (Image2 != null)
                    {
                        rnd.Image2 = new byte[Image2.ContentLength];
                        Image2.InputStream.Read(rnd.Image2, 0, Image2.ContentLength);
                        ri.Image2 = rnd.Image2;
                    }
                    if (Image3 != null)
                    {
                        rnd.Image3 = new byte[Image3.ContentLength];
                        Image3.InputStream.Read(rnd.Image3, 0, Image3.ContentLength);
                        ri.Image3 = rnd.Image3;
                    }
                    if (Image4 != null)
                    {
                        rnd.Image4 = new byte[Image4.ContentLength];
                        Image4.InputStream.Read(rnd.Image4, 0, Image4.ContentLength);
                        ri.Image4 = rnd.Image4;
                    }
                    if (Image5 != null)
                    {
                        rnd.Image5 = new byte[Image5.ContentLength];
                        Image5.InputStream.Read(rnd.Image5, 0, Image5.ContentLength);
                        ri.Image5 = rnd.Image5;
                    }
                    if (Image6 != null)
                    {
                        rnd.Image6 = new byte[Image6.ContentLength];
                        Image6.InputStream.Read(rnd.Image6, 0, Image6.ContentLength);
                        ri.Image6 = rnd.Image6;
                    }
                    if (Image7 != null)
                    {
                        rnd.Image7 = new byte[Image7.ContentLength];
                        Image7.InputStream.Read(rnd.Image7, 0, Image7.ContentLength);
                        ri.Image7 = rnd.Image7;
                    }
                    if (Image8 != null)
                    {
                        rnd.Image8 = new byte[Image8.ContentLength];
                        Image8.InputStream.Read(rnd.Image8, 0, Image8.ContentLength);
                        ri.Image8 = rnd.Image8;
                    }
                    if (Image9 != null)
                    {
                        rnd.Image9 = new byte[Image9.ContentLength];
                        Image9.InputStream.Read(rnd.Image9, 0, Image9.ContentLength);
                        ri.Image9 = rnd.Image9;
                    }
 
            u.ID_Room = rnd.ID_Room;
            u.ID_User = rnd.ID_User;
            ri.ID_Room = rnd.ID_Room;

            dc.ROOM_NAME_DETAIL.Add(rnd);
            dc.ROOM_NAME.Add(u);
            dc.ROOM_IMAGE.Add(ri);
            dc.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Guide()
        {
            return View();
        }
    }
}