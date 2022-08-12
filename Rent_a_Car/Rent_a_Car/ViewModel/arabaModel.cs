using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rent_a_Car.ViewModel
{
    public class arabaModel
    {
        public int arabaId { get; set; }

        [Required(ErrorMessage = "Araba Markası Giriniz!")]
        [Display(Name = "Araba Markası")]
        [StringLength(50, ErrorMessage = "Araba Markası En fazla 50 Karakter olmalı")]
        public string arabaMarka { get; set; }

        [Required(ErrorMessage = "Araba Modeli Giriniz!")]
        [Display(Name = "Araba Modeli")]
        [StringLength(50, ErrorMessage = "Araba Modeli En fazla 50 Karakter olmalı")]
        public string arabaModeli { get; set; }

        [Required(ErrorMessage = "Yakit Tipi Giriniz!")]
        [Display(Name = "Yakıt Tipi")]
        [StringLength(50, ErrorMessage = "Yakit Tipi En fazla 50 Karakter olmalı")]
        public string arabaYakit { get; set; }

        [Required(ErrorMessage = "Şanzıman Tipi Giriniz!")]
        [Display(Name = "Şanzıman Tipi")]
        [StringLength(50, ErrorMessage = "Şanzıman Tipi En fazla 50 Karakter olmalı")]
        public string arabaSanz { get; set; }

        [Required(ErrorMessage = "Kişi Sayısı Giriniz!")]
        [Display(Name = "Kişi Sayısı")]
        public int kisiSay { get; set; }

        [Required(ErrorMessage = "Kapı Sayısı Giriniz!")]
        [Display(Name = "Kapı Sayısı")]
        public int kapiSay { get; set; }

        [Required(ErrorMessage = "Bagaj Tipi Giriniz!")]
        [Display(Name = "Bagaj Tipi")]
        public int bagaj { get; set; }

        [Required(ErrorMessage = "Klima Giriniz!")]
        [Display(Name = "Klima")]
        [StringLength(50, ErrorMessage = "Klima En fazla 50 Karakter olmalı")]
        public string klima { get; set; }

        [Required(ErrorMessage = "Fiyat Giriniz!")]
        [Display(Name = "Fiyat")]
        public int fiyat { get; set; }

        [Required(ErrorMessage = "Gps Giriniz!")]
        [Display(Name = "Gps")]
        public string gps { get; set; }

        [Required(ErrorMessage = "Plaka Giriniz!")]
        [Display(Name = "Plaka")]
        [StringLength(50, ErrorMessage = "Plaka En fazla 50 Karakter olmalı")]
        public string plaka { get; set; }

        [Display(Name = "Fotoğraf")]
        [Required(ErrorMessage = "Fotoğraf Seçiniz!")]
        public HttpPostedFileBase foto { get; set; }


        public int kiralama { get; set; }

    }
}