using MyFirstMVC.Data;
using MyFirstMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFirstMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Merhaba Bu benim web sitemin hakkında sayfası";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "";

            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                //     TODO: Mail Gönder
                try
                {
                    System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
                    mailMessage.From = new System.Net.Mail.MailAddress("jukumus@gmail.com", "Jukumus System");
                    mailMessage.Subject = "İletişim Bilgileri";
                    mailMessage.To.Add("ylcnpkr@gmail.com");
                    string body;
                    body = "Ad Soyad: " + model.FirstName + " " + model.LastName + "<br>";
                    body += "Telefon: " + model.Phone + "<br>";
                    body += "Email: " + model.Email + "<br>";
                    body += "Mesaj: " + model.Message;

                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body = body;

                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
                    smtp.Credentials = new System.Net.NetworkCredential("jukumus@gmail.com", "Sifre");
                    smtp.EnableSsl = true;
                    smtp.Send(mailMessage);
                    ViewBag.Message = "Mesajınız İletildi";
                }
                catch (Exception)
                {
                    ViewBag.Error = "Form Gönderimi Başarısız Oldu.Lütfen Daha Sonra Tekrar Deneyiniz. ";
                }
            }
        
            return View(model);
        }

        public ActionResult Project()
        {
            using (var db=new ApplicationDbContext())
            {
                var projects = db.Projects.ToList();
                return View(projects);
            }
            
        }

        public ActionResult Cookies()
        {
            return View();
        }

        public ActionResult TermsofUse()
        {
            return View();
        }
    }
}