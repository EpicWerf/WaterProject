using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaterProject.Models
{
    public class CharityDbContext : DbContext
    {
        public CharityDbContext (DbContextOptions<CharityDbContext> options) : base (options)
        {

        }

        public DbSet<Project> Projects { get; set; }
    }
}
