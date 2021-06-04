using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace StockControl.Models
{
    public partial class TYillikSayim
    {
        [NotMapped]     
        public int SiraNo { get; set; }

        public string UrunAdi { get; set; }

        public string UrunNo { get; set; }

       public decimal Acilis { get; set; }

        public decimal Giris { get; set; }

        public decimal Cikis { get; set; }

        public decimal Duzeltme { get; set; }

        public decimal Bakiye { get; set; }

        public decimal FiiliSayim { get; set; }

        public decimal Fark { get; set; }

        public string StokDonemiYil { get; set; }

        public string KayitKadi { get; set; }

        public string SubeNo { get; set; }

        public string KayitTarihi { get; set; }


    }
}
