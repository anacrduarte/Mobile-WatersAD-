using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using WatersAD.Models;
using WatersAD.Services;
using WatersAD.Views;

namespace WatersAD.ViewModels
{
	public partial class RequestWaterMeterViewModel : ObservableValidator
	{
		private readonly ApiService _apiService;
		private readonly INavigationService _navigationService;

		
		[ObservableProperty]
		[Required]
		private string firstName = null!;

		[ObservableProperty]
		[Required]
		private string lastName = null!;

		[ObservableProperty]
		[Required]
		private string address = null!;

		[ObservableProperty]
		[Required]
		private string email = null!;

		[ObservableProperty]
		[Required]
		private string nif = null!;

		[ObservableProperty]
		[Required]
		private string phoneNumber = null!;

		[ObservableProperty]
		[Required]
		private string houseNumber = null!;

		[ObservableProperty]
		[Required]
		private string postalCode = null!;

		[ObservableProperty]
		[Required]
		private string remainPostalCode = null!;

		[ObservableProperty]
		private Locality locality = null!;

		[ObservableProperty]
		private ObservableCollection<Locality> localities = null!;

		[ObservableProperty]
		private ObservableCollection<Country> countries = null!;

		[ObservableProperty]
		private ObservableCollection<City> cities = null!;

		[ObservableProperty]
		private Country selectedCountry = null!;

		[ObservableProperty]
		private City selectedCity = null!;

		[ObservableProperty]
		[Required]
		private string addressWaterMeter = null!;

		[ObservableProperty]
		[Required]
		private string houseNumberWaterMeter = null!;

		[ObservableProperty]
		[Required]
		private string postalCodeWaterMeter = null!;

		[ObservableProperty]
		[Required]
		private string remainPostalCodeWaterMeter = null!;

		[ObservableProperty]
		private Locality localityWM = null!;

		[ObservableProperty]
		private City cityWM = null!;

		[ObservableProperty]
		private ObservableCollection<Locality> localitiesWM = null!;

		[ObservableProperty]
		private ObservableCollection<Country> countriesWM = null!;

		[ObservableProperty]
		private ObservableCollection<City> citiesWM = null!;

		[ObservableProperty]
		private Country selectedCountryWM = null!;

		[ObservableProperty]
		private City selectedCityWM = null!;

		public IAsyncRelayCommand SendRequestCommand { get; }
		public RequestWaterMeterViewModel(ApiService apiService, INavigationService navigationService)
		{
			_apiService = apiService;
			_navigationService = navigationService;

			SendRequestCommand = new AsyncRelayCommand(SendRequestAsync);
		}

		public async void Initialize()
		{
			try
			{
				var response = await _apiService.GetCountries();

				Countries = new ObservableCollection<Country>(response.Countries!);

				CountriesWM = new ObservableCollection<Country>(response.Countries!);
			}
			catch (Exception)
			{
				await Application.Current!.MainPage!.DisplayAlert("Erro", "Não foi possível carregar a lista de países. Tente novamente mais tarde.", "OK");
			}


		}
		partial void OnSelectedCountryChanged(Country value)
		{

			OnCountrySelected();
		}

		partial void OnSelectedCityChanged(City value)
		{

			OnCitySelected();
		}

		partial void OnSelectedCountryWMChanged(Country value)
		{

			OnCountrySelected();
		}

		partial void OnSelectedCityWMChanged(City value)
		{

			OnCitySelected();
		}
		public async void OnCountrySelected()
		{
			if (SelectedCountry != null)
			{
				var response = await _apiService.GetCities(SelectedCountry.Id);

				Cities = new ObservableCollection<City>(response.Cities!);


			}
			if (SelectedCountryWM != null)
			{
				var response = await _apiService.GetCities(SelectedCountry!.Id);

				CitiesWM = new ObservableCollection<City>(response.Cities!);
			}
		}


		public async void OnCitySelected()
		{
			if (SelectedCity != null)
			{
				var response = await _apiService.GetLocalities(SelectedCity.Id);
				Localities = new ObservableCollection<Locality>(response.Localities!);
			}

			if (SelectedCityWM != null)
			{
				var response = await _apiService.GetLocalities(SelectedCity!.Id);
				LocalitiesWM = new ObservableCollection<Locality>(response.Localities!);
			}
		}

		public async Task SendRequestAsync()
		{
			

			string validationMessage = ValidateFields();

			if (!string.IsNullOrEmpty(validationMessage))
			{

				await Application.Current!.MainPage!.DisplayAlert("Erro", validationMessage, "OK");
				return;
			}
			try
			{

				var request = new RequestModel
				{
					FirstName = FirstName,
					LastName = LastName,
					Address = Address,
					Email = Email,
					NIF = Nif,
					PhoneNumber = PhoneNumber,
					HouseNumber = HouseNumber,
					PostalCode = PostalCode,
					RemainPostalCode = RemainPostalCode,
					LocalityId = Locality.Id,
					CityId = SelectedCity.Id,
					CountryId = SelectedCountry.Id,
					AddressWaterMeter = AddressWaterMeter,
					HouseNumberWaterMeter = HouseNumberWaterMeter,
					PostalCodeWaterMeter = PostalCodeWaterMeter,
					RemainPostalCodeWaterMeter = RemainPostalCodeWaterMeter,
					LocalityWaterMeterId = LocalityWM.Id,
					CityWaterMeterId = SelectedCityWM.Id,
					CountryWaterMeterId = SelectedCountryWM.Id
				};


				var response = await _apiService.AddRequest(request);

				if (response.Success)
				{

					await Application.Current!.MainPage!.DisplayAlert("Sucesso", "Pedido enviado com sucesso!", "OK");
					CleanFields();
					
                    return;
                }
				else
				{

					await Application.Current!.MainPage!.DisplayAlert("Erro", "Pedido não finalizado, por favor tente mais tarde", "OK");
					return;
				}
			}
			catch (Exception ex)
			{
				await Application.Current!.MainPage!.DisplayAlert("Erro", $"Erro {ex.Message}", "OK");
		
				await NavigateToHome();
            }
		}
        private async Task NavigateToHome()
        {

            await _navigationService.NavigateToAsync<HomePage>();
        }

        public void CleanFields()
		{
			FirstName = string.Empty;
					LastName = string.Empty;
            Address = string.Empty;
            Email = string.Empty;
            Nif = string.Empty;
            PhoneNumber = string.Empty;
            HouseNumber = string.Empty;
            PostalCode = string.Empty;
            RemainPostalCode = string.Empty;
          
					AddressWaterMeter = string.Empty;
            HouseNumberWaterMeter = string.Empty;
            PostalCodeWaterMeter = string.Empty;
            RemainPostalCodeWaterMeter = string.Empty;
          

        }
        public string ValidateFields()
        {
            var errors = new List<string>();

       
            if (string.IsNullOrWhiteSpace(FirstName))
                errors.Add("O campo 'Primeiro Nome' é obrigatório.");
            else if (FirstName.Length > 50)
                errors.Add("O campo 'Primeiro Nome' pode conter no máximo 50 caracteres.");

            if (string.IsNullOrWhiteSpace(LastName))
                errors.Add("O campo 'Último Nome' é obrigatório.");
            else if (LastName.Length > 50)
                errors.Add("O campo 'Último Nome' pode conter no máximo 50 caracteres.");

            if (string.IsNullOrWhiteSpace(Address))
                errors.Add("O campo 'Endereço' é obrigatório.");
            else if (Address.Length > 100)
                errors.Add("O campo 'Endereço' pode conter no máximo 100 caracteres.");
            if (string.IsNullOrWhiteSpace(Email))
                errors.Add("O campo 'Email' é obrigatório.");
            else if (!IsValidEmail(Email))
                errors.Add("O campo 'Email' não tem um formato válido.");
            if (string.IsNullOrWhiteSpace(Email))
                errors.Add("O campo 'Email' é obrigatório.");

            if (string.IsNullOrWhiteSpace(Nif))
                errors.Add("O campo 'NIF' é obrigatório.");
            else if (!Regex.IsMatch(Nif, @"^\d{9}$"))
                errors.Add("O 'NIF' deve ter exatamente 9 dígitos.");

            if (string.IsNullOrWhiteSpace(PhoneNumber))
                errors.Add("O campo 'Número de Telefone' é obrigatório.");

            if (string.IsNullOrWhiteSpace(HouseNumber))
                errors.Add("O campo 'Número da Casa' é obrigatório.");
            else if (HouseNumber.Length > 100)
                errors.Add("O campo 'Número da Casa' pode conter no máximo 100 caracteres.");

            if (string.IsNullOrWhiteSpace(PostalCode))
                errors.Add("O campo 'Código Postal' é obrigatório.");
            else if (PostalCode.Length != 4)
                errors.Add("O campo 'Código Postal' pode conter no máximo 4 caracteres.");

            if (string.IsNullOrWhiteSpace(RemainPostalCode))
                errors.Add("O campo 'Complemento do Código Postal' é obrigatório.");
            else if (RemainPostalCode.Length != 3)
                errors.Add("O campo 'Complemento do Código Postal' pode conter no máximo 3 caracteres.");

            if (string.IsNullOrWhiteSpace(AddressWaterMeter))
                errors.Add("O campo 'Morada do Contador' é obrigatório.");
            else if (AddressWaterMeter.Length > 100)
                errors.Add("O campo 'Morada do Contador' pode conter no máximo 100 caracteres.");

            if (string.IsNullOrWhiteSpace(HouseNumberWaterMeter))
                errors.Add("O campo 'Número da Casa do Contador' é obrigatório.");
            else if (HouseNumberWaterMeter.Length > 100)
                errors.Add("O campo 'Número da Casa do Contador' pode conter no máximo 100 caracteres.");

            if (string.IsNullOrWhiteSpace(PostalCodeWaterMeter))
                errors.Add("O campo 'Código Postal do Contador' é obrigatório.");
            else if (PostalCodeWaterMeter.Length != 4)
                errors.Add("O campo 'Código Postal do Contador' pode conter no máximo 4 caracteres.");

            if (string.IsNullOrWhiteSpace(RemainPostalCodeWaterMeter))
                errors.Add("O campo 'Complemento do Código Postal do Contador' é obrigatório.");
            else if (RemainPostalCodeWaterMeter.Length != 3)
                errors.Add("O campo 'Complemento do Código Postal do Contador' pode conter no máximo 3 caracteres.");

           

			if (errors.Any())
            {
                return string.Join("\n", errors);
            }

            return string.Empty;
        }

        private bool IsValidEmail(string email)
        {
            var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, emailPattern);
        }

    }
}
