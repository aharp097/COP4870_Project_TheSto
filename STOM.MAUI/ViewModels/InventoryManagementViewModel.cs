using STO.Library.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace STOM.MAUI.ViewModels
{ 
    public class InventoryManagementViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public List<ProductViewModel> Products
        {
            get
            {
                return ContactServerProxy.Current?.Products?.Where(p=>p != null)
                    .Select(p => new ProductViewModel(p)).ToList() 
                    ?? new List<ProductViewModel>();
            }
        }
        public ProductViewModel SelectedProduct { get; set; }

        public void RefreshInventory()
        {
            NotifyPropertyChanged(nameof(Products));
        }
        public void UpdateProduct()
        {
            if (SelectedProduct?.Model == null)
            {
                return;
            }
            Shell.Current.GoToAsync($"//Product?ProductId={SelectedProduct.Model.Id}");
            ContactServerProxy.Current.AddOrUpdate(SelectedProduct.Model);
        }
        public void DeleteProduct()
        {
            if (SelectedProduct?.Model == null)
            {
                return;
            }

            ContactServerProxy.Current.Delete(SelectedProduct.Model.Id);
            RefreshInventory();
        }
    }
}
