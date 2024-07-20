using System.Security.Cryptography.X509Certificates;
using Sto.API.Database;
using STO.Models;
using STO.Library.DTO;

namespace Sto.API.EC
{
    public class InventoryEC
    { 
         public InventoryEC()
        {
        }
        public async Task<IEnumerable<ProductDTO>> Get()
        {
            return Filebase.Current.Products.Take(70).Select(p => new ProductDTO(p)); 
        }
        public async Task<ProductDTO?> Delete(int id)
        {
            //if (Filebase.Current.Products == null)
            //{
            //    return null;
            //}
            return new ProductDTO(Filebase.Current.Delete(id));
            //if (productToDelete != null)
            //{
            //    Filebase.Current.Products.Remove(productToDelete);
            //}
            //return new ProductDTO(productToDelete);
        }
        public async Task<ProductDTO> AddOrUpdate(ProductDTO p)
        {
            //var isAdd = false;
            //if (p.Id == 0)
            //{
            //    p.Id = Filebase.Current.LastID + 1;
            //    isAdd = true;
            //}
            //if (isAdd)
            //{
            //    Filebase.Current.Products.Add(new Product(p));
            //} else
            //{
            //   var prodToUpdate = Filebase.Current.Products.FirstOrDefault(a => a.Id == p.Id);
            //    if (prodToUpdate != null)
            //    {
            //        var index = FauxDatabase.Products.IndexOf(prodToUpdate);
            //        FauxDatabase.Products.RemoveAt(index);
            //        prodToUpdate = new Product(p);
            //        FauxDatabase.Products.Insert(index, prodToUpdate);
            //    }
            //}


            return new ProductDTO(Filebase.Current.AddOrUpdate(new Product(p)));
            
        }
    
    }
}
