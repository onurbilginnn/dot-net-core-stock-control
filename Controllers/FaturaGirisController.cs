using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StockControl.Models;
using StockControl.Context;
using System.Web;
using Newtonsoft.Json;
using Sakura.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace StockControl
{
    public class FaturaGirisController : Controller
    {
       
        List<TUrunListesi> UrunList = new List<TUrunListesi>();
        List<TFaturaGiris> EditUrunList = new List<TFaturaGiris>();
        List<TTedarikciBilgileri> TedarikciList = new List<TTedarikciBilgileri>();

        private readonly BAR_ERPContext _context;

        public FaturaGirisController(BAR_ERPContext context)
        {
            _context = context;
        }

        // GET: FaturaGiris
        [Authorize]
        public IActionResult FaturaListesi(int? page, int? pageSize, string fieldSearchString, string searchString)
        {
            int no = page ?? 1;
            int size = pageSize ?? 30;

            var result = _context.TFaturaGiris.Where(x => x.SubeNo.ToString() == HttpContext.Session.GetString("SubeNo"));

            if (!String.IsNullOrEmpty(searchString))
            {
                switch (fieldSearchString)
                {
                    case "option1":
                        result = result.Where(s => s.SubeNo.ToString().Contains(searchString)).OrderBy(x => x.FaturaTarihi).Distinct();
                        break;
                    case "option2":
                        result = result.Where(s => s.FaturaNo.Contains(searchString)).OrderBy(x => x.FaturaTarihi).Distinct();
                        break;

                    case "option3":
                        result = result.Where(s => s.FaturaTarihi.Contains(searchString)).OrderBy(x => x.FaturaTarihi).Distinct();
                        break;
                    case "option4":
                        result = result.Where(s => s.TedarikciAdi.Contains(searchString)).OrderBy(x => x.FaturaTarihi).Distinct();
                        break;
                    default:
                        result = _context.TFaturaGiris.Select(x => x).OrderBy(x => x.FaturaTarihi);
                        break;
                }
            }
           
                return View(result.ToPagedList(size, no));
           
       
        }

        // GET: FaturaGiris/Details/5
        [Authorize]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TFaturaGiris[] tFaturaGiris = await _context.TFaturaGiris
                .Where(m => m.FaturaNo == id).Select(x => x).ToArrayAsync();

            if (tFaturaGiris == null)
            {
                return NotFound();
            }

            return View(tFaturaGiris);
        }

        // GET: FaturaGiris/Create
        [Authorize]
        public IActionResult Create()
        {
            return View("~/Views/FaturaGiris/FaturaEkle.cshtml");
        }
        [HttpGet]
        public string GetUrunList()
        {
            UrunList = _context.TUrunListesi.Where(y => !y.UrunSeviyesi.Contains("1") && !y.UrunSeviyesi.Contains("2")).
                OrderBy(z=>z.UrunAdi).ToList();

            return JsonConvert.SerializeObject(UrunList);
        }

        [HttpGet]
        public string GetTedarikciList()
        {
            TedarikciList = _context.TTedarikciBilgileri.Select(y => y).ToList();

            return JsonConvert.SerializeObject(TedarikciList);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody]TFaturaGiris[] jsonArrTotal)
        {
     
            if (ModelState.IsValid)
            {
                foreach (var x in jsonArrTotal)
                {
                    _context.TFaturaGiris.Add(x);
                    await _context.SaveChangesAsync();

                }
                return null;
            }
            else
                return Json("Veri kaydedilemedi...");
        }

        // GET: FaturaGiris/Edit/5
        [Authorize]
        public IActionResult Edit(string id)
        {
          var model = _context.TUrunListesi.Where(y => !y.UrunSeviyesi.Contains("1") && !y.UrunSeviyesi.Contains("2")).ToArray();
            EditUrunList = _context.TFaturaGiris.Where(x => x.FaturaNo == id).Select(x => x).ToList();
            ViewBag.EditUrunList = EditUrunList;

            return View("~/Views/FaturaGiris/Edit.cshtml",model);
        }


        // POST: FaturaGiris/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit([FromBody]TFaturaGiris[] jsonArrTotal)
        {
            var removeItems = _context.TFaturaGiris.Where(x => x.FaturaNo == jsonArrTotal[0].FaturaNo);
            if (ModelState.IsValid)
            {

                foreach (var item in removeItems)
                {
                    _context.TFaturaGiris.Remove(item);

                }
                await _context.SaveChangesAsync();

                foreach (var x in jsonArrTotal)
                {
                    _context.TFaturaGiris.Add(x);
                    await _context.SaveChangesAsync();

                }
                return null;
            }
            else
                return Json("Veri güncellenemedi...");
        }

        // GET: FaturaGiris/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TFaturaGiris[] tFaturaGiris = await _context.TFaturaGiris
                .Where(m => m.FaturaNo == id).Select(x => x).ToArrayAsync();

            if (tFaturaGiris == null)
            {
                return NotFound();
            }

            return View(tFaturaGiris);
        }

        // POST: FaturaGiris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var removeItems = _context.TFaturaGiris.Where(x => x.FaturaNo == id);
            foreach (var item in removeItems)
            {
                _context.TFaturaGiris.Remove(item);

            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(FaturaListesi));
        }


        private bool TFaturaGirisExists(string id)
        {
            return _context.TFaturaGiris.Any(e => e.FaturaNo == id);
        }
    }
}
