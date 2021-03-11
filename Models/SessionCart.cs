using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using WaterProject.Infrastructure;

//The SessionCart class subclasses the Cart class and overrides the AddItem, RemoveLine, and 
//Clear methods so they call the base implementations and then store the updated state in the 
//session using the extension methods on the ISession interface
namespace WaterProject.Models
{
    public class SessionCart : Cart
    {
        //factory for creating SessionCart objects and providing them with an ISession object so they can store themselves.
        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;
            SessionCart cart = session?.GetJson<SessionCart>("Cart")
                ?? new SessionCart();
            cart.Session = session;
            return cart;
        }
        [JsonIgnore]
        public ISession Session { get; set; }
        public override void AddItem(Project proj, int qty)
        {
            base.AddItem(proj, qty);
            Session.SetJson("Cart", this);
        }
        public override void RemoveLine(Project proj)
        {
            base.RemoveLine(proj);
            Session.SetJson("Cart", this);
        }
        public override void Clear()
        {
            base.Clear();
            Session.Remove("Cart");
        }
    }
}
