using STOM.MAUI.ViewModels;
namespace STOM.MAUI.Views;

public partial class ImportView : ContentPage
{
    public ImportView()
    {
        InitializeComponent();
        BindingContext = new ImportViewModel();
    }
    private void ImportClicked(object sender, EventArgs e)
    {
        (BindingContext as ImportViewModel).ImportCSV();
        Shell.Current.GoToAsync("//Inventory");
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Inventory");
    }
}