using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StockControl.Models
{
    public partial class TSatis
    {
        [NotMapped]
        public int SiraNo { get; set; }

        public string SatisNo { get; set; }

        [Required(ErrorMessage ="Satış Tarihi seçiniz...")]
        public string SatisTarihi { get; set; }

        [Required(ErrorMessage = "Ürün Adı seçiniz...")]
        public string UrunAdi { get; set; }

        public string UrunNo { get; set; }

        public string UrunSeviyesi { get; set; }

        public string BagUrunSeviyesi { get; set; }

        public decimal SatisAdet { get; set; }

        public string KayitTarihi { get; set; }

        public string DegTarihi { get; set; }

        public int SubeNo { get; set; }

        public string KayitKadi { get; set; }

        public string DegKadi { get; set; }
    }
}
