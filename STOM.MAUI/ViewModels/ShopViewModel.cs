using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using STO.Library.Models;
using STO.Library.Services;
using STO.Models;

namespace STOM.MAUI.ViewModels
{
    public class ShopViewModel :INotifyPropertyChanged
    {
        public ShopViewModel()
        {
            InventoryQuery = string.Empty;
            Cart = new ShoppingCart();
            SetupCommands();
        }

        private string inventoryQuery;
        public string InventoryQuery 
        {
            set 
            {
                inventoryQuery = value;
                NotifyPropertyChanged();
            }
            get 
            { 
                return inventoryQuery; 
            }

        }
        public List<ProductViewModel> Products
        {
            get
            {
                //return ContactServerProxy.Current.Products.Where(p => p != null && p.Stock > 0)
                //    .Where(p => p?.Name?.ToUpper()?.Contains(InventoryQuery.ToUpper()) ?? false)
                //    .Select(p => new ProductViewModel(p)).ToList()
                //    ?? new List<ProductViewModel>();
                return ContactServerProxy.Current.Products.Where(p => p != null && p.Stock > 0).Select(p => new ProductViewModel(p)).ToList()
                    ?? new List<ProductViewModel>();
            }
        }
        public ShopViewModel(int id)
        {
            ShoppingCartService.Current.currentID = id;
            Cart = ShoppingCartService.Current?.Carts?.FirstOrDefault(c => c.Id == id);
            if (Cart == null)
            {
                Cart = new ShoppingCart();
            }
        }

        public ShopViewModel(ShoppingCart? c)
        {
            Cart = c;
            SetupCommands();
        }
        public ICommand EditCommand { get; private set; }

        public ICommand? DeleteCommand { get; private set; }
        public ShoppingCart? Cart { get; set; }
        public string TotalInCart
        {
            get
            {
                decimal total = 0;
                decimal track = 0;
                if (PIC == null)
                {
                    return "$0.00";
                }
                foreach (var item in PIC)
                {
                    if (item?.Model?.Bogo == true)
                    {
                        for (int i = 0; i < item.Stock; i++)
                        {
                            if (i%2  == 0)
                            {
                                track = item.Price + track;
                            }
                        }
                        
                    } else if (item?.Model?.MarkedDown == true)
                    {
                        track = item.Model.MarkDownPercent / 100;
                        track = item.Price * track;
                        track = item.Price - track;
                        track = track * item.Stock;
                    }
                    else
                    {
                        track = item.Price * item.Stock;
                    }
                    
                    total += track;
                }
                return total.ToString("C");
            }
            
        }

        public List<ProductViewModel> PIC
        {
            get
            {
                //return ShoppingCartService.Current?.Cart?.Contents?.Where(p => p != null)
                //    .Where(p => p?.Name?.ToUpper()?.Contains(InventoryQuery.ToUpper()) ?? false).Select(p => new ProductViewModel(p)).ToList()
                //    ?? new List<ProductViewModel>();
                return ShoppingCartService.Current?.Cart?.Contents?.Where(p => p != null)
                    .Select(p => new ProductViewModel(p)).ToList()
                    ?? new List<ProductViewModel>();

            }
        }

        private ProductViewModel? selectedProduct;
        public ProductViewModel? SelectedProduct
        {
            
            get => selectedProduct;
            set
            {
                selectedProduct = value;
                if (selectedProduct != null && selectedProduct.Model == null)
                {
                    selectedProduct.Model = new Product();
                } else if (selectedProduct != null && selectedProduct.Model != null) 
                {
                    selectedProduct.Model = new Product(selectedProduct.Model);
                }

                NotifyPropertyChanged();
                
            }
        }
        //public ShoppingCart Cart {  
        //    get
        //    {
        //        return ShoppingCartService.Current.Cart;

        //    }
        //}


        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void RefreshInventory()
        {
            InventoryQuery = string.Empty;
            NotifyPropertyChanged(nameof(Products));
        }

        public void Search()
        {
            NotifyPropertyChanged(nameof(Products));
        }
        public void AddToCart()
        {
            if (SelectedProduct?.Model == null)
            {
                return;
            }
            //SelectedProduct.Model = new Product(SelectedProduct.Model);
            SelectedProduct.Model.Stock = 1;
            ShoppingCartService.Current.AddToCart(SelectedProduct.Model);

            SelectedProduct = null;
            NotifyPropertyChanged(nameof(PIC));
            NotifyPropertyChanged(nameof(TotalInCart));
            NotifyPropertyChanged(nameof(Products));
        }
        public void SetupCommands()
        {
            EditCommand = new Command((p) => ExecuteEdit(p as ShopViewModel));
            DeleteCommand = new Command((p) => ExecuteDelete((p as ShopViewModel)?.Cart?.Id));

        }
        private void ExecuteEdit(ShopViewModel ? c)
        {
            if (c?.Cart == null)
            {
                return;
            }
            Shell.Current.GoToAsync($"//Shop?cartId={c.Cart.Id}");
            //ShoppingCartService.Current.AddOrUpdate(SelectedCart.Cart);
        }

        private void ExecuteDelete(int? id)
        {
            if (id == null)
            {
                return;
            }
            ShoppingCartService.Current.Delete(id ?? 0);
        }


    }

}
