using STO.Models;
using STOM.MAUI.ViewModels;

namespace STOM.MAUI.Views;

public partial class ShopView : ContentPage
{
	public ShopView()
	{
		InitializeComponent();
        BindingContext = new ShopViewModel();
    }

    private void GoBack(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void Search_Clicked(object sender, EventArgs e)
    {
        (BindingContext as ShopViewModel).Search();
    }

    private void AddtoCart_Clicked(object sender, EventArgs e)
    {
        (BindingContext as ShopViewModel).AddToCart();
    }

    private void Checkout_Clicked(object sender, EventArgs e)
    {
        (BindingContext as ShopViewModel).Search();
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as ShopViewModel).RefreshInventory();
    }
}