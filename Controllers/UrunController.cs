using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StockControl.Models;
using StockControl.Context;
using System.Net.Http;
using Sakura.AspNetCore;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;


namespace StockControl
{
    public class UrunController : Controller
    {
        List<TKategori> KategoriList = new List<TKategori>();
        List<string> UrunAdiList = new List<string>();


        private readonly BAR_ERPContext _context;

        public UrunController(BAR_ERPContext context)
        {
            _context = context;
        }

        // GET: Urun
        [Authorize]
        public IActionResult UrunListesi(int? page, int? pageSize, string fieldSearchString, string searchString)
        {
            int no = page ?? 1;
            int size = pageSize ?? 10;


            var result = _context.TUrunListesi.Where(x => x.SubeNo.ToString() == HttpContext.Session.GetString("SubeNo"));

            if (!String.IsNullOrEmpty(searchString))
            {
                switch (fieldSearchString)
                {
                    case "option1":
                        result = result.Where(s => s.SubeNo.ToString().Contains(searchString));
                        break;
                    case "option2":
                        result = result.Where(s => s.UrunNo.Contains(searchString));
                        break;
                    case "option3":
                        result = result.Where(s => s.UrunAdi.Contains(searchString));
                        break;
                    case "option4":
                        result = result.Where(s => s.UrunKategori.Contains(searchString));
                        break;
                    case "option5":
                        result = result.Where(s => s.UrunSeviyesi.Contains(searchString));
                        break;
                    default:
                        result = _context.TUrunListesi.Select(x => x);
                        break;
                }
            }
          
                return View(result.ToPagedList(size, no));
           
          
        }


        // GET: Urun/Details/5
        [Authorize]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tUrunListesi = await _context.TUrunListesi
                .SingleOrDefaultAsync(m => m.UrunNo == id);
            if (tUrunListesi == null)
            {
                return NotFound();
            }

            return View(tUrunListesi);
        }


        // GET: Urun/Create
        [Authorize]
        public IActionResult Create()
        {
           return View("~/Views/Urun/UrunEkle.cshtml");
        }

        [HttpGet]
        public string GetKategoriList()
        {
            KategoriList = (from kategori in _context.TKategori select kategori).ToList();
            KategoriList = KategoriList.OrderBy(x => x.AltKategori).ToList();

            return JsonConvert.SerializeObject(KategoriList);
        }
        // POST: Urun/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("UrunNo,UrunAdi,UrunKategori,UrunAltKategori,MiktarBirimi,KdvOrani,SubeNo,UrunSeviyesi,KayitTarihi,DegTarihi,KayitKadi,DegKadi")] TUrunListesi tUrunListesi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tUrunListesi);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(UrunListesi));
            }
            return View(tUrunListesi);
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult IsProductNameExist(string urunAdi, string edit)
        {
            UrunAdiList = (from ua in _context.TUrunListesi
                           where ua.SubeNo.ToString() == HttpContext.Session.GetString("SubeNo")
                           select ua.UrunAdi).ToList();


            if (edit == "Create")
            {
                var validateName = UrunAdiList.IndexOf(urunAdi);

                if (validateName != -1)
                {
                    return Json($"{urunAdi} kullanılmaktadır.Başka bir isim giriniz.");
                }
            }

            return Json(true);
        }


        [HttpPost]
        public int CountUrunNo([FromBody]TUrunListesi ur)
        {

            int count = _context.TUrunListesi.Count(x => x.UrunNo.Substring(0, 11) == ur.UrunNo.Substring(0, 11)) + 1;
            return count;
        }


        // GET: Urun/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
           
            ViewData["RowNo"] = _context.TUrunListesi.Select(x => x.UrunNo).Count() + 1;

            if (id == null)
            {
                return NotFound();
            }

            var tUrunListesi = await _context.TUrunListesi.SingleOrDefaultAsync(m => m.UrunNo == id);
            if (tUrunListesi == null)
            {
                return NotFound();
            }

            return View(tUrunListesi);
        }

        // POST: Urun/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(string id, [Bind("UrunNo,UrunAdi,UrunKategori,UrunAltKategori,MiktarBirimi,KdvOrani,SubeNo,UrunSeviyesi,KayitTarihi,DegTarihi,KayitKadi,DegKadi")] TUrunListesi tUrunListesi)
        {
            TUrunListesi x = new TUrunListesi();
            if (id != tUrunListesi.UrunNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tUrunListesi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TUrunListesiExists(tUrunListesi.UrunNo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(UrunListesi));
            }

            return View(tUrunListesi);
        }

        // GET: Urun/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tUrunListesi = await _context.TUrunListesi
                .SingleOrDefaultAsync(m => m.UrunNo == id);
            if (tUrunListesi == null)
            {
                return NotFound();
            }

            return View(tUrunListesi);
        }

        // POST: Urun/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tUrunListesi = await _context.TUrunListesi.SingleOrDefaultAsync(m => m.UrunNo == id);
            _context.TUrunListesi.Remove(tUrunListesi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(UrunListesi));
        }

        private bool TUrunListesiExists(string id)
        {
            return _context.TUrunListesi.Any(e => e.UrunNo == id);
        }
    }
}
