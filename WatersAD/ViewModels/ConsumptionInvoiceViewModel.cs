using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatersAD.Models;
using WatersAD.Services;
using WatersAD.Views;

namespace WatersAD.ViewModels
{
	public partial class ConsumptionInvoiceViewModel : ObservableObject
	{
		private readonly ApiService _apiService;
		private readonly INavigationService _navigationService;

		[ObservableProperty]
		private ObservableCollection<Consumption> consumptions = null!;
	

		public IAsyncRelayCommand DetailsDataCommand { get; }
		public ConsumptionInvoiceViewModel(ApiService apiService, INavigationService navigationService)
        {
			
			_apiService = apiService;
			_navigationService = navigationService;

			
			DetailsDataCommand = new AsyncRelayCommand<int>(async (id) => await NavigateToInvoiceDetails(id));
		}

		private async Task NavigateToInvoiceDetails(int id)
		{
			var parameters = new Dictionary<string, object>
				{
					{ "InvoiceId", id }
				};
			await _navigationService.NavigateToAsync<InvoiceDetailsPage>(parameters);
		}

		public async void Initialize()
		{
			try
			{

				var email = Preferences.Get("username", string.Empty);
				if(string.IsNullOrEmpty(email))
				{
					return;
				}
				var response = await _apiService.GetConsumptionsAndInvoices(email);

				Consumptions = new ObservableCollection<Consumption>(response.Consumptions!);

				
			}
			catch (Exception)
			{
				await Application.Current!.MainPage!.DisplayAlert("Erro", "Não foi possível carregar a lista de países. Tente novamente mais tarde.", "OK");
			}


		}
	}
}
