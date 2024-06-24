using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using STO.Library.Services;

namespace STOM.MAUI.ViewModels
{
    public class ShopViewModel :INotifyPropertyChanged
    {
        public string InventoryQuery { get; set; } = string.Empty;
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

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void RefreshInventory()
        {
            NotifyPropertyChanged(nameof(Products));
        }
    }
   
}
