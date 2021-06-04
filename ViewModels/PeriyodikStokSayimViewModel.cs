using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using StockControl.Models;
using Sakura.AspNetCore;

namespace StockControl.ViewModels
{
    public partial class PeriyodikStokSayimViewModel
    {
        public IPagedList<TUrunListesi> UrunList { get; set; }
        public IPagedList<TFaturaGiris> FaturaListAcilis { get; set; }
        public IPagedList<TFaturaGiris>  FaturaListGiris { get; set; }
        public IPagedList<TSatis> SatisListAcilis { get; set; }
        public IPagedList<TSatis>  SatisListCikis { get; set; }
        public IPagedList<TYillikSayim> YillikSayim { get; set; }
        public IPagedList<TStokDuz> StokDuzAcilis { get; set; }
        public IPagedList<TStokDuz> StokDuzAra { get; set; }

       
    }
}
