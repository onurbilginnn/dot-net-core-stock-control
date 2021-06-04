using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;


namespace StockControl.Models
{
    public partial class TUrunListesi
    {
        [Required(ErrorMessage = "Ürün No oluşturunuz...")]
        public string UrunNo { get; set; }

        [Required(ErrorMessage = "Doldurulması zorunlu alan")]
        [Remote(action: "IsProductNameExist", controller: "Urun", AdditionalFields = "edit")]
        [MinLength(3,ErrorMessage ="En az 3 harfli bir isim giriniz.")]
        public string UrunAdi { get; set; }

        [Required(ErrorMessage = "Alan seçiniz...")]
        public string UrunKategori { get; set; }

        [Required(ErrorMessage = "Alan seçiniz...")]
        public string UrunAltKategori { get; set; }

        [Required(ErrorMessage = "Alan seçiniz...")]
        public string MiktarBirimi { get; set; }

        [Required(ErrorMessage = "Alan seçiniz...")]
        public decimal? KdvOrani { get; set; }

        public int SubeNo { get; set; }

        [Required(ErrorMessage = "Alan seçiniz...")]
        public string UrunSeviyesi { get; set; }

        public string KayitTarihi { get; set; }

        public string DegTarihi { get; set; }

        public string KayitKadi { get; set; }

        public string DegKadi { get; set; }

    }
}
