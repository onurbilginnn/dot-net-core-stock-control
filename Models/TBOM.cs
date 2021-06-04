using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace StockControl.Models
{
    public partial class TBOM
    {
        [NotMapped]
        public int SiraNo { get; set; }

        public string SubeNo { get; set; }

        public string UrunSeviyesi { get; set; }

        public decimal FireOrani { get; set; }

        public string UrunNo { get; set; }

        public string UrunAdi { get; set; }

        public decimal UrunMiktari { get; set; }

        public decimal FireliMiktar { get; set; }

        public string MiktarBirimi { get; set; }

        public string FiyatKurTipi { get; set; }

        public string BaglAraUrunNo { get; set; }

        public string BaglAraUrunAdi { get; set; }

        public string BaglBitUrunNo { get; set; }

        public string BaglBitUrunAdi { get; set; }

        public decimal BaglRefMik { get; set; }

        public string KayitTarihi { get; set; }

        public string DegTarihi { get; set; }

        public string KayitKadi { get; set; }

        public string DegKadi { get; set; }



    }
}
