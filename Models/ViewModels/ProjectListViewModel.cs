using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// a viewmodel is a model built for a specific view
// this is populated by the linq statement in the controller at project runtime
// it is then brought into the index page through the "@model ProjectsListViewModel" statement
namespace WaterProject.Models.ViewModels
{
    public class ProjectListViewModel
    {
        // The IEnumberable is only pulled in the index page with the following statement
        // "@foreach (var x in Model.Projects)"
        public IEnumerable<Project> Projects { get; set; }

        //this one is used to set how many pages there are
        public PagingInfo PagingInfo { get; set; }
        public string Type { get; set; }

    }
}
