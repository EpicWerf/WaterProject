using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WaterProject.Models;
using WaterProject.Models.ViewModels;


namespace WaterProject.Controllers
{
    //"HomeController" can be anything (like "BlahController), as long as you change appropriate references (like in the endpoints in the startup file, and have a folder named blah in the views)
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ICharityRepository _repository;
        public int PageSize = 4;

        public HomeController(ILogger<HomeController> logger, ICharityRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        //add in information for pagination
        public IActionResult Index(string category, int page = 1)
        {
            return View(new ProjectListViewModel
            {
                Projects = _repository.Projects
                        .Where(p => category == null || p.Type == category)
                        .OrderBy(p => p.ProjectId)
                        .Skip((page - 1) * PageSize)
                        .Take(PageSize)
                    ,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    //the next line will create page numbers automatically using an if statement that uses a where and count to create the correct number of pages
                    TotalNumItems = category == null ? _repository.Projects.Count() : _repository.Projects.Where(x => x.Type == category).Count()
                },
                Type = category
            }) ;                       
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
    }
}
