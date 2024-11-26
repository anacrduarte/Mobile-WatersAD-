using CommunityToolkit.Mvvm.ComponentModel;
using WatersAD.Models;
using WatersAD.Services;

namespace WatersAD.ViewModels
{
	public partial class InvoiceDetailsViewModel : ObservableObject
	{
		private readonly ApiService _apiService;
		private readonly INavigationService _navigationService;

		[ObservableProperty]
		private int invoiceId;

		[ObservableProperty]
		private InvoiceDetails invoices = null!;
		public InvoiceDetailsViewModel(ApiService apiService, INavigationService navigationService)
		{

			_apiService = apiService;
			_navigationService = navigationService;
		}

		public async void LoadInvoiceDetails(int Id)
		{
			try
			{



				var response = await _apiService.GetInvoiceDetais(Id);

				if (response.InvoiceDetails != null)
				{
					Invoices = response.InvoiceDetails!;
				}
				else
				{

					await Application.Current!.MainPage!.DisplayAlert("Aviso", "Nenhum detalhe encontrado para esta fatura.", "OK");
				}

			}
			catch (Exception)
			{
				await Application.Current!.MainPage!.DisplayAlert("Erro", "Não foi possível carregar os detalhes da fatura. Tente novamente mais tarde.", "OK");
			}
		}
	}
}
