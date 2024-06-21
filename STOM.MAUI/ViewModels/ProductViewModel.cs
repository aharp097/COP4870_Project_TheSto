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
            if (Product == null)
            {
                return string.Empty;
            }
            return $"[{Product.Id}] {Product.Name} - {Product.Price.ToString("C2")} \n{Product.Description} \nIn Stock: {Product.Stock} \n";
        }
        public ICommand EditCommand {  get; private set; }

        public ICommand? DeleteCommand { get; private set; }

        public Product? Product;

        public string DisplayPrice
        {
            get
            {
                if (Product == null) { return string.Empty; }
                return $"{Product.Price:C}";
            }
        }

        public string PriceAsString
        {
            set
            {
                if (Product == null)
                {
                    return;
                }
                if (decimal.TryParse(value, out var price))
                {
                    Product.Price = price;
                }
                else
                {

                }
            }
        }

        private void ExecuteEdit(ProductViewModel? p)
        {
            if (p?.Product == null)
            {
                return; 
            }
            Shell.Current.GoToAsync($"//Product?ProductId={p.Product.Id}");
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
            if (Product != null)
            {
                ContactServerProxy.Current.AddOrUpdate(Product);
            }
            
        }
        public void SetupCommands()
        {
            EditCommand = new Command((p) => ExecuteEdit(p as ProductViewModel));
            DeleteCommand = new Command((p) => ExecuteDelete((p as ProductViewModel)?.Product?.Id));

        }
        public ProductViewModel() 
        {
            Product = new Product();
            SetupCommands();
        }

        public ProductViewModel(int id)
        {
            Product = ContactServerProxy.Current?.Products?.FirstOrDefault(p => p.Id == id);
            if(Product == null)
            {
                Product = new Product();
            }
        }
        public ProductViewModel(Product p)
        {
            Product = p;
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
