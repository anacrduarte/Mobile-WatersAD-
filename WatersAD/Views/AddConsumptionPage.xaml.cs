using WatersAD.ViewModels;

namespace WatersAD.Views;

public partial class AddConsumptionPage : ContentPage
{
	private readonly AddCosumptionViewModel _model;

	public AddConsumptionPage(AddCosumptionViewModel model)
	{
		InitializeComponent();
		_model = model;
		BindingContext = _model;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		await	_model.Initialize();


	}
}