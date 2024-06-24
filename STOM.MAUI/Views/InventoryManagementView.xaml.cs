using STO.Library.Services;
using STO.Models;
using STOM.MAUI.ViewModels;
namespace STOM.MAUI.Views;

public partial class InventoryManagementView : ContentPage
{
	public InventoryManagementView()
	{
		InitializeComponent();
		BindingContext = new InventoryManagementViewModel();
	}

	private void EditClicked(object sender, EventArgs e)
	{
		(BindingContext as InventoryManagementViewModel)?.UpdateProduct();
	}

    private void AddClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Product");
    }
    private void GoBack(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as InventoryManagementViewModel)?.RefreshInventory();
    }

    private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {

    }
    private void InlineDelete_Clicked(object sender, EventArgs e)
    {
        (BindingContext as InventoryManagementViewModel)?.RefreshInventory();
    }
}