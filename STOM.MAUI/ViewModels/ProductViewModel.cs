using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using STO.Library.Services;
using STO.Models;

namespace STOM.MAUI.ViewModels
{
    public class ProductViewModel
    {
        public override string ToString()
        {
            if (Model == null)
            {
                return string.Empty;
            }
            return $"[{Model.Id}] {Model.Name} - {Model.Price.ToString("C2")} \n{Model.Description} \nIn Stock: {Model.Stock} \n";
        }
        public ICommand EditCommand {  get; private set; }

        public ICommand? DeleteCommand { get; private set; }

        public Product? Model { get; set; }
            
        public string DisplayPrice
        {
            get
            {
                if (Model == null) { return string.Empty; }
                return $"{Model.Price:C}";
            }
        }

        public string PriceAsString
        {
            set
            {
                if (Model == null)
                {
                    return;
                }
                if (decimal.TryParse(value, out var price))
                {
                    Model.Price = price;
                }
                else
                {

                }
            }
        }

        private void ExecuteEdit(ProductViewModel? p)
        {
            if (p?.Model == null)
            {
                return; 
            }
            Shell.Current.GoToAsync($"//Product?ProductId={p.Model.Id}");
        }

        private void ExecuteDelete(int? id)
        {
            if (id == null)
            {
                return;
            }

            ContactServerProxy.Current.Delete(id ?? 0);
        }

        public void Add()
        {
            if (Model != null)
            {
                ContactServerProxy.Current.AddOrUpdate(Model);
            }
            
        }
        public void SetupCommands()
        {
            EditCommand = new Command((p) => ExecuteEdit(p as ProductViewModel));
            DeleteCommand = new Command((p) => ExecuteDelete((p as ProductViewModel)?.Model?.Id));

        }
        public ProductViewModel() 
        {
            Model = new Product();
            SetupCommands();
        }
                
        public ProductViewModel(int id)
        {
            Model = ContactServerProxy.Current?.Products?.FirstOrDefault(p => p.Id == id);
            if(Model == null)
            {
                Model = new Product();
            }
        }
        public ProductViewModel(Product? p)
        {
            Model = p;
            SetupCommands();
        }
        public string? Display
        {
            get
            {
                return ToString();
            }
        }
    }
}
