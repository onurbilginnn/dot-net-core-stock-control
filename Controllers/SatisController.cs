using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StockControl.Context;
using StockControl.Models;
using Newtonsoft.Json;
using Sakura.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace StockControl
{
    public class SatisController : Controller
    {
        private readonly BAR_ERPContext _context;

        public SatisController(BAR_ERPContext context)
        {
            _context = context;
        }

        // GET: TSatis
        [Authorize]
        public IActionResult SatisListesi(int? page, int? pageSize, string fieldSearchString, string searchString)
        {
            int no = page ?? 1;
            int size = pageSize ?? 100;

            var result = _context.TSatis.Where(x => x.SubeNo.ToString() == HttpContext.Session.GetString("SubeNo"));

            if (!String.IsNullOrEmpty(searchString))
            {
                switch (fieldSearchString)
                {
                   
                    case "option2":
                        result = result.Where(s => s.SatisNo.Contains(searchString)).OrderBy(x => x.SatisTarihi).Distinct();
                        break;

                    case "option3":
                        result = result.Where(s => s.SatisTarihi.Contains(searchString)).OrderBy(x => x.SatisTarihi).Distinct();
                        break;
                  
                    default:
                        result = _context.TSatis.Select(x => x).OrderBy(x => x.SatisTarihi);
                        break;
                }
            }
           
                return View(result.ToPagedList(size, no));
            
            
        }

        // GET: TSatis/Details/5
        [Authorize]
        public async Task<IActionResult> Details(string id)
        {
            {
                if (id == null)
                {
                    return NotFound();
                }

                TSatis[] tSatis = await _context.TSatis
                    .Where(m => m.SatisNo == id).Select(x => x).ToArrayAsync();

                if (tSatis == null)
                {
                    return NotFound();
                }

                return View(tSatis);
            }

        }

        // GET: TSatis/Create
        [Authorize]
        public IActionResult Create()
        {
            List<TBOM> model = _context.TBOM.Where(x =>!string.IsNullOrEmpty(x.BaglBitUrunNo)).ToList();

            return View("~/Views/Satis/SatisEkle.cshtml",model);
        }

        // POST: TSatis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody]TSatis[] jsonArrTotal)
        {
            if (ModelState.IsValid)
            {
                foreach (var x in jsonArrTotal)
                {
                    _context.TSatis.Add(x);
                    await _context.SaveChangesAsync();

                }
                return null;
            }
            else
                return Json("Veri kaydedilemedi...");
        }
        [HttpPost]
        public string GetBitUrunBilgileri([FromBody]string urAd)
        {
            var araUrunInfo = _context.TUrunListesi.Where(x => x.UrunAdi == urAd).ToList();

            return JsonConvert.SerializeObject(araUrunInfo);
        }
        [HttpPost]
        public string GetAltUrunBilgileri([FromBody]string urNo)
        {
            var araUrunInfo = _context.TBOM.Where(x => x.BaglBitUrunNo == urNo).ToList();

            return JsonConvert.SerializeObject(araUrunInfo);
        }
        [HttpPost]
        public string GetAltAltUrunBilgileri([FromBody]string urNo)
        {
            var araUrunInfo = _context.TBOM.Where(x => x.BaglAraUrunNo == urNo).ToList();

            return JsonConvert.SerializeObject(araUrunInfo);
        }
        [HttpGet]
        public string GetBOMBitUrun()
        {
            var bitUrunList = _context.TBOM.Where(x => x.SubeNo.ToString()== HttpContext.Session.GetString("SubeNo") && x.BaglBitUrunAdi!="")
                .Select(y=>y.BaglBitUrunAdi).Distinct().ToList();

            return JsonConvert.SerializeObject(bitUrunList);
        }
        [HttpGet]
        public int CountSatis()
        {
            int count = _context.TSatis.Select(x => x.SatisNo).Distinct().Count() + 1;
            return count;
        }
        [HttpGet]
        public string GetUrunDBBilgileri()
        {
           var urunList = _context.TUrunListesi.Select(x => x).ToList();
            return JsonConvert.SerializeObject(urunList);
        }
        // GET: TSatis/Edit/5
        [Authorize]
        public IActionResult Edit(string id)
        {
            var model = _context.TSatis.Where(y => y.SatisNo==id).ToArray();           

            return View("~/Views/Satis/Edit.cshtml", model);
        }

        // POST: TSatis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit([FromBody]TSatis[] jsonArrTotal)
        {
            var removeItems = _context.TSatis.Where(x => x.SatisNo == jsonArrTotal[0].SatisNo);
            if (ModelState.IsValid)
            {

                foreach (var item in removeItems)
                {
                    _context.TSatis.Remove(item);

                }
                await _context.SaveChangesAsync();

                foreach (var x in jsonArrTotal)
                {
                    _context.TSatis.Add(x);
                    await _context.SaveChangesAsync();

                }
                return null;
            }
            else
                return Json("Veri güncellenemedi...");
        }

        // GET: TSatis/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TSatis[] tSatis = await _context.TSatis
                .Where(m => m.SatisNo == id).Select(x => x).ToArrayAsync();

            if (tSatis == null)
            {
                return NotFound();
            }

            return View(tSatis);
        }

        // POST: TSatis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var removeItems = _context.TSatis.Where(x => x.SatisNo.ToString() == id);
            foreach (var item in removeItems)
            {
                _context.TSatis.Remove(item);

            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(SatisListesi));
        }

        private bool TSatisExists(string id)
        {
            return _context.TSatis.Any(e => e.SatisNo == id);
        }
    }
}
