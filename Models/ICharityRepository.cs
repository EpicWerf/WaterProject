using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaterProject.Models
{
    //interface creates a template that is meant to be inherited
    //"if you are going to use this information, this is what it will have"
    //defines structures to be passed down
    public interface ICharityRepository
    {
        //put objects in a class that will be easier to query out of
        //refers specifically to the project class/model that we made
        IQueryable<Project> Projects { get;  }
    }
}
