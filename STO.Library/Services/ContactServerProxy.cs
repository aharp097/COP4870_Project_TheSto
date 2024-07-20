using STO.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using STO.Library.Utilities;
using STO.Library.DTO;

namespace STO.Library.Services
{//vid19
    public class ContactServerProxy
    {
        private ContactServerProxy() 
        {
            
            var response = new WebRequestHandler().Get("/Inventory").Result;
            products = JsonConvert.DeserializeObject<List<ProductDTO>>(response);
        }
        private static ContactServerProxy? instance;
        private static object instanceLock = new object();
        public decimal tax = 0;
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
        private List<ProductDTO>? products;
        public ReadOnlyCollection<ProductDTO>? Products
        { 
            get 
            { 
                return products?.AsReadOnly(); 
            } 
        }

        public async Task<IEnumerable<ProductDTO>> Get()
        {
            var result = await new WebRequestHandler().Get("/Inventory");
            var deserializedResult = JsonConvert.DeserializeObject<List<ProductDTO>>(result);
            products = deserializedResult?.ToList() ?? new List<ProductDTO>();
            return products;
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
        public async Task<ProductDTO?> AddOrUpdate(ProductDTO? product)
        {

            var result = await new WebRequestHandler().Post("/Inventory", product);
            return JsonConvert.DeserializeObject<ProductDTO>(result);
        }

        public async Task<ProductDTO?> Delete(int id)
        {
            //if (products == null)
            //{
            //    return;
            //}
            //var productToDelete = products.FirstOrDefault(p => p.Id == id);
            //if (productToDelete != null)
            //{
            //    products.Remove(productToDelete);
            //}
            var response = await new WebRequestHandler().Delete($"/{id}");
            var productToDelete = JsonConvert.DeserializeObject<ProductDTO>(response);
            return productToDelete;
        }
        public ProductDTO? Get(int id)
        {
            if (products == null)
            {
                return null; 
            }
            var productToReturn = products.FirstOrDefault(p => p.Id == id);
            return productToReturn;
        }

        public void SetTax(decimal t)
        {
            tax = t;


        }
    }
}
