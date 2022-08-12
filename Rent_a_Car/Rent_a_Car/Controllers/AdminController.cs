using Rent_a_Car.Auth;
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
    public class AdminController : BaseController
    {
        carDatabaseEntities5 db = new carDatabaseEntities5();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult FotoEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FotoEkle(bannerModel model)
        {
            Banner yeni = new Banner();
            if (model.foto != null && model.foto.ContentLength > 0)
            {
                string dosya = Guid.NewGuid().ToString();
                string uzanti = Path.GetExtension(model.foto.FileName).ToLower();
                if (uzanti != ".jpg" && uzanti != ".png" && uzanti != ".jpeg")
                {
                    ModelState.AddModelError("foto", "Dosya Uzantısı JPEG,JPG veya PNG olmalı!");
                    return View(model);
                }
                string dosyaAdi = dosya + uzanti;
                model.foto.SaveAs(Server.MapPath("~/Content/Banner/" + dosyaAdi));
                yeni.resimYol = dosyaAdi;
            }
            db.Banner.Add(yeni);
            db.SaveChanges();
            ModelState.Clear();
            ViewBag.sonuc = "Fotoğraf Eklendi";
            return View();
        }
        public ActionResult FotoListele()
        {
            List<Banner> bannerListe = db.Banner.ToList();
            return View(bannerListe);
        }
        public ActionResult FotoSil(int? id)
        {
            Banner kayit = db.Banner.Where(k => k.Id == id).SingleOrDefault();
            if (kayit == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                string resimYol = Server.MapPath("~/Content/Banner/" + kayit.resimYol);
                if (System.IO.File.Exists(resimYol))
                {
                    System.IO.File.Delete(resimYol);
                }
                db.Banner.Remove(kayit);
                db.SaveChanges();
                return RedirectToAction("FotoListele");
            }
        }
        public ActionResult Arabalar(int? id)
        {
            if (id == 1)
            {
                ViewBag.sonuc = "Araba Silindi";
            }
            List<Araba> arabalar = db.Araba.ToList();
            return View(arabalar);
        }
        public ActionResult ArabaEkle()
        {
            arabaModel model = getModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult ArabaEkle(arabaModel model)
        {

            if (db.Araba.Where(m => m.plaka == model.plaka).Count() > 0)
            {
                ViewBag.hata = "Girilen Araba Kayıtlıdır!";
                return View();
            }
            else
            {
                Araba araba = new Araba();
                if (model.foto != null && model.foto.ContentLength > 0)
                {
                    string dosya = Guid.NewGuid().ToString();
                    string uzanti = Path.GetExtension(model.foto.FileName).ToLower();
                    if (uzanti != ".jpg" && uzanti != ".jpeg" && uzanti != ".png")
                    {
                        ModelState.AddModelError("Foto", "Dosya Uzantısı JPG,JPEG veya PNG Olmalıdır!");
                        return View(model);
                    }
                    string dosyaAdi = dosya + uzanti;
                    model.foto.SaveAs(Server.MapPath("~/Content/carFoto/" + dosyaAdi));
                    araba.foto = dosyaAdi;

                    araba.arabaId = model.arabaId;
                    araba.arabaMarka = model.arabaMarka;
                    araba.arabaModeli = model.arabaModeli;
                    araba.arabaYakit = model.arabaYakit;
                    araba.arabaSanz = model.arabaSanz;
                    araba.kisiSay = model.kisiSay;
                    araba.kapiSay = model.kapiSay;
                    araba.bagaj = model.bagaj;
                    araba.klima = model.klima;
                    araba.fiyat = model.fiyat;
                    araba.gps = model.gps;
                    araba.plaka = model.plaka;
                    araba.kiralama = 0;
                    db.Araba.Add(araba);
                    db.SaveChanges();
                    ViewBag.sonuc = "Araba Eklendi";
                    model = getModel();
                    return View(model);
                }
                else
                {
                    ModelState.AddModelError("Foto", "Dosya Seçim Hatası!");
                    model = getModel();
                    return View(model);
                }
            }
        }
        public ActionResult ArabaDuzenle(int? id)
        {

            Araba araba = db.Araba.Where(m => m.arabaId == id).SingleOrDefault();
            if (araba == null)
            {
                return RedirectToAction("Arabalar");
            }
            arabaModel model = new arabaModel();
            model.arabaId = araba.arabaId;
            model.arabaMarka = araba.arabaMarka;
            model.arabaModeli = araba.arabaModeli;
            model.arabaYakit = araba.arabaYakit;
            model.arabaSanz = araba.arabaSanz;
            model.klima = araba.klima;
            model.gps = araba.gps;
            model.plaka = araba.plaka;
            return View(model);
        }
        [HttpPost]
        public ActionResult ArabaDuzenle(arabaModel model)
        {
            Araba araba = db.Araba.Where(m => m.arabaId == model.arabaId).SingleOrDefault();
            araba.arabaId = model.arabaId;
            araba.arabaMarka = model.arabaMarka;
            araba.arabaModeli = model.arabaModeli;
            araba.arabaYakit = model.arabaYakit;
            araba.arabaSanz = model.arabaSanz;
            araba.kisiSay = model.kisiSay;
            araba.kapiSay = model.kapiSay;
            araba.bagaj = model.bagaj;
            araba.klima = model.klima;
            araba.fiyat = model.fiyat;
            araba.gps = model.gps;
            araba.plaka = model.plaka;
            db.SaveChanges();
            ViewBag.sonuc = "Araba Güncellendi";
            model = getModel();
            return View(model);
        }
        public ActionResult ArabaSil(int? id)
        {

            Araba araba = db.Araba.Where(m => m.arabaId == id).SingleOrDefault();
            if (araba != null)
            {
                if (System.IO.File.Exists(Server.MapPath("~/Content/BlogFoto/" + araba.foto)))
                {
                    System.IO.File.Delete(Server.MapPath("~/Content/BlogFoto/" + araba.foto));
                }
                db.Araba.Remove(araba);
                db.SaveChanges();
                return RedirectToAction("Arabalar/1");
            }
            return RedirectToAction("Arabalar");
        }
        private arabaModel getModel()
        {
            arabaModel model = new arabaModel();
            return model;
        }

        public ActionResult Uyeler(int? id)
        {
            if (id == 1)
            {
                ViewBag.hata = "Üyeye Ait Makale Olduğu İçin Üye Silinemez!";
            }
            if (id == 2)
            {
                ViewBag.sonuc = "Üye Silindi";
            }
            List<Uye> uyeler = db.Uye.ToList();
            return View(uyeler);
        }
        public ActionResult UyeEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UyeEkle(uyeModel model)
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
                model.Foto.SaveAs(Server.MapPath("~/Content/UyeFoto/" + dosyaAdi));
                yeni.foto = dosyaAdi;
            }
            yeni.kullaniciAdi = model.KullaniciAdi;
            yeni.sifre = model.Sifre;
            yeni.adSoyad = model.AdSoyad;
            yeni.email = model.Email;
            yeni.uyeAdmin = model.UyeAdmin;
            db.Uye.Add(yeni);
            db.SaveChanges();
            ViewBag.sonuc = "Üye Eklendi";
            return View();
        }
        public ActionResult UyeDuzenle(int? id)
        {
            Uye uye = db.Uye.Where(m => m.uyeId == id).SingleOrDefault();
            if (uye == null)
            {
                return RedirectToAction("Uyeler");
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
        public ActionResult UyeDuzenle(uyeModel model)
        {
            Uye uye = db.Uye.Where(m => m.uyeId == model.UyeId).SingleOrDefault();
            uye.kullaniciAdi = model.KullaniciAdi;
            uye.sifre = model.Sifre;
            uye.adSoyad = model.AdSoyad;
            uye.email = model.Email;
            uye.uyeAdmin = model.UyeAdmin;
            db.SaveChanges();
            ViewBag.sonuc = "Üye Güncellendi";
            return View();
        }
        public ActionResult UyeSil(int? id)
        {
            Uye uye = db.Uye.Where(m => m.uyeId == id).SingleOrDefault();
            if (uye != null)
            {
                if (System.IO.File.Exists(Server.MapPath("~/Content/UyeFoto/" + uye.foto)))
                {
                    System.IO.File.Delete(Server.MapPath("~/Content/UyeFoto/" + uye.foto));
                }
                db.Uye.Remove(uye);
                db.SaveChanges();
                return RedirectToAction("Uyeler/2");
            }
            return RedirectToAction("Uyeler");
        }
        public ActionResult Kiralamalar(int? id)
        {
            if (id == 1)
            {
                ViewBag.sonuc = "Kiralama Silindi";
            }
            List<Kiralama> kiralamalar = db.Kiralama.ToList();
            return View(kiralamalar);
        }
        public ActionResult KiralamaSil(int? id)
        {


            Kiralama kiralama = db.Kiralama.Where(m => m.Id == id).SingleOrDefault();
            Araba kira = db.Araba.Where(m => m.arabaId == kiralama.arabaId).SingleOrDefault();
            if (kiralama != null)
            {
                db.Kiralama.Remove(kiralama);
                kira.kiralama = 0;
                db.SaveChanges();
                return RedirectToAction("Kiralamalar/1");
            }
            return RedirectToAction("Kiralamalar");
        }


    }
}
