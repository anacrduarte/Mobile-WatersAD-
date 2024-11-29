using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using WatersAD.Models;
using WatersAD.Services;
using WatersAD.Views;

namespace WatersAD.ViewModels
{
    public partial class AddCosumptionViewModel : ObservableValidator
    {
        private readonly ApiService _apiService;
        private readonly INavigationService _navigationService;
        [ObservableProperty]
        private ObservableCollection<WaterMeter> waterMeters = null!;

        [ObservableProperty]
        private WaterMeter selectedWaterMeter = null!;

        [ObservableProperty]
        [Required]
        private double consumptionValue;

        [ObservableProperty]
        private bool isDateLoaded = false;

        [ObservableProperty]
        private DateTime consumptionDate;

        public IAsyncRelayCommand AddConsumptionCommand { get; }
        public AddCosumptionViewModel(ApiService apiService, INavigationService navigationService)
        {
            _apiService = apiService;
            _navigationService = navigationService;
            AddConsumptionCommand = new AsyncRelayCommand(AddConsumptionAsync);
        }

        public async Task Initialize()
        {

            if (DateTime.Now.Day >= 28 || DateTime.Now.Day <= 8)
            {
                try
                {
                    var email = Preferences.Get("username", string.Empty);
                    if (string.IsNullOrEmpty(email))
                    {
                        return;
                    }
                    var response = await _apiService.GetWaterMeters(email);

                    WaterMeters = new ObservableCollection<WaterMeter>(response.WaterMeters!);


                }
                catch (Exception)
                {
                    await Application.Current!.MainPage!.DisplayAlert("Erro", "Não foi possível carregar a lista de contadores. Tente novamente mais tarde.", "OK");
                    await NavigateToHome();
                }
            }
            else
            {
                await _navigationService.NavigateToAsync<InfoConsumptionPage>();
            }


        }

        partial void OnSelectedWaterMeterChanged(WaterMeter value)
        {
            _ = HandleWaterMeterSelection(value);
        }
        private async Task HandleWaterMeterSelection(WaterMeter value)
        {
            try
            {
                var response = await _apiService.GetLastDate(value.Id);
                if (response.Date == null)
                {
                    await Application.Current!.MainPage!.DisplayAlert("Erro", "Contador não encontrado", "OK");
                    return;
                }

                ConsumptionDate = (DateTime)response.Date;
                IsDateLoaded = true;


            }
            catch (Exception ex)
            {
                await Application.Current!.MainPage!.DisplayAlert("Erro", $"Erro {ex.Message}", "OK");
                await NavigateToHome();
              
            }
          


        }

        public async Task AddConsumptionAsync()
        {
            string validationMessage = ValidateFields();

            if (!string.IsNullOrEmpty(validationMessage))
            {

                await Application.Current!.MainPage!.DisplayAlert("Erro", validationMessage, "OK");
                return;
            }
            try
            {

                var request = new AddConsumption
                {
                    ConsumptionValue = ConsumptionValue,
                    ConsumptionDate = ConsumptionDate,
                    RegistrationDate = DateTime.Now,
                    WaterMeterId = SelectedWaterMeter.Id,
                    ClientId = SelectedWaterMeter.ClientId,


                };


                var response = await _apiService.AddConsumption(request);

                if (response.Success)
                {

                    await Application.Current!.MainPage!.DisplayAlert("Sucesso", "Consumo adicionado com sucesso!", "OK");
                 
                    await NavigateToHome();
                }
                else
                {

                    await Application.Current!.MainPage!.DisplayAlert("Erro", "Consumo não finalizado, por favor tente mais tarde, relembramos que o valor de consumo tem que ser superior ao mês anterior.", "OK");
                  
                    await NavigateToHome();
                }
            }
            catch (Exception ex)
            {
                await Application.Current!.MainPage!.DisplayAlert("Erro", $"Erro {ex.Message}", "OK");
          
                await NavigateToHome();
            }
        }

        public string ValidateFields()
        {
            var errors = new List<string>();


            if (ConsumptionValue == 0)
                errors.Add("O campo 'Valor de consumo' é obrigatório.");




            if (errors.Any())
            {
                return string.Join("\n", errors);
            }

            return string.Empty;
        }

        private async Task NavigateToHome()
        {

            await _navigationService.NavigateToAsync<HomePage>();
        }
    }
}
