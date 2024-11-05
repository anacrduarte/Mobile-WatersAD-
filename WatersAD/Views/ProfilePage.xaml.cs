using WatersAD.ViewModels;

namespace WatersAD.Views;

public partial class ProfilePage : ContentPage
{
	private readonly ProfileViewModel _model;

	public ProfilePage(ProfileViewModel model)
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