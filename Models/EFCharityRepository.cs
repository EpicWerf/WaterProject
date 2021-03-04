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
        //when you call and create EFCharityRepository instance, you need to pass a context file with it        
        //store it in our private _context property
        public EFCharityRepository (CharityDbContext context)
        {
            _context = context;
        }

        //when somebody calls projects within our EFCharityRepository class, give them the projects that are stored in _projects
        //lambda will set projects (on the left) automatically with the _context.Projects
        public IQueryable<Project> Projects => _context.Projects;
    }
}
