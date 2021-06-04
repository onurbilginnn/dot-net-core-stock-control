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
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace StockControl
{
    public class TedarikciController : Controller
    {
        private readonly BAR_ERPContext _context;

        public TedarikciController(BAR_ERPContext context)
        {
            _context = context;
        }

        // GET: TTedarikciBilgileri
        [Authorize]
        public IActionResult TedarikçiListesi(int? page, int? pageSize, string fieldSearchString, string searchString)
        {
            int no = page ?? 1;
            int size = pageSize ?? 10;
            var result = _context.TTedarikciBilgileri.Where(x => x.SubeNo.ToString() == HttpContext.Session.GetString("SubeNo"));
            if (!String.IsNullOrEmpty(searchString))
            {
                switch (fieldSearchString)
                {
                    case "option1":
                        result = result.Where(s => s.TedarikciNo.Contains(searchString));
                        break;
                    case "option2":
                        result = result.Where(s => s.TedarikciAdi.Contains(searchString));
                        break;
                    case "option3":
                        result = result.Where(s => s.VTCKN.Contains(searchString));
                        break;
                    case "option4":
                        result = result.Where(s => s.Vade.Contains(searchString));
                        break;

                    default:
                        result = _context.TTedarikciBilgileri.Select(x => x);
                        break;
                }
            }
           
                return View(result.ToPagedList(size, no));
            
           
        }

        // GET: TTedarikciBilgileri/Details/5
        [Authorize]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tTedarikciBilgileri = await _context.TTedarikciBilgileri
                .SingleOrDefaultAsync(m => m.TedarikciNo == id);
            if (tTedarikciBilgileri == null)
            {
                return NotFound();
            }

            return View(tTedarikciBilgileri);
        }
        [HttpGet]
        public int GetTedarikciList()
        {
            List<TTedarikciBilgileri> tedarikciListesi = _context.TTedarikciBilgileri.Select(x => x).ToList();
            int say = tedarikciListesi.Select(x => x.TedarikciNo).Count() + 1;
            return say;
        }

        // GET: TTedarikciBilgileri/Create
        [Authorize]
        public IActionResult Create()
        {
            
           
            return View("~/Views/Tedarikci/TedarikciEkle.cshtml");
        }

        // POST: TTedarikciBilgileri/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("SubeNo,TedarikciNo,TedarikciAdi,VTCKN,BankaAdi,IBAN,Vade,OdemeSekli,Ulke,Sehir,Adres,PostaKodu,TelNo,Email,KayitTarihi,DegTarihi,KayitKadi,DegKadi")] TTedarikciBilgileri tTedarikciBilgileri)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tTedarikciBilgileri);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(TedarikçiListesi));
            }
            return View(tTedarikciBilgileri);
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult IsSupplierNameExist(string tedarikciAdi)
        {
            List<string> tedarikciAdiList = (from ua in _context.TTedarikciBilgileri
                                             where ua.SubeNo.ToString()== HttpContext.Session.GetString("SubeNo")
                                             select ua.TedarikciAdi).ToList();


            var validateName = tedarikciAdiList.IndexOf(tedarikciAdi);

            if (validateName != -1)
            {
                return Json($"{tedarikciAdi} kullanılmaktadır.Başka bir isim giriniz.");
            }


            return Json(true);
        }
        // GET: TTedarikciBilgileri/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tTedarikciBilgileri = await _context.TTedarikciBilgileri.SingleOrDefaultAsync(m => m.TedarikciNo == id);
            if (tTedarikciBilgileri == null)
            {
                return NotFound();
            }
            return View(tTedarikciBilgileri);
        }

        // POST: TTedarikciBilgileri/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(string id, [Bind("SubeNo,TedarikciNo,TedarikciAdi,VTCKN,BankaAdi,IBAN,Vade,OdemeSekli,Ulke,Sehir,Adres,PostaKodu,TelNo,Email,KayitTarihi,DegTarihi,KayitKadi,DegKadi")] TTedarikciBilgileri tTedarikciBilgileri)
        {
            if (id != tTedarikciBilgileri.TedarikciNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tTedarikciBilgileri);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TTedarikciBilgileriExists(tTedarikciBilgileri.TedarikciNo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(TedarikçiListesi));
            }
            return View(tTedarikciBilgileri);
        }

        // GET: TTedarikciBilgileri/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tTedarikciBilgileri = await _context.TTedarikciBilgileri
                .SingleOrDefaultAsync(m => m.TedarikciNo == id);
            if (tTedarikciBilgileri == null)
            {
                return NotFound();
            }

            return View(tTedarikciBilgileri);
        }

        // POST: TTedarikciBilgileri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tTedarikciBilgileri = await _context.TTedarikciBilgileri.SingleOrDefaultAsync(m => m.TedarikciNo == id);
            _context.TTedarikciBilgileri.Remove(tTedarikciBilgileri);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(TedarikçiListesi));
        }

        private bool TTedarikciBilgileriExists(string id)
        {
            return _context.TTedarikciBilgileri.Any(e => e.TedarikciNo == id);
        }


    }
}
