using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using STO.Models;

namespace STO.Library.Models
{
    public class ShoppingCart
    {
        public string? Name { get; set; }
        public int Id { get; set; }
        
        public List<Product>? Contents { get; set; }

        public ShoppingCart() 
        { 
            Contents = new List<Product>();
        }

        public ShoppingCart(ShoppingCart c)
        {
            if (c.Contents == null)
            {
                Contents = new List<Product>();
            }
            else
            {
                Contents = c.Contents;
            }
            
            Id = c.Id;
            Name = c.Name;
        }


    }
}
