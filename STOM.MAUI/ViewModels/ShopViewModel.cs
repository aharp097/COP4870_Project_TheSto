using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
                return ContactServerProxy.Current.Products.Where(p => p != null || p.Stock > 0)
                    .Where(p => p?.Name?.ToUpper()?.Contains(InventoryQuery.ToUpper()) ?? false)
                    .Select(p => new ProductViewModel(p)).ToList()
                    ?? new List<ProductViewModel>();
            }
        }

        public List<ProductViewModel> PIC
        {
            get
            {
                return ShoppingCartService.Current?.Cart?.Contents?.Where(p => p != null)
                    .Where(p => p?.Name?.ToUpper()?.Contains(InventoryQuery.ToUpper()) ?? false).Select(p => new ProductViewModel(p)).ToList()
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
        public ShoppingCart Cart {  
            get
            {
                return ShoppingCartService.Current.Cart;

            }
        }

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
            NotifyPropertyChanged(nameof(Products));
        }
    }

}
