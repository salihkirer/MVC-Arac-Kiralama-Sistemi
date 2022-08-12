using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rent_a_Car.ViewModel
{
    public class kiralaModel
    {
        public int Id { get; set; }

        [Display(Name = "Üye Id")]
        public int uyeId { get; set; }

        [Display(Name = "Araba Id")]
        public int arabaId { get; set; }

        [Display(Name = "Alış Yeri")]
        public string alisYeri { get; set; }

        [Display(Name = "Teslim Yeri")]
        public string teslimYeri { get; set; }

        [Display(Name = "Alış Tarihi")]
        public DateTime alisTarihi { get; set; }

        [Display(Name = "Teslim Tarihi")]
        public DateTime teslimTarihi { get; set; }

    }
}