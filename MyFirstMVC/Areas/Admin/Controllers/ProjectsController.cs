using MyFirstMVC.Data;
using MyFirstMVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFirstMVC.Areas.Admin.Controllers
{
    public class ProjectsController : Controller
    {
        // GET: Admin/Projects
        public ActionResult Index()
        {
            using (var db = new ApplicationDbContext())
            {
                var projects = db.Projects.Include("Category").ToList();
                return View(projects);
            }

        }

        public ActionResult Create()
        {
            var project = new Project(); //Bu sayede varsayılan değerler dolu geliyor
            using (var db = new ApplicationDbContext())
            {
                ViewBag.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
            }
            return View();
        }

        [HttpPost]
        [ValidateInput(false)] //Bu kod olmazsa editor ile bold gonderemeyiz //bu actiona html/script etiketleri artık gönderilebilir
        public ActionResult Create(Project project, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ApplicationDbContext())
                {
                    //Dosyayı Upload etmeyi dene
                    try
                    {
                        project.Photo = UploadFile(upload);
                    }
                    catch (Exception ex)
                    {
                        //Upload Sırasında Hata Oluşursa View da görüntülemek üzere hatayı değişkene e ekle
                        ViewBag.Error = ex.Message;
                        ViewBag.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
                        //Hata Oluştuğu için projeyi View a eklemek yerine View ı tekrar göster ve metottan çık
                        return View(project);
                    }
                  
                    db.Projects.Add(project);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            using (var db = new ApplicationDbContext())
            {
                ViewBag.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
            }
            return View(project);
        }

        public string UploadFile(HttpPostedFileBase upload)
        {
            //Yüklenmek istenen dosya var mı?
            if (upload != null && upload.ContentLength > 0)
            {
                //Dosyaanın uzantısını kontrol et
                var extension = Path.GetExtension(upload.FileName).ToLower();
                if (extension == ".jpg" || extension == ".jpeg" || extension == ".gif" || extension == ".png")
                {
                    //Uzantı Doğruysa dosyanın yükleneceği Uploads dizini var mı kontrol et
                    if (Directory.Exists(Server.MapPath("~/Uploads")))
                    {
                        //Dosya adındaki geçersiz karakterleri düzelt
                        string fileName = upload.FileName.ToLower();
                        fileName = fileName.Replace("İ", "i");
                        fileName = fileName.Replace("Ş", "s");
                        fileName = fileName.Replace("Ğ", "g");
                        fileName = fileName.Replace("ı", "i");
                        fileName = fileName.Replace("ö", "o");
                        fileName = fileName.Replace("ü", "u");
                        fileName = fileName.Replace("ç", "c");
                        fileName = fileName.Replace("ğ", "g");
                        fileName = fileName.Replace("(", "");
                        fileName = fileName.Replace(" ", "-");
                        fileName = fileName.Replace(",", "");
                        fileName = fileName.Replace(" ", ""); //Burada Boşluk Olacak
                        fileName = fileName.Replace("`", "");
                        fileName = fileName.Replace("?", "-");
                        fileName = fileName.Replace("%", "-");
                        
                        //Aynı isimde dosya olabilir diye dosya adının önüne zaman pulu ekliyoruz
                        //guid kullanılabilir profesyonel sitelerde
                        fileName = DateTime.Now.Ticks.ToString() + fileName;

                        //Dosyayı Uploads Dizinine Yükle
                        upload.SaveAs(Path.Combine(Server.MapPath("~/Uploads"), fileName));

                        //Yüklenen Dosyanın adını geri döndür
                        return fileName;
                    }
                    else
                    {
                        throw new Exception("Upload dizini mevcut değil");
                    }
                }
                else
                {
                    throw new Exception("Geçerli bir Dosya uzantısı seçiniz (.jpg .jpeg .gif .png)");
                }
            }
            return null;
        }

        public ActionResult Edit(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var project = db.Projects.Where(x => x.Id == id).FirstOrDefault();
                if (project != null)
                {
                    ViewBag.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
                    return View(project);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Project project,HttpPostedFileBase upload,string deletePhoto)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ApplicationDbContext())
                {
                    try
                    {
                        project.Photo = UploadFile(upload);
                    }
                    catch (Exception ex)
                    {
                        //Upload Sırasında Hata Oluşursa View da görüntülemek üzere hatayı değişkene e ekle
                        ViewBag.Error = ex.Message;
                        ViewBag.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
                        //Hata Oluştuğu için projeyi View a eklemek yerine View ı tekrar göster ve metottan çık
                        return View(project);
                    }
                    var oldproject = db.Projects.Where(x => x.Id == project.Id).FirstOrDefault();
                    if (oldproject != null)
                    {
                       
                        oldproject.Title = project.Title;
                        oldproject.Description = project.Description;
                        oldproject.Body = project.Body;
                        if (!string.IsNullOrEmpty(deletePhoto))
                        {
                            oldproject.Photo = null;
                        }
                        if (!string.IsNullOrEmpty(project.Photo))
                        {                          
                            oldproject.Photo = project.Photo;
                        }
                        oldproject.CategoryId = project.CategoryId;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }
            using (var db = new ApplicationDbContext())
            {
                ViewBag.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
            }
            return View(project);
        }

        public ActionResult Delete(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var project = db.Projects.Where(x => x.Id == id).FirstOrDefault();
                if (project != null)
                {
                    db.Projects.Remove(project);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }


    }
}