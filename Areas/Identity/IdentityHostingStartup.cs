using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockControl.Areas.Identity.Data;
using StockControl.Models;
using Microsoft.AspNetCore.Builder;


[assembly: HostingStartup(typeof(StockControl.Areas.Identity.IdentityHostingStartup))]
namespace StockControl.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<StockControlContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("DefaultConnection")));


                services.AddDefaultIdentity<StockControlUser>()
                    .AddEntityFrameworkStores<StockControlContext>().AddDefaultTokenProviders().AddDefaultUI();

                services.Configure<IdentityOptions>(options =>
                {
                    // Password settings
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 0;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequiredUniqueChars = 0;
                    
                   
                });
                services.Configure<IISOptions>(options =>
                {
                    options.ForwardClientCertificate= false;

                });


            });
        }
    }
}