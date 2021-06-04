using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StockControl.Models;
using StockControl.Context;
using StockControl.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Sakura.AspNetCore;
using Microsoft.AspNetCore.Http;

namespace StockControl
{
    public class BOMController : Controller
    {
        
        private readonly BAR_ERPContext _context;

        public BOMController(BAR_ERPContext context)
        {
            _context = context;
        }

        // GET: BOM
        [Authorize]
        public IActionResult BOMListesi(int? page, int? pageSize, string fieldSearchString, string searchString)
        {
            int no = page ?? 1;
            int size = pageSize ?? 30;

            var result = _context.TBOM.Where(x => x.SubeNo.ToString()== HttpContext.Session.GetString("SubeNo"));

            if (!String.IsNullOrEmpty(searchString))
            {
                switch (fieldSearchString)
                {
                    case "option1":
                        result = result.Where(s => s.BaglAraUrunNo.Contains(searchString) ||
                       s.BaglBitUrunNo.Contains(searchString)).OrderBy(x => x.KayitTarihi).Distinct();

                        break;
                    case "option2":
                        result = result.Where(s => s.BaglAraUrunAdi.Contains(searchString) ||
                       s.BaglBitUrunAdi.Contains(searchString)).OrderBy(x => x.KayitTarihi).Distinct();
                        break;

                    case "option3":
                        result = result.Where(s => s.KayitTarihi.Contains(searchString)).OrderBy(x => x.KayitTarihi).Distinct();
                        break;

                    default:
                        result = _context.TBOM.Select(x => x).OrderBy(x => x.KayitTarihi);
                        break;
                }
            }
            
                return View(result.ToPagedList(size, no));
            
         
        }

        // GET: BOM/Details/5
        [Authorize]
        public IActionResult Details(string id)
        {
            List<string> miktarBirim = _context.TUrunListesi.Where(x => x.UrunNo == id).Select(y => y.MiktarBirimi).ToList();
            ViewBag.miktarBirim = miktarBirim;

            TBOMEditViewModel editList = new TBOMEditViewModel();
            editList.BOMInfo = _context.TBOM.Where(x => x.BaglBitUrunNo == id || x.BaglAraUrunNo == id).ToArray();
            editList.AllBOM = _context.TBOM.Where(x => x.BaglAraUrunAdi != "").ToArray();
            return View(editList);
            
        }

        // GET: BOM/Create
        [Authorize]
        public async Task<IActionResult> BOMEkle()
        {
            var modelTUrun = _context.TUrunListesi.Select(x => x).OrderBy(x => x.UrunAdi);
            
            return View(await modelTUrun.ToArrayAsync());
        }

        [HttpGet]
        public string GetUrunList()
        {
            var modelTUrun = _context.TUrunListesi.Select(x => x).OrderBy(x => x.UrunAdi);
            List<TUrunListesi> modelTUrun2 = new List<TUrunListesi>();
            var modelTBOM = _context.TBOM.Select(x => x);
            foreach (var item in modelTUrun)
            {
                if (item.SubeNo.ToString() == HttpContext.Session.GetString("SubeNo"))
                {
                    if (item.UrunSeviyesi == "2 - Ara Ürün")
                    {
                        if (!modelTBOM.Select(x => x.BaglAraUrunNo).Contains(item.UrunNo))
                        {
                            modelTUrun2.Add(item);
                        }
                    }
                    else
                    {
                        if (!modelTBOM.Select(x => x.BaglBitUrunNo).Contains(item.UrunNo))
                        {
                            modelTUrun2.Add(item);
                        }
                    }
                }
            }
            return JsonConvert.SerializeObject(modelTUrun2);
        }
        [HttpGet]
        public string GetPriceList()
        {
            int dateNow = DateTime.Now.Month+(DateTime.Now.Year*12);
            int monthInv = 0;
            int yearInv = 0;
            int dateInv = 0;
            var priceList = _context.TFaturaGiris.Select(x => x).ToArray();
            var prices = _context.TFaturaGiris.Select(x => x.FaturaTarihi).ToArray();
            for (int i = 0; i < prices.Length; i++)
            {
                monthInv = Convert.ToInt32(prices[i].Substring(3, 2));
                yearInv = Convert.ToInt32(prices[i].Substring(6,4))*12;
                dateInv = monthInv + yearInv;
                if ((dateNow - dateInv) > 6)
                {
                    priceList[i].BirimFiyatı = 0;
                }
            }
            return JsonConvert.SerializeObject(priceList);
        }

        [HttpPost]
        public string GetAraUrunList([FromBody]string urNo)
        {
            var araUrunInfo = _context.TBOM.Where(x=>x.BaglAraUrunNo==urNo).ToList();
            
            return JsonConvert.SerializeObject(araUrunInfo);
        }
       
        // POST: BOM/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> BOMEkle([FromBody]TBOM[] jsonArrTotal)
        {
            if (ModelState.IsValid)
            {
                foreach (var x in jsonArrTotal)
                {
                    _context.TBOM.Add(x);
                    await _context.SaveChangesAsync();

                }
                return null;
            }
            else
                return Json("Veri kaydedilemedi...");
        }

        // GET: BOM/Edit/5
        [Authorize]
        public IActionResult Edit(string id)
        {
           //var editBOMList = _context.TBOM.Where(x => x.BaglBitUrunNo == id || x.BaglAraUrunNo == id).ToArray();  
           List<string> miktarBirim = _context.TUrunListesi.Where(x => x.UrunNo == id).Select(y=>y.MiktarBirimi).ToList();
            ViewBag.miktarBirim = miktarBirim;

            TBOMEditViewModel editList = new TBOMEditViewModel();            
            editList.BOMInfo= _context.TBOM.Where(x => x.BaglBitUrunNo == id || x.BaglAraUrunNo == id).ToArray();
            editList.AllBOM = _context.TBOM.Where(x=>x.BaglAraUrunAdi!= "").ToArray();
            return View(editList);
        }

        // POST: BOM/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit([FromBody]TBOM[] jsonArrTotal)
        {
            IQueryable<TBOM> removeItems = Enumerable.Empty<TBOM>().AsQueryable();
            if (jsonArrTotal[0].BaglAraUrunNo=="")
            removeItems = _context.TBOM.Where(x => x.BaglBitUrunNo == jsonArrTotal[0].BaglBitUrunNo);
            else
            removeItems = _context.TBOM.Where(x => x.BaglAraUrunNo == jsonArrTotal[0].BaglAraUrunNo );
            if (ModelState.IsValid)
            {

                foreach (var item in removeItems)
                {
                    _context.TBOM.Remove(item);

                }
                await _context.SaveChangesAsync();

                foreach (var x in jsonArrTotal)
                {
                    _context.TBOM.Add(x);
                    await _context.SaveChangesAsync();

                }
                return null;
            }
            else
                return Json("Veri güncellenemedi...");
        }

        // GET: BOM/Delete/5
        [Authorize]
        public IActionResult Delete(string id)
        {
            List<string> miktarBirim = _context.TUrunListesi.Where(x => x.UrunNo == id).Select(y => y.MiktarBirimi).ToList();
            ViewBag.miktarBirim = miktarBirim;

            TBOMEditViewModel editList = new TBOMEditViewModel();
            editList.BOMInfo = _context.TBOM.Where(x => x.BaglBitUrunNo == id || x.BaglAraUrunNo == id).ToArray();
            editList.AllBOM = _context.TBOM.Where(x => x.BaglAraUrunAdi != "").ToArray();
            return View(editList);
        }

        // POST: BOM/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var removeItems = _context.TBOM.Where(m => m.BaglAraUrunNo == id || m.BaglBitUrunNo == id);
            foreach (var item in removeItems)
            {
                _context.TBOM.Remove(item);

            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(BOMListesi));
        }

        [HttpPost]
        public string ReceteUrunler([FromBody]TUrunListesi input)
        {
            List<TUrunListesi> receteUrunList = _context.TUrunListesi.Where(m => m.SubeNo.ToString() == input.SubeNo.ToString() &&
            !m.UrunSeviyesi.Contains("1")).ToList();
            List<string> receteliUrunler = _context.TBOM.Select(x => x.BaglAraUrunNo).ToList();

            foreach (var item in receteUrunList.ToArray())
            {
                if (item.UrunSeviyesi.Contains("Ara"))
                {
                    if (!receteliUrunler.Contains(item.UrunNo))
                    {
                        receteUrunList.Remove(item);
                    }
                }
            }
            return JsonConvert.SerializeObject(receteUrunList);
        }



        private bool TBOMExists(int id)
        {
            return _context.TBOM.Any(e => e.SiraNo == id);
        }
    }
}
