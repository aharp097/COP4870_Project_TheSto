using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STO.Models;

namespace STO.Library.DTO
{
    public class ProductDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Id { get; set; }
        public int? Stock { get; set; }
        public bool Bogo { get; set; }

        public bool MarkedDown { get; set; }
        public decimal MarkDownPercent { get; set; }

        public ProductDTO()
        {

        }
        public ProductDTO(Product p)
        {
            Name = p.Name;
            Description = p.Description;
            Price = p.Price;
            Id = p.Id;
            Stock = p.Stock;
            Bogo = p.Bogo;
            MarkedDown = p.MarkedDown;
            MarkDownPercent = p.MarkDownPercent;
        }

        public ProductDTO(ProductDTO p)
        {
            Name = p.Name;
            Description = p.Description;
            Price = p.Price;
            Id = p.Id;
            Stock = p.Stock;
            Bogo = p.Bogo;
            MarkedDown = p.MarkedDown;
            MarkDownPercent = p.MarkDownPercent;
        }
    }
}
