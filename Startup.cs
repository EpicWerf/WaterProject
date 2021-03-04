using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaterProject.Models;

namespace WaterProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //this line tells us that we are going to use a controller and views in this project
            services.AddControllersWithViews();

            //this line actually calls that DbContext class that we made
            //Otherwise that class would just sit there, not being called
            //use the connection string to do that
            services.AddDbContext<CharityDbContext>(options =>
            {
                options.UseSqlite(Configuration["ConnectionStrings:WaterCharityConnection"]);
            });

            //each session will get it's own tailored repository
            services.AddScoped<ICharityRepository, EFCharityRepository>();

            //######################################################################################
            //allows you to use razor pages
            services.AddRazorPages();

            //######################################################################################
            //setting up session storage
            services.AddDistributedMemoryCache();
            services.AddSession();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //######################################################################################
            //allows you to use session storage
            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            //endpoints are ways to set up custom and consistent URLs that the user can use
            app.UseEndpoints(endpoints =>
            {
                //if they type a category and page into the URL
                endpoints.MapControllerRoute(
                    "catpage",
                    "{category}/{page:int}",
                    new { Controller = "Home", Action = "Index" });

                //if they only type the page number
                endpoints.MapControllerRoute(
                    "page",
                    "{page:int}",
                    new { Controller = "Home", Action = "Index" });

                //if they type a category into the URL. Set the page to 1 since the user didn't provide it
                endpoints.MapControllerRoute(
                    "category",
                    "{category}",
                    new { Controller = "Home", Action = "Index", page = 1});

                //if they type projects/page into the URL
                endpoints.MapControllerRoute(
                    "pagination",
                    "Projects/{page}",
                    new { Controller = "Home", Action = "Index" });

                //if what comes in doesn't match anything, use the default route setup (Home -> Index)
                endpoints.MapDefaultControllerRoute();

                //######################################################################################
                //allows endpoints to use razor pages (add routing for razor pages)
                endpoints.MapRazorPages();

            });

            //goes to the SeedData class, calls the EnsurePopulated method
            //decide if there is anything in the database and make decisions based on that
            SeedData.EnsurePopulated(app);
        }
    }
}
