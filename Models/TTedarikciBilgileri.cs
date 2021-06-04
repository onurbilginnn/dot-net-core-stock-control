using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;


namespace StockControl.Models
{
    public partial class TTedarikciBilgileri
    {
        [Required(ErrorMessage = "Alan seçiniz...")]
        public int SubeNo { get; set; }

        [Required(ErrorMessage = "Tedarikçi No oluşturunuz...")]

        public string TedarikciNo { get; set; }

        [Required(ErrorMessage = "Doldurulması zorunlu alan")]
        [Remote(action: "IsSupplierNameExist", controller: "Tedarikci")]
        [MinLength(3, ErrorMessage = "En az 3 harfli bir isim giriniz.")]
        public string TedarikciAdi { get; set; }

        [Required(ErrorMessage = "Doldurulması zorunlu alan")]
        public string VTCKN { get; set; }

        //[Required(ErrorMessage = "Doldurulması zorunlu alan")]
        public string BankaAdi { get; set; }

        //[Required(ErrorMessage = "Doldurulması zorunlu alan")]
        [MinLength(27, ErrorMessage = "IBAN numarası eksik girilmiştir.")]
        public string IBAN { get; set; }

        [Required(ErrorMessage = "Doldurulması zorunlu alan")]
        public string Vade { get; set; }

        public string OdemeSekli { get; set; }

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
