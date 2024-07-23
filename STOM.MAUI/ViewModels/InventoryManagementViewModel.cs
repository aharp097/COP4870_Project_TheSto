using STO.Library.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using STO.Library.DTO;

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

        public async void RefreshInventory()
        {
            await ContactServerProxy.Current.Get();
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
        public async void DeleteProduct()
        {
            if (SelectedProduct?.Model == null)
            {
                return;
            }

            await ContactServerProxy.Current.Delete(SelectedProduct?.Model?.Id ?? 0);
            RefreshInventory();
        }
        private ProductDTO? ImportModel { get; set; }
        public async void Import()
        {
            var csv = (@"C:\Users\amark\Documents\College\COP4870\Inventory.csv");

            using (var reader = new StreamReader(csv)) 
            {
                while(reader.EndOfStream == false)
                {
                    var content = reader.ReadLine();
                    var cell = content.Split(',').ToList();
                    if(cell.Any(c => c.Length > 0))
                    {
                        ImportModel = new ProductDTO();
                        ImportModel.Name = cell[0];
                        ImportModel.Description = cell[1];
                        ImportModel.Price = decimal.Parse(cell[2]);
                        ImportModel.Stock = int.Parse(cell[3]);
                        ImportModel.Bogo = bool.Parse(cell[4]);
                        ImportModel.MarkedDown = bool.Parse(cell[5]);
                        ImportModel.MarkDownPercent = decimal.Parse(cell[6]);
                        await ContactServerProxy.Current.AddOrUpdate(ImportModel);
                    }

                }
            }
        }
    }
}
