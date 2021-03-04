using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WaterProject.Infrastructure;
using WaterProject.Models;
//######################################################################################

namespace WaterProject.Pages
{
    //this model will bring in the repository
    public class DonateModel : PageModel
    {
        private ICharityRepository repository;

        //constructor
        public DonateModel (ICharityRepository repo)
        {
            repository = repo;
        }

        //properties
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }

        //methods
        //on a get - set the returnURl equal to whatever was passed in
        public void OnGet(string returnUrl)
        {
            //if nothing was passed in, set it equal to "/"
            ReturnUrl = returnUrl ?? "/";
            //if nothing was in the cart, get a new cart
            Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
        }

        //
        public IActionResult OnPost(long projectId, string returnUrl)
        {
            //look at first or default
            Project project = repository.Projects.FirstOrDefault(p => p.ProjectId == projectId);

            //get the cart or add a new cart
            Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();

            //add an item to the cart of qty 1
            Cart.AddItem(project, 1);

            //convert the cart into Json
            HttpContext.Session.SetJson("cart", Cart);

            //send to a new page using the returnUrl
            return RedirectToPage(new { returnUrl = returnUrl });
        }
    }
}
