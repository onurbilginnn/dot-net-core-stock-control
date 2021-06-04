using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StockControl.Models
{

    public partial class TFaturaGiris
    {
        [NotMapped]
        public int SiraNo { get; set; }
        [JsonProperty("SubeNo")]
        public string SubeNo { get; set; }
        [JsonProperty("FaturaNo")]
        public string FaturaNo { get; set; }
        [JsonProperty("FaturaTarihi")]
        public string FaturaTarihi { get; set; }
        [JsonProperty("IrsaliyeNo")]
        public string IrsaliyeNo { get; set; }
        [JsonProperty("IrsaliyeTarihi")]
        public string IrsaliyeTarihi { get; set; }
        [JsonProperty("TedarikciAdi")]
        public string TedarikciAdi { get; set; }
        [JsonProperty("UrunNo")]
        public string UrunNo { get; set; }
        [JsonProperty("UrunAdi")]
        public string UrunAdi { get; set; }
        [JsonProperty("UrunKategori")]
        public string UrunKategori { get; set; }
        [JsonProperty("UrunAltKategori")]
        public string UrunAltKategori { get; set; }
        [JsonProperty("UrunMiktari")]
        public decimal UrunMiktari { get; set; }
        [JsonProperty("MiktarBirimi")]
        public string MiktarBirimi { get; set; }
        [JsonProperty("BirimFiyatı")]
        public decimal BirimFiyatı { get; set; }
       
        public string FiyatKurTipi { get; set; }
        [JsonProperty("KdvsizToplam")]
        public decimal KdvsizToplam { get; set; }
        [JsonProperty("KdvYuzde")]
        public decimal KdvYuzde { get; set; }

        [JsonProperty("KdvTutar")]
        public decimal KdvTutar { get; set; }

        [JsonProperty("KdvliToplamTutar")]
        public decimal KdvliToplamTutar { get; set; }

        public string KayitTarihi { get; set; }

        public string DegTarihi { get; set; }

        public string KayitKadi { get; set; }

        public string DegKadi { get; set; }
    }
}
