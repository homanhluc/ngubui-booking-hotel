using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ngubui_Project.Models;
using System.Data.Entity;

namespace Ngubui_Project.Controllers
{
    public class EditProfileController : Controller
    {
        // GET: EditProfile
        NgubuiEntities db = new NgubuiEntities();
        public ActionResult EditProFile(int ID_User = 0)
        {
            return View(db.USER_NAME_DETAIL.Find(ID_User));
        }
        [HttpPost]
        public ActionResult EditProFile(USER_NAME_DETAIL und, HttpPostedFileBase Avartar)
        {
            if (ModelState.IsValid)
            {
                if (Avartar != null)
                {
                    und.AnhDaiDien = new byte[Avartar.ContentLength];
                    Avartar.InputStream.Read(und.AnhDaiDien, 0, Avartar.ContentLength);
                }
                db.Entry(und).State = EntityState.Modified;
                db.SaveChanges();
                ModelState.Clear();
            }
            
            return RedirectToAction("EditProFile", new {@ID_User = und.ID_User });
        }
        public PartialViewResult HistorypostroomPartial(int ID_User=0)
        {
            var list =(from p in db.ROOM_NAME_DETAIL
                       where p.ID_User == ID_User
                       select p).ToList();
            return PartialView(list);
        }
        public PartialViewResult HistorysettedroomPartial(int ID_User = 0)
        {
            var list = (from p in db.ROOM_NAME_DETAIL
                        join o in db.ROOM_NAME on p.ID_Room equals o.ID_Room
                        where p.ID_User == ID_User && o.TrangThai == "Đặt"
                        select p).ToList();
            return PartialView(list);
        }
        public ActionResult Edit(int ID_Room = 0)
        {
            ROOM_NAME rn = db.ROOM_NAME.SingleOrDefault(n => n.ID_Room == ID_Room);
            ROOM_NAME rnb = db.ROOM_NAME.SingleOrDefault(n => n.ID_Room == ID_Room);
            rnb.ID_Room = rn.ID_Room;
            rnb.ID_User = rn.ID_User;
            rnb.TrangThai = null;
            db.Entry(rnb).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index","Home");
        }
    }
}