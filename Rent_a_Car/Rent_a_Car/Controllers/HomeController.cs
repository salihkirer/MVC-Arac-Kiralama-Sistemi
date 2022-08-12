using Rent_a_Car.Models;
using Rent_a_Car.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rent_a_Car.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        carDatabaseEntities5 db = new carDatabaseEntities5();
        public ActionResult Index()
        {
            List<Banner> bannerListe = db.Banner.ToList();
            return View(bannerListe);
        }
        public ActionResult Hakkimizda()
        {
            return View();
        }
        public ActionResult Iletisim()
        {
            return View();
        }
        public ActionResult UyeOl()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UyeOl(uyeModel model)
        {
            if (db.Uye.Where(m => m.kullaniciAdi == model.KullaniciAdi).Count() > 0)
            {
                ViewBag.hata = "Girilen Kullanıcı Adı Kayıtlıdır!";
                return View();
            }
            Uye yeni = new Uye();
            if (model.Foto != null && model.Foto.ContentLength > 0)
            {
                string dosya = Guid.NewGuid().ToString();
                string uzanti = Path.GetExtension(model.Foto.FileName).ToLower();
                if (uzanti != ".jpg" && uzanti != ".jpeg" && uzanti != ".png")
                {
                    ModelState.AddModelError("Foto", "Dosya Uzantısı JPG,JPEG veya PNG Olmalıdır!");
                    return View(model);
                }
                string dosyaAdi = dosya + uzanti;
                model.Foto.SaveAs(Server.MapPath("~/Content/uyeFoto/" + dosyaAdi));
                yeni.foto = dosyaAdi;
            }
            yeni.kullaniciAdi = model.KullaniciAdi;
            yeni.sifre = model.Sifre;
            yeni.adSoyad = model.AdSoyad;
            yeni.email = model.Email;
            db.Uye.Add(yeni);
            db.SaveChanges();
            Uye uye = db.Uye.OrderByDescending(u => u.uyeId).FirstOrDefault();
            Session["uyeOturum"] = true;
            Session["uyeId"] = uye.uyeId;
            Session["uyeKadi"] = uye.kullaniciAdi;
            Session["uyeAdmin"] = uye.uyeAdmin;
            Session["uyeFoto"] = uye.foto;
            return RedirectToAction("Index");
        }
        public ActionResult UyeGuncelle(int? id)
        {
            Uye uye = db.Uye.Where(m => m.uyeId == id).SingleOrDefault();
            if (uye == null)
            {
                return RedirectToAction("UyeOl");
            }
            uyeModel model = new uyeModel();
            model.UyeId = uye.uyeId;
            model.KullaniciAdi = uye.kullaniciAdi;
            model.Sifre = uye.sifre;
            model.AdSoyad = uye.adSoyad;
            model.Email = uye.email;
            model.UyeAdmin = uye.uyeAdmin;
            return View(model);
        }
        [HttpPost]
        public ActionResult UyeGuncelle(uyeModel model)
        {
            if (db.Uye.Where(m => m.kullaniciAdi == model.KullaniciAdi).Count() > 0)
            {
                ViewBag.hata = "Girilen Kullanıcı Adı Kayıtlıdır!";
                return View();
            }
            Uye uye = db.Uye.Where(m => m.uyeId == model.UyeId).SingleOrDefault();
            if (model.Foto != null && model.Foto.ContentLength > 0)
            {
                string dosya = Guid.NewGuid().ToString();
                string uzanti = Path.GetExtension(model.Foto.FileName).ToLower();
                if (uzanti != ".jpg" && uzanti != ".jpeg" && uzanti != ".png")
                {
                    ModelState.AddModelError("Foto", "Dosya Uzantısı JPG,JPEG veya PNG Olmalıdır!");
                    return View(model);
                }
                string dosyaAdi = dosya + uzanti;
                model.Foto.SaveAs(Server.MapPath("~/Content/uyeFoto/" + dosyaAdi));
                uye.foto = dosyaAdi;
            }
            uye.kullaniciAdi = model.KullaniciAdi;
            uye.sifre = model.Sifre;
            uye.adSoyad = model.AdSoyad;
            uye.email = model.Email;
            uye.uyeAdmin = model.UyeAdmin;
            db.SaveChanges();
            ViewBag.sonuc = "Bilgiler Güncellendi";
            return View();
        }
        public ActionResult OturumAc(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public ActionResult OturumAc(uyeModel model, string returnUrl)
        {
            Uye uye = db.Uye.Where(m => m.kullaniciAdi == model.KullaniciAdi && m.sifre == model.Sifre).SingleOrDefault();
            if (uye != null)
            {
                Session["uyeOturum"] = true;
                Session["uyeId"] = uye.uyeId;
                Session["uyeKadi"] = uye.kullaniciAdi;
                Session["uyeAdmin"] = uye.uyeAdmin;
                Session["uyeFoto"] = uye.foto;
                if (returnUrl == null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return Redirect(returnUrl);
                }
            }
            else
            {
                ViewBag.hata = "Geçersiz Kullanıcı Adı veya Parola!";
                return View();
            }
        }
        public ActionResult OturumKapat(string returnUrl)
        {
            Session.Abandon();
            return Redirect(returnUrl);
        }
        public ActionResult SonEklenenler()
        {
            List<Araba> arabalar = db.Araba.Where(m => m.kiralama == 0).OrderByDescending(m => m.arabaId).Take(4).ToList();
            return PartialView(arabalar);
        }
        public ActionResult UcuzArac()
        {
            List<Araba> arabalar = db.Araba.Where(m => m.kiralama == 0).OrderBy(m => m.fiyat).Take(1).ToList();
            return PartialView(arabalar);
        }
        public ActionResult Arabalar()
        {
            List<Araba> arabalar = db.Araba.Where(m => m.kiralama == 0).ToList();
            return View(arabalar);
        }
        public ActionResult ArabaKirala(int? id)
        {
            if (Session["uyeId"] == null)
            {
                return RedirectToAction("OturumAc");
            }
            Araba araba = db.Araba.Where(m => m.arabaId == id).SingleOrDefault();
            if (araba == null)
            {
                return RedirectToAction("Arabalar");
            }
            kiralaModel model = new kiralaModel();
            model.arabaId = araba.arabaId;
            return View(model);
        }
        [HttpPost]
        public JsonResult ArabaKirala(int arabaId, string alisYeri, string teslimYeri, DateTime alisTarihi, DateTime teslimTarihi)
        {

            if (Session["uyeId"] != null)
            {
                int uyeId = Convert.ToInt32(Session["uyeId"]);
                db.Kiralama.Add(new Kiralama()
                {
                    uyeId = uyeId,
                    arabaId = arabaId,
                    alisYeri = alisYeri,
                    teslimYeri = teslimYeri,
                    alisTarihi = alisTarihi,
                    teslimTarihi = teslimTarihi

                });
                Araba kira = db.Araba.Where(m => m.arabaId == arabaId).SingleOrDefault();
                kira.kiralama = uyeId;
                db.SaveChanges();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ArabaDetay(int? id)
        {
            Araba arabadetay = db.Araba.Where(m => m.arabaId == id).SingleOrDefault();
            if (arabadetay == null)
            {
                return RedirectToAction("Arabalar");
            }
            return View(arabadetay);
        }

    }
}