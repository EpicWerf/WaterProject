using Microsoft.AspNetCore.Mvc;
using WaterProject.Models;

namespace WaterProject.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private Cart cart;
        public CartSummaryViewComponent(Cart cartService)
        {
            cart = cartService;
        }

        //passes on the Cart to the View method to generate the fragment of HTML that will be included in the layout
        public IViewComponentResult Invoke()
        {
            return View(cart);
        }
    }
}
