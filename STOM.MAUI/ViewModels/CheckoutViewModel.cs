using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using STO.Library.Models;
using STO.Library.Services;

namespace STOM.MAUI.ViewModels
{
    public class CheckoutViewModel : INotifyPropertyChanged
    {
        public CheckoutViewModel()
        {
            
                Cart = new ShoppingCart();
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public CheckoutViewModel(int id)
        {
            ShoppingCartService.Current.currentID = id;
            Cart = ShoppingCartService.Current?.Carts?.FirstOrDefault(c => c.Id == id);
            if (Cart == null)
            {
                Cart = new ShoppingCart();
            }
        }
        public ShoppingCart? Cart { get; set; }
        public decimal tic = 0;

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
                            if (i % 2 == 0)
                            {
                                track = item.Price + track;
                            }
                        }

                    }
                    else if (item?.Model?.MarkedDown == true)
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
                tic = total;
                return total.ToString("C");
            }

        }
        public string TotalWithTax
        {
            get
            {
                decimal tax = ContactServerProxy.Current.tax;
                decimal total = tax * tic;
                total += tic;
               return (total.ToString("C"));
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

    }
}
