using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaterProject.Models
{
    public class EFCharityRepository : ICharityRepository
    {
        private CharityDbContext _context;

        //constructor
        public EFCharityRepository (CharityDbContext context)
        {
            _context = context;
        }
        public IQueryable<Project> Projects => _context.Projects;
    }
}
