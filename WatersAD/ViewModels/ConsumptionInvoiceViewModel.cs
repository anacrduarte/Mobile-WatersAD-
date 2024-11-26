using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
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

        [ObservableProperty]
        private int selectedYear;

        
        public ObservableCollection<int> Years { get; set; }

        public IAsyncRelayCommand DetailsDataCommand { get; }

		public ConsumptionInvoiceViewModel(ApiService apiService, INavigationService navigationService)
		{

			_apiService = apiService;
			_navigationService = navigationService;


			DetailsDataCommand = new AsyncRelayCommand<int>(async (id) => await NavigateToInvoiceDetails(id));

            Years = new ObservableCollection<int> { 2022, 2023, 2024, 2025, 2026 };

        }

		private async Task NavigateToInvoiceDetails(int id)
		{
			var parameters = new Dictionary<string, object>
				{
					{ "InvoiceId", id }
				};
			await _navigationService.NavigateToAsync<InvoiceDetailsPage>(parameters);
		}

        partial void OnSelectedYearChanged(int value)
        {
            FilterConsumptions();
        }
        public async void FilterConsumptions()
        {
            
            if (SelectedYear != 0)
            {
                Consumptions = new ObservableCollection<Consumption>(Consumptions.Where(c => c.ConsumptionDate.Year == SelectedYear));

				if (Consumptions.Count == 0)
				{
                    await Application.Current!.MainPage!.DisplayAlert("Aviso", "Não tem faturas na data selecionada.", "OK");
                    SelectedYear = 0;
                    Initialize();
                }
            }
			
        }

        public async void Initialize()
		{
			try
			{

				var email = Preferences.Get("username", string.Empty);
				if (string.IsNullOrEmpty(email))
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
