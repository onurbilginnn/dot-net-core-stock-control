using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StockControl.Models;
using StockControl.Context;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace StockControl.Controllers
{
    public class HomeController : Controller
    {
        private readonly BAR_ERPContext _context;

        public HomeController(BAR_ERPContext context)
        {
            _context = context;
        }
       
        public IActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Index([FromBody]string[] arr)
        {
            string subeNo = "";
            HttpContext.Session.SetString("Sube", arr[0]);
            HttpContext.Session.SetString("User", arr[1]);
            foreach (char x in arr[0])
            {
                if (Char.IsDigit(x))
                {
                    subeNo += x;
                }
                else
                {
                    break;
                }
            }
            HttpContext.Session.SetString("SubeNo",subeNo);
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
           
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpGet]
        public string Subeler()
        {
            List<TSubeBilgileri> subeList = _context.TSubeBilgileri.Select(m => m).ToList();
           
            return JsonConvert.SerializeObject(subeList);
        }

        
        public IActionResult GetSubeNo([FromBody]string sube)
        {
            HttpContext.Session.SetString("sube",sube);
            return RedirectToAction("Index","Home");
        }

        public IActionResult Redirect()        {
            
            return RedirectToAction("Account/Login","Identity");
        }

    }
}
