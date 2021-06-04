using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using StockControl.Models;

namespace StockControl.ViewModels
{
    public partial class TBOMEditViewModel
    {
        public TBOM[] BOMInfo{ get; set; }
        public TBOM[] AllBOM { get; set; }
    }
}
