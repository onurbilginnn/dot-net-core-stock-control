using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StockControl.Context;
using StockControl.Models;
using StockControl.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Sakura.AspNetCore;
using Newtonsoft.Json;


namespace StockControl
{
    public class StokSayimController : Controller
    {
        
        public static string basTarihi = "";
        public static string bitTarihi = "";
        public static string stokSayimDonemi = "";
        private readonly BAR_ERPContext _context;

        public StokSayimController(BAR_ERPContext context)
        {
            _context = context;
        }

        // GET: TStokDuz
        [Authorize]
        public IActionResult StokRaporuGiris()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public int StokRaporuGiris([FromBody]string[] sendArr)
        {
            int basYil,basAy,basGun, bitYil, bitAy, bitGun = 0;
            var basArr = sendArr[0].Split("-");
            var bitArr= sendArr[1].Split("-");

            basYil =Convert.ToInt32(basArr[0]);
            basAy= Convert.ToInt32(basArr[1]);
            basGun= Convert.ToInt32(basArr[2]);
            bitYil = Convert.ToInt32(bitArr[0]);
            bitAy = Convert.ToInt32(bitArr[1]);
            bitGun = Convert.ToInt32(bitArr[2]);

            basTarihi = basGun + "-" + basAy + "-" + basYil;
            bitTarihi = bitGun + "-" + bitAy + "-" + bitYil;
            stokSayimDonemi = bitYil.ToString();
            var stokAcilisList = _context.YillikSayim.Select(x =>x.StokDonemiYil);
            
            if (stokAcilisList.Contains(bitYil.ToString()))
                return 1;
            else
                return -1;
        }

        [Authorize]
        public IActionResult StokYilSayim(int? page, int? pageSize,string searchString)
        {
            int no = page ?? 1;
            int size = pageSize ?? 10;

            var gecmisYil = Convert.ToInt32(stokSayimDonemi) - 1;
            var model = new StokSayimViewModel();
            if (!string.IsNullOrEmpty(searchString))
            {
                model.UrunList = _context.TUrunListesi.Where(x => x.UrunSeviyesi == "3 - Hammadde" && x.UrunAdi.Contains(searchString) && x.SubeNo.ToString() == HttpContext.Session.GetString("SubeNo")).ToPagedList(size, no);
                model.FaturaList = _context.TFaturaGiris.Where(x => Convert.ToDateTime(x.FaturaTarihi).Year.ToString() == gecmisYil.ToString()).ToPagedList(size, no);
                model.SatisList = _context.TSatis.Where(x => Convert.ToDateTime(x.SatisTarihi).Year.ToString() == gecmisYil.ToString()).ToPagedList(size, no);
                model.YillikSayim = _context.YillikSayim.Where(x => x.StokDonemiYil == gecmisYil.ToString()).ToPagedList(size, no);
                model.StokDuz = _context.StokDuz.Where(x => Convert.ToDateTime(x.DuzTarihi).Year.ToString() == gecmisYil.ToString()).ToPagedList(size, no);

            }
            else
            {
                model.UrunList = _context.TUrunListesi.Where(x => x.UrunSeviyesi == "3 - Hammadde" && x.SubeNo.ToString() == HttpContext.Session.GetString("SubeNo")).ToPagedList(size, no);
                model.FaturaList = _context.TFaturaGiris.Where(x => Convert.ToDateTime(x.FaturaTarihi).Year.ToString() == gecmisYil.ToString()).ToPagedList(size, no);
                model.SatisList = _context.TSatis.Where(x => Convert.ToDateTime(x.SatisTarihi).Year.ToString() == gecmisYil.ToString()).ToPagedList(size, no);
                model.YillikSayim = _context.YillikSayim.Where(x => x.StokDonemiYil == gecmisYil.ToString()).ToPagedList(size, no);
                model.StokDuz = _context.StokDuz.Where(x => Convert.ToDateTime(x.DuzTarihi).Year.ToString() == gecmisYil.ToString()).ToPagedList(size, no);

            }
            ViewBag.StokDonem =stokSayimDonemi;

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> StokYilSayim([FromBody]TYillikSayim[] jsonArrTotal)
        {
            if (ModelState.IsValid)
            {
                foreach (var x in jsonArrTotal)
                {
                    _context.YillikSayim.Add(x);
                    await _context.SaveChangesAsync();

                }
                return null;
            }
            else
            {
                var errors = ModelState
             .Where(x => x.Value.Errors.Count > 0)
             .Select(x => new { x.Key, x.Value.Errors })
            .ToArray();
                return Json(errors);
            }
        }

        [Authorize]
        public IActionResult StokYilSayimListesi(int? page, int? pageSize,string searchString)
        {
            int no = page ?? 1;
            int size = pageSize ?? 10;
            var result = _context.YillikSayim.Where(x => x.SubeNo.ToString() == HttpContext.Session.GetString("SubeNo"));
            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(s => s.StokDonemiYil.Contains(searchString)).OrderBy(x => x.KayitTarihi).Distinct();
            }
                
            return View(result.ToPagedList(size, no));
        }

        [Authorize]
        public IActionResult StokYilSayimDetaylar( int? page, int? pageSize, string id, string searchString)
        {
            int no = page ?? 1;
            int size = pageSize ?? 10;
          
            var model = new StokSayimViewModel();
           
                model.UrunList = _context.TUrunListesi.Where(x => x.UrunSeviyesi == "3 - Hammadde" && x.SubeNo.ToString() == HttpContext.Session.GetString("SubeNo")).ToPagedList(size, no);
            if (!string.IsNullOrEmpty(searchString))
                model.YillikSayim = _context.YillikSayim.Where(x => x.StokDonemiYil == id && x.SubeNo.ToString() == HttpContext.Session.GetString("SubeNo")
                 && x.UrunAdi.Contains(searchString)).ToPagedList(size, no);

            else
                model.YillikSayim = _context.YillikSayim.Where(x => x.StokDonemiYil == id && x.SubeNo.ToString() == HttpContext.Session.GetString("SubeNo")).ToPagedList(size, no);

            return View(model);
        }

        [Authorize]
        public IActionResult StokPeriyodikSayim(int? page, int? pageSize, string searchString)
        {
            int no = page ?? 1;
            int size = pageSize ?? 10;

            DateTime basTarihiDeger = Convert.ToDateTime(basTarihi);
            DateTime bitTarihiDeger = Convert.ToDateTime(bitTarihi);

            var model = new PeriyodikStokSayimViewModel();
            if (!string.IsNullOrEmpty(searchString))
            {
                model.UrunList = _context.TUrunListesi.Where(x => x.UrunSeviyesi == "3 - Hammadde" && x.UrunAdi.Contains(searchString) && x.SubeNo.ToString() == HttpContext.Session.GetString("SubeNo")).ToPagedList(size, no);
                model.FaturaListAcilis = _context.TFaturaGiris.Where(x => Convert.ToDateTime(x.FaturaTarihi) < basTarihiDeger
             && Convert.ToDateTime(x.FaturaTarihi).Year.ToString() == stokSayimDonemi).ToPagedList(size, no);

                model.FaturaListGiris = _context.TFaturaGiris.Where(x => Convert.ToDateTime(x.FaturaTarihi) >= basTarihiDeger
                 && Convert.ToDateTime(x.FaturaTarihi) <= bitTarihiDeger).ToPagedList(size, no);

                model.SatisListAcilis = _context.TSatis.Where(x => Convert.ToDateTime(x.SatisTarihi) < basTarihiDeger && Convert.ToDateTime(x.SatisTarihi).Year.ToString() == stokSayimDonemi).ToPagedList(size, no);

                model.SatisListCikis = _context.TSatis.Where(x => Convert.ToDateTime(x.SatisTarihi) >= basTarihiDeger
                && Convert.ToDateTime(x.SatisTarihi) <= bitTarihiDeger).ToPagedList(size, no);

                model.YillikSayim = _context.YillikSayim.Where(x => x.StokDonemiYil == stokSayimDonemi.ToString()).ToPagedList(size, no);

                model.StokDuzAcilis = _context.StokDuz.Where(x => Convert.ToDateTime(x.DuzTarihi) < basTarihiDeger
               && Convert.ToDateTime(x.DuzTarihi).Year.ToString() == stokSayimDonemi).ToPagedList(size, no);

                model.StokDuzAra = _context.StokDuz.Where(x => Convert.ToDateTime(x.DuzTarihi) >= basTarihiDeger
                 && Convert.ToDateTime(x.DuzTarihi) <= bitTarihiDeger).ToPagedList(size, no);
            }
            else
            {
                model.UrunList = _context.TUrunListesi.Where(x => x.UrunSeviyesi == "3 - Hammadde" && x.SubeNo.ToString() == HttpContext.Session.GetString("SubeNo")).ToPagedList(size, no);
                model.FaturaListAcilis = _context.TFaturaGiris.Where(x => Convert.ToDateTime(x.FaturaTarihi) < basTarihiDeger
             && Convert.ToDateTime(x.FaturaTarihi).Year.ToString() == stokSayimDonemi).ToPagedList(size, no);

                model.FaturaListGiris = _context.TFaturaGiris.Where(x => Convert.ToDateTime(x.FaturaTarihi) >= basTarihiDeger
                 && Convert.ToDateTime(x.FaturaTarihi) <= bitTarihiDeger).ToPagedList(size, no);

                model.SatisListAcilis = _context.TSatis.Where(x => Convert.ToDateTime(x.SatisTarihi) < basTarihiDeger && Convert.ToDateTime(x.SatisTarihi).Year.ToString() == stokSayimDonemi).ToPagedList(size, no);

                model.SatisListCikis = _context.TSatis.Where(x => Convert.ToDateTime(x.SatisTarihi) >= basTarihiDeger
                && Convert.ToDateTime(x.SatisTarihi) <= bitTarihiDeger).ToPagedList(size, no);

                model.YillikSayim = _context.YillikSayim.Where(x => x.StokDonemiYil == stokSayimDonemi.ToString()).ToPagedList(size, no);

                model.StokDuzAcilis = _context.StokDuz.Where(x => Convert.ToDateTime(x.DuzTarihi) < basTarihiDeger
               && Convert.ToDateTime(x.DuzTarihi).Year.ToString() == stokSayimDonemi).ToPagedList(size, no);

                model.StokDuzAra = _context.StokDuz.Where(x => Convert.ToDateTime(x.DuzTarihi) >= basTarihiDeger
                 && Convert.ToDateTime(x.DuzTarihi) <= bitTarihiDeger).ToPagedList(size, no);
            }
             

            ViewBag.BasTarihi = basTarihi;
            ViewBag.BitTarihi = bitTarihi;

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> StokPeriyodikSayim([FromBody]TStokDuz[] jsonArrTotal)
        {
            if (ModelState.IsValid)
            {
                foreach (var x in jsonArrTotal)
                {
                    _context.StokDuz.Add(x);
                    await _context.SaveChangesAsync();

                }
                return null;
            }
            else
            {               
                return Json("Veri kaydedilemedi...");
            }
        }

    }
}
