using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using STO.Library.DTO;
using STO.Models;

namespace STO.Library.Models
{
    public class ShoppingCart
    {
        public string? Name { get; set; }
        public int Id { get; set; }
        
        public List<ProductDTO>? Contents { get; set; }

        public ShoppingCart() 
        { 
            Contents = new List<ProductDTO>();
        }

        public ShoppingCart(ShoppingCart c)
        {
            if (c.Contents == null)
            {
                Contents = new List<ProductDTO>();
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
