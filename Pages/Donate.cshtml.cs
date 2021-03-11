using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WaterProject.Infrastructure;
using WaterProject.Models;

namespace WaterProject.Pages
{
    //this model will bring in the repository
    public class DonateModel : PageModel
    {
        private ICharityRepository repository;

        //constructor
        public DonateModel (ICharityRepository repo, Cart cartService)
        {
            repository = repo;
            Cart = cartService;
        }

        //properties
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }

        //methods
        //on a get - set the returnURl equal to whatever was passed in
        public void OnGet(string returnUrl)
        {
            //Set the ReturnUrl = returnUrl or / if nothing was passed in
            ReturnUrl = returnUrl ?? "/";
        }

        //
        public IActionResult OnPost(long projectId, string returnUrl)
        {
            //look at first or default
            Project proj = repository.Projects
                .FirstOrDefault(p => p.ProjectId == projectId);
            //get the cart or add a new cart
            Cart.AddItem(proj, 1);
            //send to a new page using the returnUrl
            return RedirectToPage(new { returnUrl = returnUrl });
        }
        //handler to remove an item from the cart
        public IActionResult OnPostRemove(long projectId, string returnUrl)
        {
            Cart.RemoveLine(Cart.Lines.First(cl =>
                cl.Project.ProjectId == projectId).Project);
            return RedirectToPage(new { returnUrl = returnUrl });
        }
    }
}
