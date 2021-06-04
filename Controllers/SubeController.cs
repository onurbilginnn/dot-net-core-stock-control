using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StockControl.Models;
using StockControl.Context;
using Sakura.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace StockControl.Controllers
{
    public class SubeController : Controller
    {
        private readonly BAR_ERPContext _context;

        public SubeController(BAR_ERPContext context)
        {
            _context = context;
        }



        // GET: Sube
        [Authorize]
        public IActionResult SubeListesi(int? page, int? pageSize, string fieldSearchString, string searchString)
        {
            int no = page ?? 1;
            int size = pageSize ?? 10;

            var result = _context.TSubeBilgileri.Select(x => x);

            if (!String.IsNullOrEmpty(searchString))
            {
                switch (fieldSearchString)
                {
                    case "option1":
                        result = result.Where(s => s.SubeNo.ToString().Contains(searchString));
                        break;
                    case "option2":
                        result = result.Where(s => s.SubeAdi.Contains(searchString));
                        break;
                    case "option3":
                        result = result.Where(s => s.Ulke.Contains(searchString));
                        break;
                    case "option4":
                        result = result.Where(s => s.Sehir.Contains(searchString));
                        break;
                    case "option5":
                        result = result.Where(s => s.Adres.Contains(searchString));
                        break;
                    default:
                        result = _context.TSubeBilgileri.Select(x => x);
                        break;
                }
            }
  
            
                return View(result.ToPagedList(size, no));
            
         
        }

        // GET: Sube/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tSubeBilgileri = await _context.TSubeBilgileri
                .SingleOrDefaultAsync(m => m.SubeNo == id);
            if (tSubeBilgileri == null)
            {
                return NotFound();
            }

            return View(tSubeBilgileri);
        }

        // GET: Sube/Create
        [Authorize]
        public IActionResult Create()
        {
            //ViewData["RowNo"] = _context.TSubeBilgileri.Select(x => x.SubeNo).Count()+1;
            return View("~/Views/Sube/SubeEkle.cshtml");
        }

        // POST: Sube/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("SubeNo,SubeAdi,Ulke,Sehir,Adres,PostaKodu,TelNo,Email,KayitTarihi,DegTarihi,KayitKadi,DegKadi")] TSubeBilgileri tSubeBilgileri)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tSubeBilgileri);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(SubeListesi));
            }
            return View(tSubeBilgileri);
        }

        [HttpPost]
        public int CountSubeNo([FromBody]TSubeBilgileri su)
        {
            int count = _context.TSubeBilgileri.Count(x => x.Ulke == su.Ulke) + 1;
            return count;
        }


        // GET: Sube/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["RowNo"] = _context.TSubeBilgileri.Select(x => x.SubeNo).Count() + 1;
            if (id == null)
            {
                return NotFound();
            }

            var tSubeBilgileri = await _context.TSubeBilgileri.SingleOrDefaultAsync(m => m.SubeNo == id);
            if (tSubeBilgileri == null)
            {
                return NotFound();
            }
            return View(tSubeBilgileri);
        }

        // POST: Sube/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("SubeNo,SubeAdi,Ulke,Sehir,Adres,PostaKodu,TelNo,Email,KayitTarihi,DegTarihi,KayitKadi,DegKadi")] TSubeBilgileri tSubeBilgileri)
        {
            if (id != tSubeBilgileri.SubeNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tSubeBilgileri);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TSubeBilgileriExists(tSubeBilgileri.SubeNo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(SubeListesi));
            }
            return View(tSubeBilgileri);
        }

        // GET: Sube/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tSubeBilgileri = await _context.TSubeBilgileri
                .SingleOrDefaultAsync(m => m.SubeNo == id);
            if (tSubeBilgileri == null)
            {
                return NotFound();
            }

            return View(tSubeBilgileri);
        }

        // POST: Sube/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tSubeBilgileri = await _context.TSubeBilgileri.SingleOrDefaultAsync(m => m.SubeNo == id);
            _context.TSubeBilgileri.Remove(tSubeBilgileri);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(SubeListesi));
        }

        private bool TSubeBilgileriExists(int id)
        {
            return _context.TSubeBilgileri.Any(e => e.SubeNo == id);
        }
    }
}
