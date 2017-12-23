using Ngubui_Project.Models;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ngubui_Project.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        NgubuiEntities db = new NgubuiEntities();
        public ActionResult GisMap()
        {
            return View();
        }
        public ActionResult Notcheck(int? page)
        {
            var listNewRooms = (from p in db.ROOM_NAME_DETAIL
                                where p.XetDuyet == null
                                select p).ToList();
            int pageNumber = (page ?? 1);
            int pageSize = 9;
            return View(listNewRooms.OrderBy(n=>n.ID_Room).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Checked(int? page)
        {
            var listNewRooms = (from p in db.ROOM_NAME_DETAIL
                                where p.XetDuyet == "Có"
                                select p).ToList();
            int pageNumber = (page ?? 1);
            int pageSize = 9;
            return View(listNewRooms.OrderBy(n => n.ID_Room).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Edit(int ID_Room=0)
        {
            return View(db.ROOM_NAME_DETAIL.Find(ID_Room));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ROOM_NAME_DETAIL rnd)
        {
            db.Entry(rnd).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Notcheck");
        }


        [HttpPost]
        public ActionResult RoomSearchResult(FormCollection f, int? page)
        {
            string sTuKhoa = f["txtTimKiem"].ToString();
            ViewBag.TuKhoa = sTuKhoa;
            List<ROOM_NAME_DETAIL> lstKQTK = db.ROOM_NAME_DETAIL.Where(n => n.TieuDe.Contains(sTuKhoa)).ToList();


            //Phân trang
            int pageNumber = (page ?? 1);
            int pageSize = 9;
            if (lstKQTK.Count == 0)
            {
                ViewBag.ThongBao = "Không tìm thấy!";
                return View(db.ROOM_NAME_DETAIL.OrderBy(n => n.TieuDe).ToPagedList(pageNumber, pageSize));
            }
            else
            {
                ViewBag.ThongBao = "Đã tìm thấy " + lstKQTK.Count + " kết quả";
            }
            return View(lstKQTK.OrderBy(n => n.TieuDe).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult RoomSearchResult(string sTuKhoa, int? page)
        {
            ViewBag.TuKhoa = sTuKhoa;
            List<ROOM_NAME_DETAIL> lstKQTK = db.ROOM_NAME_DETAIL.Where(n => n.TieuDe.Contains(sTuKhoa)).ToList();


            //Phân trang
            int pageNumber = (page ?? 1);
            int pageSize = 9;
            if (lstKQTK.Count == 0)
            {
                ViewBag.ThongBao = "Không tìm thấy!";
                return View(db.ROOM_NAME_DETAIL.OrderBy(n => n.TieuDe).ToPagedList(pageNumber, pageSize));
            }
            else
            {
                ViewBag.ThongBao = "Đã tìm thấy " + lstKQTK.Count + " kết quả";
            }
            return View(lstKQTK.OrderBy(n => n.TieuDe).ToPagedList(pageNumber, pageSize));
        }
    }
}