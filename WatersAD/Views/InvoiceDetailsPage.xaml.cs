using WatersAD.ViewModels;

namespace WatersAD.Views;

public partial class InvoiceDetailsPage : ContentPage
{
	private readonly InvoiceDetailsViewModel _model;

	public InvoiceDetailsPage(InvoiceDetailsViewModel model)
	{
		InitializeComponent();
		_model = model;
		
	}


	protected override void OnAppearing()
	{
		base.OnAppearing();



		if (BindingContext is Dictionary<string, object> parameters)
		{
			if (parameters.ContainsKey("InvoiceId"))
			{
				int invoiceId = (int)parameters["InvoiceId"];
				_model.LoadInvoiceDetails(invoiceId);
				BindingContext = _model;

			}
		}
	}
}
