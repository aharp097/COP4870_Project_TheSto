using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STO.Library.Models;
using STO.Models;

namespace STO.Library.Services
{
    public class ShoppingCartService
    {
        private static ShoppingCartService? instance;
        private static object instanceLock = new object();

        public ReadOnlyCollection<ShoppingCart> carts;

        public ShoppingCart Cart
        {
            get
            {
                if (carts == null || !carts.Any())
                {
                    return new ShoppingCart();
                }
                return carts?.FirstOrDefault() ?? new ShoppingCart();
            }
        }
        private ShoppingCartService()
        {

        }
        public static ShoppingCartService Current
        {
            get
            {
                lock(instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ShoppingCartService();
                    }
                }
                
                return instance;
            }
        }
        /*public ShoppingCart AddOrUpdate(ShoppingCart c)
        {

        }*/
        public void AddToCart(Product newp)
        {
            if(Cart == null || Cart.Contents == null)
            {
                return;
            }

            var oldp = Cart?.Contents?.FirstOrDefault(oldps => oldps.Id == newp.Id);
            
            var inventoryp = ContactServerProxy.Current.Products.FirstOrDefault(invProd => invProd.Id == newp.Id);
            if (inventoryp != null)
            {
                return;
            }
            inventoryp.Stock -= newp.Stock;
            if (oldp != null)
            {
                oldp.Stock += newp.Stock;
            }
            else
            {
                Cart.Contents.Add(newp);
            }
        }
            
    }
    
}
