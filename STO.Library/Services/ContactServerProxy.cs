using STO.Models;
using System.Collections.ObjectModel;

namespace STO.Library.Services
{
    public class ContactServerProxy
    {
        private ContactServerProxy() 
        {
            products = new List<Product>();
        }
        private static ContactServerProxy? instance;
        private static object instanceLock = new object();
        public static ContactServerProxy Current
        {
            get
            {
                lock(instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ContactServerProxy();
                    }
                }
                
                return instance;
            }
        }
        private List<Product>? products;
        public ReadOnlyCollection<Product>? Products
        { 
            get 
            { 
                return products?.AsReadOnly(); 
            } 
        }
                                                 //functionality
        public int LastID
        {
            get
            {
                if (products?.Any() ?? false)
                {
                    return products?.Select(c => c.Id)?.Max() ?? 0;
                }
                return 0;
            }
        }
        public Product? AddOrUpdate(Product product)
        {
            if(products == null)
            {
                return null;
            }
            var isAdd = false;
            if (product.Id == 0)
            {
                product.Id = LastID + 1;
                isAdd = true;
            }
            if (isAdd)
            {
                products.Add(product);
            }
            

            return product;
        }

        public void Delete(int id)
        {
            if (products == null)
            {
                return;
            }
            var productToDelete = products.FirstOrDefault(p => p.Id == id);
            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }
        }
        public Product? Get(int id)
        {
            if (products == null)
            {
                return null; 
            }
            var productToReturn = products.FirstOrDefault(p => p.Id == id);
            return productToReturn;
        }

    }
}
