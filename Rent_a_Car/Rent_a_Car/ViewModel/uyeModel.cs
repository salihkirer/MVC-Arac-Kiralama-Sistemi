using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rent_a_Car.ViewModel
{
    public class uyeModel
    {
        public int UyeId { get; set; }
        [Required(ErrorMessage = "Kullanıcı Adı Giriniz!")]
        [Display(Name = "Kullanıcı Adı")]
        public string KullaniciAdi { get; set; }
        [Required(ErrorMessage = "E-Posta Giriniz!")]
        [Display(Name = "E-Posta ")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Parola Giriniz!")]
        [Display(Name = "Parola")]
        public string Sifre { get; set; }
        [Required(ErrorMessage = "Adı Soyadı Giriniz!")]
        [Display(Name = "Adı Soyadı")]
        public string AdSoyad { get; set; }
        [Required(ErrorMessage = "Fotoğraf Seçiniz")]
        [Display(Name = "Fotoğraf")]
        public HttpPostedFileBase Foto { get; set; }
        public int UyeAdmin { get; set; }
    }
}