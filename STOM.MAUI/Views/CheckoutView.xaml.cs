namespace STOM.MAUI.Views;
using STO.Library.Services;
using STO.Models;
using STOM.MAUI.ViewModels;
[QueryProperty(nameof(CartId), "cartId")]
public partial class CheckoutView : ContentPage
{
    public int CartId { get; set; }
    public CheckoutView()
	{
		InitializeComponent();
	}

    private void GoBack(object sender, EventArgs e)
    {
        //  BindingContext = null;
        Shell.Current.GoToAsync("//Cart");
    }
    //private void Checkout_Clicked(object sender, EventArgs e)
    //{

    //}

    private void CompleteTransaction(object sender, EventArgs e)
    {
        //  BindingContext = null;
        ShoppingCartService.Current.Delete(CartId);
        Shell.Current.GoToAsync("//Cart");
    }
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        ShoppingCartService.Current.currentID = CartId;
        BindingContext = new CheckoutViewModel(CartId);
    }
}