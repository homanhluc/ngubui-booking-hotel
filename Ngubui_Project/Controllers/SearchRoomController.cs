using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ngubui_Project.Models;
using PagedList;
namespace Ngubui_Project.Controllers
{
    public class SearchRoomController : Controller
    {
        // GET: SearchRoom
        NgubuiEntities db = new NgubuiEntities();
        [HttpPost]
        public ActionResult RoomSearchResult(FormCollection f, int? page)
        {
            string sTuKhoa = f["txtTimKiem"].ToString();
            ViewBag.TuKhoa = sTuKhoa;
            List<ROOM_NAME_DETAIL> lstKQTK = db.ROOM_NAME_DETAIL.Where(n => n.DiaChi.Contains(sTuKhoa)).ToList();
            //Phân trang
            int pageNumber = (page ?? 1);
            int pageSize = 9;
            if (lstKQTK.Count == 0)
            {
                ViewBag.ThongBao = "Không tìm thấy!";
                return View();
            }
            else
            {
                ViewBag.ThongBao = "Đã tìm thấy "+ lstKQTK.Count + " kết quả";
            }
            return View(lstKQTK.OrderBy(n => n.DiaChi).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult RoomSearchResult(string sTuKhoa, int? page)
        {
            ViewBag.TuKhoa = sTuKhoa;
            List<ROOM_NAME_DETAIL> lstKQTK = db.ROOM_NAME_DETAIL.Where(n => n.DiaChi.Contains(sTuKhoa)).ToList();


            //Phân trang
            int pageNumber = (page ?? 1);
            int pageSize = 9;
            if (lstKQTK.Count == 0)
            {
                ViewBag.ThongBao = "Không tìm thấy!";
                return View();
            }
            else
            {
                ViewBag.ThongBao = "Đã tìm thấy " + lstKQTK.Count + " kết quả";
            }
            return View(lstKQTK.OrderBy(n => n.DiaChi).ToPagedList(pageNumber, pageSize));
        }
    }
}