using Newtonsoft.Json;
using STO.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sto.API.Database
{
    public class Filebase
    {
        private string _root;
        private static Filebase _instance;


        public static Filebase Current
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new Filebase();
                }

                return _instance;
            }
        }
        public int LastID
        {
            get
            {
                if (Products?.Any() ?? false)
                {
                    return Products?.Select(c => c.Id)?.Max() ?? 0;
                }
                return 0;
            }
        }

        private Filebase()
        {
            _root = @"C:\temp\Products";
        }

        public Product AddOrUpdate(Product product)
        {
            //set up a new Id if one doesn't already exist
            if(product.Id <= 0)
            {
                product.Id = LastID;
            }

            //go to the right place]
            string path = $"{_root}\\{product.Id}.json";

            //if the item has been previously persisted
            if(File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }

            //write the file
            File.WriteAllText(path, JsonConvert.SerializeObject(product));

            //return the item, which now has an id
            return product;
        }

        public List<Product> Products
        {
            get
            {
                var root = new DirectoryInfo(_root);
                var _prods = new List<Product>();
                foreach (var appFile in root.GetFiles())
                {
                    var prod = JsonConvert.DeserializeObject<Product>(File.ReadAllText(appFile.FullName));
                    if(prod != null)
                    {
                        _prods.Add(prod);
                    }
                }
                return _prods;
            }
        }

        public Product Delete(int id)
        {
            //TODO: refer to AddOrUpdate for an idea of how you can implement this.
            throw new NotImplementedException();
        }
    }


}
