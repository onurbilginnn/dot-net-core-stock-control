using System;
using System.Collections.Generic;
using System.Data.SqlClient;

using System.ComponentModel.DataAnnotations;


namespace StockControl.Models
{

    public partial class TSubeBilgileri
    {

        public int SubeNo { get; set; }

        [Required(ErrorMessage = "Doldurulması zorunlu alan")]
        public string SubeAdi { get; set; }

        [Required(ErrorMessage = "Doldurulması zorunlu alan")]
        public string Ulke { get; set; }

        [Required(ErrorMessage = "Doldurulması zorunlu alan")]
        public string Sehir { get; set; }

        [Required(ErrorMessage = "Doldurulması zorunlu alan")]
        public string Adres { get; set; }

        public string PostaKodu { get; set; }

        [Required(ErrorMessage = "Doldurulması zorunlu alan")]
        [MinLength(13, ErrorMessage = "Telefon numarası eksik girilmiştir.")]
        public string TelNo { get; set; }

        [Required(ErrorMessage = "Doldurulması zorunlu alan")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi değil.")]
        public string Email { get; set; }

        public string KayitTarihi { get; set; }

        public string DegTarihi { get; set; }

        public string KayitKadi { get; set; }

        public string DegKadi { get; set; }

    }
}
