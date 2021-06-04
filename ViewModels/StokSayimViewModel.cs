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
    public partial class StokSayimViewModel
    {
        public IPagedList<TUrunListesi> UrunList { get; set; }
        public IPagedList<TFaturaGiris>  FaturaList { get; set; }
        public IPagedList<TSatis>  SatisList { get; set; }
        public IPagedList<TYillikSayim> YillikSayim { get; set; }
        public IPagedList<TStokDuz> StokDuz { get; set; }

       
    }
}
