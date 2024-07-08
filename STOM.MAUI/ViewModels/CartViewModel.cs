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
    public class CartViewModel : INotifyPropertyChanged
    {
        public CartViewModel() { 
            NewCart = new ShoppingCart();
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public List<ShopViewModel> Carts
        {
            get
            {
                return ShoppingCartService.Current?.Carts?.Where(c => c != null)
                    .Select(c => new ShopViewModel(c)).ToList()
                    ?? new List<ShopViewModel>();
            }
        }

        public ICommand EditCommand { get; private set; }

        public ICommand? DeleteCommand { get; private set; }
        public ShopViewModel SelectedCart { get; set; }
        //private void ExecuteEdit()
        //{
        //    if (SelectedCart == null)
        //    {
        //        return;
        //    }
        //    Shell.Current.GoToAsync($"//Shop?CartId={SelectedCart?.Cart?.Id}");
        //    ShoppingCartService.Current.AddOrUpdate(SelectedCart.Cart);
        //}

        //private void ExecuteDelete()
        //{
        //    if (SelectedCart == null)
        //    {
        //        return;
        //    }
        //    ContactServerProxy.Current.Delete(SelectedCart.Cart.Id);
        //    RefreshCarts();
        //}

        public void RefreshCarts()
        {
            NotifyPropertyChanged(nameof(Carts));
        }
        public void UpdateCart()
        {
            if (SelectedCart?.Cart == null)
            {
                return;
            }
            Shell.Current.GoToAsync($"//Shop?cartId={SelectedCart.Cart.Id}");
            ShoppingCartService.Current.AddOrUpdate(SelectedCart.Cart);
        }
        public void DeleteCart()
        {
            if (SelectedCart?.Cart == null)
            {
                return;
            }

            ContactServerProxy.Current.Delete(SelectedCart.Cart.Id);
            RefreshCarts();
        }
        public ShoppingCart newCart;
        public ShoppingCart NewCart
        {
            get => newCart;
            set
            {
                newCart = value;
                NotifyPropertyChanged();
            }
        }

        //public ShoppingCart? NewCart { get; set; }
        public void AddCart()
        {
            if (NewCart != null)
            {
                ShoppingCartService.Current.AddOrUpdate(NewCart);
                NewCart = new ShoppingCart();
            }
            RefreshCarts() ;

            
        }

    }
}
