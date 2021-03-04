using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaterProject.Models
{
    // Inherits from Dbcontext class provided through the system
    //hover over DbContext for more info
    //we created a specific version of a DbContext that will inherit "base" option from DbContext
    public class CharityDbContext : DbContext
    {

        //constructor called when instance of the object is built the FIRST time
        //auto called method when we build an object the first time
        //this will just go out and get all the base options for a DbContext
        public CharityDbContext (DbContextOptions<CharityDbContext> options) : base (options)
        {

        }

        //property that will return a DbSet of type Project 
        //referes to project class/model
        public DbSet<Project> Projects { get; set; }
    }
}
