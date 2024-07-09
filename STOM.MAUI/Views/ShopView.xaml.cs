using STO.Library.Services;
using STO.Models;
using STOM.MAUI.ViewModels;

namespace STOM.MAUI.Views;
[QueryProperty(nameof(CartId), "cartId")]
public partial class ShopView : ContentPage
{
    public int CartId { get; set; }
    public ShopView()
	{
		InitializeComponent();
        //BindingContext = new ShopViewModel(CartId);
    }

    private void GoBack(object sender, EventArgs e)
    {
      //  BindingContext = null;
        Shell.Current.GoToAsync("//Cart");
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
        (BindingContext as ShopViewModel).CheckOut();
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        ShoppingCartService.Current.currentID = CartId;
        BindingContext = new ShopViewModel(CartId);
    }
}