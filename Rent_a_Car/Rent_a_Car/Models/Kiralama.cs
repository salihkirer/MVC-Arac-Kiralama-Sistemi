//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Rent_a_Car.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Kiralama
    {
        public int Id { get; set; }
        public Nullable<int> uyeId { get; set; }
        public Nullable<int> arabaId { get; set; }
        public string alisYeri { get; set; }
        public string teslimYeri { get; set; }
        public Nullable<System.DateTime> alisTarihi { get; set; }
        public Nullable<System.DateTime> teslimTarihi { get; set; }
    
        public virtual Araba Araba { get; set; }
        public virtual Uye Uye { get; set; }
    }
}
