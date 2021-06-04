using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StockControl.Models
{
    public partial class TStokDuz
    {
        [NotMapped]
        public int SiraNo { get; set; }

        public string UrunAdi { get; set; }

        public string UrunNo { get; set; }

        public string DuzKadi { get; set; }

        public decimal DuzAdedi { get; set; }

        public string DuzTarihi { get; set; }

        public string SubeNo { get; set; }

        public string KayitTarihi { get; set; }
    }
}
