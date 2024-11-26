using WatersAD.ViewModels;

namespace WatersAD.Views;

public partial class ProfileSettingsPage : ContentPage
{
    private readonly ProfileSettingsViewModel _model;

    public ProfileSettingsPage(ProfileSettingsViewModel model)
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