using WatersAD.ViewModels;

namespace WatersAD.Views;

public partial class ConsumptionsAndInvoicesPage : ContentPage
{
	private readonly ConsumptionInvoiceViewModel _model;

	public ConsumptionsAndInvoicesPage(ConsumptionInvoiceViewModel model)
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