using WatersAD.ViewModels;

namespace WatersAD.Views;

public partial class RequestWaterMeterPage : ContentPage
{
	private readonly RequestWaterMeterViewModel _model;

	public RequestWaterMeterPage(RequestWaterMeterViewModel model)
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