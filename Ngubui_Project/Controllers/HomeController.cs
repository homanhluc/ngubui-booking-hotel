using Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Ngubui_Project.Models;
using System.Data.Entity;
using System.Text;
using System.Net.Mail;

namespace Ngubui_Project.Controllers
{

    
    public class HomeController : Controller
    {
        // GET: Home
        NgubuiEntities db = new NgubuiEntities();
        public ActionResult Index()
        {
            return View();
        }
        //load danh sách phòng mới nhất
        public PartialViewResult NewRoomPartial()
        {
            var listNewRoom = (from p in db.ROOM_NAME_DETAIL
                               join o in db.ROOM_NAME on p.ID_Room equals o.ID_Room
                               where o.TrangThai == null
                                orderby p.ID_Room descending
                               select p).Take(6).ToList();
            return PartialView(listNewRoom);
        }
        //load danh sách các phòng khách sạn
        public PartialViewResult KhachSanPartial()
        {
            var listRoomKhachsan = (from p in db.ROOM_NAME_DETAIL
                              where p.LoaiPhong == "Khách sạn"
                              orderby p.ID_Room descending
                              select p).Take(6).ToList();
            
            return PartialView(listRoomKhachsan);
        }
        //load danh sách các phòng nhà nghĩ
        public PartialViewResult NhaNghiPartial()
        {
            var listRoomNhanghi = (from p in db.ROOM_NAME_DETAIL
                                   where p.LoaiPhong == "Nhà nghỉ"
                                   orderby p.ID_Room descending
                                   select p).Take(6).ToList();

            return PartialView(listRoomNhanghi);
        }
        //load danh sách các phòng phòng trọ
       
        public PartialViewResult PhongTroPartial()
        {
            var listRoomPhongtro = (from p in db.ROOM_NAME_DETAIL
                                    where p.LoaiPhong == "Phòng trọ"
                                    orderby p.ID_Room descending
                                    select p).Take(6).ToList();

            return PartialView(listRoomPhongtro);
        }
        public PartialViewResult SimilarRoomPartial(int ID_Room)
        {
            ROOM_NAME_DETAIL Room = db.ROOM_NAME_DETAIL.SingleOrDefault(n => n.ID_Room == ID_Room);
            var list = (from p in db.ROOM_NAME_DETAIL
                        where p.LoaiPhong == Room.LoaiPhong.ToString()
                        orderby p.ID_Room descending
                        select p).Take(5).ToList();
            return PartialView(list);
        }
        public ViewResult RoomDetail(int ID_Room)
        {
            ROOM_NAME_DETAIL Room = db.ROOM_NAME_DETAIL.SingleOrDefault(n => n.ID_Room == ID_Room);
            ROOM_IMAGE RoomImg = db.ROOM_IMAGE.SingleOrDefault(n => n.ID_Room == ID_Room);
            USER_NAME_DETAIL und = db.USER_NAME_DETAIL.SingleOrDefault(n => n.ID_User == Room.ID_User);
            USER_NAME un = db.USER_NAME.SingleOrDefault(n => n.ID_User == Room.ID_User);
            Session["UND"] = und;
            Session["UN"] = un;
            Session["DiaChi"] = Room.DiaChi;
            Session["LoaiPhong"] = Room.LoaiPhong;
            Session["Gia"] = Room.GiaPhong;
            Session["ID_Room"] = Room.ID_Room;
            Session["Image1"] = RoomImg.Image1;
            Session["Image2"] = RoomImg.Image2;
            Session["Image3"] = RoomImg.Image3;
            Session["Image4"] = RoomImg.Image4;
            Session["Image5"] = RoomImg.Image5;
            Session["Image6"] = RoomImg.Image6;
            Session["Image7"] = RoomImg.Image7;
            Session["Image8"] = RoomImg.Image8;
            Session["Image9"] = RoomImg.Image9;
            if (Room == null || RoomImg == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(Room);
        }
        [HttpPost]
        public ActionResult CheckDate(ROOM_NAME_DETAIL rnd)
        {

            Session["Checkdate"] = rnd;
            return RedirectToAction("Book");
        }
        public ActionResult BookingConfirmation(int ID_Room = 0, int ID_User=0)
        {
            ROOM_NAME_DETAIL rnd = db.ROOM_NAME_DETAIL.SingleOrDefault(n => n.ID_Room == ID_Room);
            USER_NAME un = db.USER_NAME.SingleOrDefault(n => n.ID_User == ID_User);
            USER_NAME_DETAIL und = db.USER_NAME_DETAIL.SingleOrDefault(n => n.ID_User == ID_User);
            ROOM_NAME rn = db.ROOM_NAME.SingleOrDefault(n => n.ID_Room == ID_Room);
            ROOM_NAME rnb = db.ROOM_NAME.SingleOrDefault(n => n.ID_Room == ID_Room);
            var mails = (from p in db.ROOM_NAME_DETAIL
                        join o in db.USER_NAME on p.ID_User equals o.ID_User
                        where p.ID_Room == ID_Room
                        select new {o.Email }).First();
            var Emails = mails.Email;
            rnb.ID_Room = rn.ID_Room;
            rnb.ID_User = rn.ID_User;
            rnb.TrangThai = "Đặt";
            db.Entry(rnb).State = EntityState.Modified;
            db.SaveChanges();
            //Lưu book
            var checkdate = Session["Checkdate"] as Ngubui_Project.Models.ROOM_NAME_DETAIL;
            BOOK_ROOM br = new BOOK_ROOM();
            br.ID_User = un.ID_User;
            br.ID_Room = rnb.ID_Room;
            br.NgayDatPhong = checkdate.Checkin;
            br.NgayNhanPhong = checkdate.Checkout;
            br.TrangThai = rnb.TrangThai;
            db.BOOK_ROOM.Add(br);
            db.SaveChanges();
            //gửi mail

            //Send key code

            //lấy dữ liệu mới nhất từ database

            StringBuilder Body = new StringBuilder();
            Body.Append("<p>Chào chủ phòng! " + "!<br/>Bạn vừa nhận được một đơn đặt phòng từ: " + "<strong>" +un.Ho+" "+un.Ten+ "</strong>"+ "</p>");
            Body.Append("<h4>Chi tiết đặt phòng</h4>");
            Body.Append("<p>Ngày nhận phòng: "+ checkdate.Checkin+ "</p>");
            Body.Append("<p>Ngày trả phòng: " + checkdate.Checkout+ "</p>");
            Body.Append("<p>Số khách: " + checkdate.SoKhach+ "</p>");
            Body.Append("<h4>Liên hệ để xác nhận</h4>");
            Body.Append("<p>Số điện thoại: " + und.SoDienThoai + "</p>");
            Body.Append("<p>Địa chỉ Email: " + un.Email + "</p>");

            MailMessage mail = new MailMessage();
            mail.To.Add(Emails);
            mail.From = new MailAddress("homanhluc.it@gmail.com");
            mail.Subject = "Chào mừng bạn đã đến với Ngubui";
            mail.Body = Body.ToString();// phần thân của mail ở trên
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = new System.Net.NetworkCredential("homanhluc.it@gmail.com", "01644545169Luc");// tài khoản Gmail
            smtp.EnableSsl = true;
            try { smtp.Send(mail); } //phòng trường hợp không có mạng
            catch { ViewBag.succes = "Đăng ký không thành công!"; }
            return RedirectToAction("Index");
        }
        public PartialViewResult ImageRoom(int ID_Room)
        {

            var listImage = db.ROOM_IMAGE.Take(6).ToList();
            return PartialView(listImage);
        }
        public ActionResult Book()
        {

            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Faq()
        {
            return View();
        }
        
        //connect FB
        /* public ActionResult Facebook()
         {
             var fb = new FacebookClient();
             var loginUrl = fb.GetLoginUrl(new
             {
                 client_id = "1329561837101010",
                 client_secret = "1bc7e9f9225e9503141acd44016efc26",
                 redirect_uri = RedirectUri.AbsoluteUri,
                 response_type = "code",
                 scope = "email"
             });
             return Redirect(loginUrl.AbsoluteUri);
         }
         private Uri RedirectUri
         {
             get
             {
                 var uriBuilder = new UriBuilder(Request.Url);

                 uriBuilder.Query = null;

                 uriBuilder.Fragment = null;

                 uriBuilder.Path = Url.Action("FacebookCallback");

                 return uriBuilder.Uri;

             }
         }
         public ActionResult FacebookCallback(string code)
         {
             var fb = new FacebookClient();
             dynamic result = fb.Post("oauth/access_token", new

             {

                 client_id = "479548222086702",

                 client_secret = "41ea2c69cc3f93c0c157f7869a67fc26",

                 redirect_uri = RedirectUri.AbsoluteUri,

                 code = code

             });
             var accessToken = result.access_token;

             //Luu access token ma fb tra ve vao session
             Session["AccessToken"] = accessToken;

             fb.AccessToken = accessToken;

             dynamic info = fb.Get("me?fields=first_name,last_name,id,email");
             string email = info.email;

             FormsAuthentication.SetAuthCookie(email, false);

             return RedirectToAction("Index", "Home");
         }
         public ActionResult Login()
         {
             return View();
         }
         */
    }
}