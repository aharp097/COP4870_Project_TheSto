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
                return ContactServerProxy.Current.Products.Where(p => p != null)
                    .Where(p => p?.Name?.ToUpper()?.Contains(InventoryQuery.ToUpper()) ?? false)
                    .Select(p => new ProductViewModel(p)).ToList()
                    ?? new List<ProductViewModel>();
            }
        }

        // private Product selectedProduct;
        public Product SelectedProduct = new Product();
        /*{
            set
            {
                selectedProduct = value;
            }
            get
            {
                return selectedProduct;
            }
        }*/
        public ShoppingCart Cart {  get; set; }

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
    }
   
}
