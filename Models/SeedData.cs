using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaterProject.Models
{
    public class SeedData
    {
        public static void EnsurePopulated (IApplicationBuilder application)
        {
            //grab an instance of our CharitDbContext using a scoped version of it
            CharityDbContext context = application.ApplicationServices.
                CreateScope().ServiceProvider.GetRequiredService<CharityDbContext>();

            //if there are any pending migrations, migrate!
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            //if there is nothing in the database yet...
            if(!context.Projects.Any())
            {
                //then add all this stuff:
                context.Projects.AddRange(
                    new Project
                    {
                        Type = "Well Rehab",
                        Program = "Water for Sierra Leone",
                        Impact = 400,
                        Phase = "Community Managed",
                        CompletionDate = new DateTime(2010, 08, 01),
                        Features = "WR, LL, CE, HST"
                    },

                    new Project
                    {
                        Type = "Well Rehab",
                        Program = "Water for Burkina Faso",
                        Impact = 500,
                        Phase = "Community Managed",
                        CompletionDate = new DateTime(2012, 08, 01),
                        Features = "WR, LL, CE, HST"
                    },

                    new Project
                    {
                        Type = "Borehole Well and Hand Pump",
                        Program = "Wells for South Sudan - NeverThirst",
                        Impact = 500,
                        Phase = "Community Managed",
                        CompletionDate = new DateTime(2013, 08, 01),
                        Features = "BW/HP, LL, CE, HST"
                    },

                    new Project
                    {
                        Type = "Urban Water Kiosk",
                        Program = "Urban Water Kiosk",
                        Impact = 500,
                        Phase = "Community Managed",
                        //CompletionDate = new DateTime(2013, 08, 01),
                        Features = "UWK, LL, CE, HST"
                    },

                    new Project
                    {
                        Type = "Borehole Well and Hand Pump",
                        Program = "Wells for Rawanda",
                        Impact = 500,
                        Phase = "Community Managed",
                        //CompletionDate = new DateTime(2013, 08, 01),
                        Features = "BW/HP, LL, CE, HST"
                    }


                );

                //go write this to the database
                context.SaveChanges();
            }
        }
    }
}
