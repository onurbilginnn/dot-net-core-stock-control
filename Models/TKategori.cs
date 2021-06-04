using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockControl.Models
{
    public partial class TKategori
    {
        public int KategoriSiraNo { get; set; }
        public string Kategori { get; set; }
        public int KategoriNo { get; set; }
        public string AltKategori { get; set; }
        public int AltKategoriNo { get; set; }
    }
}
