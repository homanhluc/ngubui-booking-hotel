using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ngubui_Project.Models;
using System.Web.Security;
using System.Text;
using System.Net.Mail;

namespace Ngubui_Project.Controllers
{
    public class AccountController : Controller
    {
        NgubuiEntities db = new NgubuiEntities();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(USER_NAME un, USER_NAME_DETAIL und)
        {
            //chèn dữ liệu vào bảng
            if (ModelState.IsValid)
            {
                var result = db.USER_NAME.Where(a => a.Email.Equals(un.Email)).ToList();
                if (result.Count == 0)
                {

                    und.ID_User = un.ID_User;
                    db.USER_NAME.Add(un);
                    db.USER_NAME_DETAIL.Add(und);
                    //lưu vào csdl
                    db.SaveChanges();
                    ModelState.Clear();
                    un = null;
                    und = null;
                    ViewBag.succes = "Đăng ký thành công";
                    //Send key code
                    var g = (from p in db.USER_NAME orderby p.ID_User descending select p).Take(1).ToList(); //lấy dữ liệu mới nhất từ database
                    string name = "";
                    for (int i = 0; i < g.Count; i++)
                    {
                        name = g[i].Ho + " " + g[i].Ten;
                    }
                    string gmail = "";
                    for (int i = 0; i < g.Count; i++)
                    {
                        gmail = g[i].Email;
                    }

                    StringBuilder Body = new StringBuilder();
                    Body.Append("<p>Chào " + "<strong>" + name + "</strong>" + "!<br/>Chào mừng bạn đến với website ngubui</p>");
                    Body.Append("<h3>Chúng tôi rất hân hạnh được hỗ trợ bạn!</h3>");

                    MailMessage mail = new MailMessage();
                    mail.To.Add(gmail);
                    mail.From = new MailAddress("homanhluc.it@gmail.com");
                    mail.Subject = "Chào mừng bạn đã đến với Ngubui";
                    mail.Body = Body.ToString();// phần thân của mail ở trên
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = new System.Net.NetworkCredential("homanhluc.it@gmail.com", "01644545169");// tài khoản Gmail
                    smtp.EnableSsl = true;
                    try { smtp.Send(mail); } //phòng trường hợp không có mạng
                    catch { ViewBag.succes = "Đăng ký không thành công!"; }
                }
                else
                {
                    ViewBag.succes = "Email đã được sử dụng!";
                }
            }

            return View();
        }
        public ActionResult Loggin()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Loggin(USER_NAME un)
        {
            if (un.Email.Equals("admin@gmail.com") && un.MatKhau.Equals("12345678"))
            {
                return RedirectToAction("Notcheck", "Admin");
            }
            using (NgubuiEntities dc = new NgubuiEntities())
            {
                
                var v = dc.USER_NAME.Where(a => a.Email.Equals(un.Email) && a.MatKhau.Equals(un.MatKhau)).FirstOrDefault();
                USER_NAME_DETAIL und = new USER_NAME_DETAIL();
                var n = dc.USER_NAME_DETAIL.Where(a => a.ID_User.Equals(v.ID_User)).FirstOrDefault();
                Session["Avartar"] = n;
                if (v != null)
                {
                    Session["Ho"] = v.Ho.ToString();
                    Session["Ten"] = v.Ten.ToString();
                    Session["Email"] = v.Email.ToString();
                    Session["ID_User"] = v.ID_User.ToString();
                    Session["Account"] = v;
                    FormsAuthentication.SetAuthCookie(un.Email, false);
                    return RedirectToAction("AfterLogin");
                }
            }
            return View(un);
        }
        public ActionResult AfterLogin()
        {
            if (Session["Email"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Loggin");
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}