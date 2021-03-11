using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace WaterProject.Models
{
    public class Cart
    {
        //the Lines object is a list of CartLines
        public List<CartLine> Lines { get; set; } = new List<CartLine>();

        public virtual void AddItem(Project proj, int qty)
        {
            //the next block will check to see if you have the selected item in your cart
            CartLine line = Lines
                .Where(p => p.Project.ProjectId == proj.ProjectId)
                .FirstOrDefault();

            //if you don't, it'll add it to the cart
            if (line == null)
            {
                Lines.Add(new CartLine
                {
                    Project = proj,
                    Quantity = qty
                });
            }
            //if you do, it'll update the quantity rather than adding the item twice
            else
            {
                line.Quantity += qty;
            }
        }

        //remove ONE item from the cart
        public virtual void RemoveLine(Project proj) =>
            Lines.RemoveAll(x => x.Project.ProjectId == proj.ProjectId);

        //remove ALL items from cart
        public virtual void Clear() => Lines.Clear();

        //Get total price for the items in the cart
        //price is hard coded here; for the assignment you will do e.Price or something like that
        public decimal ComputeTotalSum() => Lines.Sum(e => 25 * e.Quantity);

        public class CartLine
        {
            public int CartLineID { get; set; }
            public Project Project { get; set; }
            public int Quantity { get; set; }
        }
    }
}
