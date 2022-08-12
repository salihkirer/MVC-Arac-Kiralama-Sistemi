using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rent_a_Car.ViewModel
{
    public class bannerModel
    {
        public int Id { get; set; }

        [Display(Name = "Fotoğraf")]
        [Required(ErrorMessage = "Fotoğraf Seçiniz!")]
        public HttpPostedFileBase foto { get; set; }
    }
}