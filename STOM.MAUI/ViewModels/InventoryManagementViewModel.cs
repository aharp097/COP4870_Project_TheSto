using STO.Library.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace STOM.MAUI.ViewModels
{ //18 vid 10
    public class InventoryManagementViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyProductChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public List<ProductViewModel> Products
        {
            get
            {
                return ContactServerProxy.Current?.Products?.Select(p => new ProductViewModel(p)).ToList() ?? new List<ProductViewModel>();
            }
        }
        public ProductViewModel SelectedProduct { get; set; }
        public InventoryManagementViewModel()
        {
           
        }

        public void RefreshInventory()
        {
            NotifyProductChanged("Products");
        }
        public void UpdateProduct()
        {
            if (SelectedProduct?.Product == null)
            {
                return;
            }
            Shell.Current.GoToAsync($"//Product?ProductId={SelectedProduct.Product.Id}");
            ContactServerProxy.Current.AddOrUpdate(SelectedProduct.Product);
        }
        public void DeleteProduct()
        {
            if (SelectedProduct?.Product == null)
            {
                return;
            }

            ContactServerProxy.Current.Delete(SelectedProduct.Product.Id);
            RefreshInventory();
        }
    }
}
