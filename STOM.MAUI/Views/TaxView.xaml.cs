using STOM.MAUI.ViewModels;
namespace STOM.MAUI.Views;

public partial class TaxView : ContentPage
{
	public TaxView()
	{
		InitializeComponent();
        BindingContext = new TaxViewModel();
	}
    private void SetClicked(object sender, EventArgs e)
    {
        (BindingContext as TaxViewModel).UpdateTax();
        Shell.Current.GoToAsync("//Inventory");
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Inventory");
    }
}