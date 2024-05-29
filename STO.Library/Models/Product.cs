using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STO.Models
{
    public class Product
    {   //19:34 vid 2
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Id { get; set; }
        public int Stock { get; set; }
        public Product()
        {
            Name = "??";
            Description = "??";
            Price = 0;
            Id = 0;
            Stock = 0;
        }

        public string? Display
        {
            get
            {
                return ToString();
            }
        }

        public override string ToString()
        {
            return $"[{Id}] {Name} - {Price.ToString("C2")} \n{Description} \nIn Stock: {Stock} \n";
        }
    }
}
