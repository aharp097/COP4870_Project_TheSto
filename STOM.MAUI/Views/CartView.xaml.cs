using STO.Library.Services;
using STOM.MAUI.ViewModels;

namespace STOM.MAUI.Views;

public partial class CartView : ContentPage
{
	public CartView()
    {
        InitializeComponent();
        BindingContext = new CartViewModel();
    }

    private void ShopClicked(object sender, EventArgs e)
    {
        (BindingContext as CartViewModel)?.UpdateCart();
    }

    private void AddClicked(object sender, EventArgs e)
    {
        (BindingContext as CartViewModel)?.AddCart();
    }
    private void GoBack(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as CartViewModel)?.RefreshCarts();
    }

    private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {

    }
    private void InlineDelete_Clicked(object sender, EventArgs e)
    {
        
        (BindingContext as CartViewModel)?.RefreshCarts();
    }
}