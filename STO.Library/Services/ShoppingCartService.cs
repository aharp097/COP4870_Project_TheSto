using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STO.Library.DTO;
using STO.Library.Models;
using STO.Models;

namespace STO.Library.Services
{
    public class ShoppingCartService
    {
        private static ShoppingCartService? instance;
        private static object instanceLock = new object();
        private List<ShoppingCart> carts;
        public ReadOnlyCollection<ShoppingCart> Carts
        {
            get
            {
                return carts.AsReadOnly();
            }
        }

        public int currentID { get; set; }
        public ShoppingCart Cart
        {
            get
            {
                if (!carts.Any())
                {
                    var newCart = new ShoppingCart();
                    carts.Add(newCart);
                    return newCart;
                }
                return carts?.FirstOrDefault(c => c.Id == currentID) ?? new ShoppingCart();
            }
        }
        private ShoppingCartService()
        {
            carts = new List<ShoppingCart>
            {
                new ShoppingCart{Id = 1,Name = "Shopping Cart"}
                , new ShoppingCart{Id = 2,Name = "Wishlist"}
            };

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
        public int LastID
        {
            get
            {
                if (carts?.Any() ?? false)
                {
                    return carts?.Select(c => c.Id)?.Max() ?? 0;
                }
                return 0;
            }
        }
        public ShoppingCart AddOrUpdate(ShoppingCart c)
        {
            if (carts == null)
            {
                return null;
            }
            var isAdd = false;
            if (c.Id == 0)
            {
                c.Id = LastID + 1;
                isAdd = true;
            }
            if (isAdd)
            {
                carts.Add(c);
            }


            return c;
        }

        public void Delete(int id)
        {
            if (carts == null)
            {
                return;
            }
            var cartToDelete = carts.FirstOrDefault(p => p.Id == id);
            if (cartToDelete != null)
            {
                carts.Remove(cartToDelete);
            }
        }

        public ShoppingCart? Get(int id)
        {
            if (carts == null)
            {
                return null;
            }
            currentID = id;
            var cartToReturn = carts.FirstOrDefault(p => p.Id == id);
            return cartToReturn;
        }

        public void AddToCart(ProductDTO newp)
        {
            if (Cart == null || Cart.Contents == null)
            {
                return;
            }

            var oldp = Cart?.Contents?.FirstOrDefault(oldps => oldps.Id == newp.Id);

            var inventoryp = ContactServerProxy.Current.Products.FirstOrDefault(invProd => invProd.Id == newp.Id);
            if (inventoryp == null)
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
                Cart?.Contents.Add(newp);
            }
        }
        public void RestoreProduct(ProductDTO newp)
        {
            if (Cart == null || Cart.Contents == null)
            {
                return;
            }

            var oldp = Cart?.Contents?.FirstOrDefault(oldps => oldps.Id == newp.Id);

            var inventoryp = ContactServerProxy.Current.Products.FirstOrDefault(invProd => invProd.Id == newp.Id);
            if (inventoryp == null)
            {
                return;
            }
            inventoryp.Stock += newp.Stock;
            if (oldp != null)
            {
                oldp.Stock -= newp.Stock;
            }
            //else
            //{
            //    Cart?.Contents.Add(newp);
            //}
        }



    }
    
}
