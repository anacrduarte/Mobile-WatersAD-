using WatersAD.ViewModels;

namespace WatersAD.Views;

public partial class ServicePriceListPage : ContentPage
{
    private readonly TiersViewModel _model;

    public ServicePriceListPage(TiersViewModel model)
    {
        InitializeComponent();
        _model = model;
        BindingContext = _model;
   
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        _model.Initialize();


    }
}