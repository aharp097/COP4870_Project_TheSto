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
}