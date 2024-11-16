using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Org.Apache.Http.Protocol;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatersAD.Models;
using WatersAD.Services;

namespace WatersAD.ViewModels
{
    public partial class RequestWaterMeterViewModel : ObservableObject
	{
		private readonly ApiService _apiService;
		private readonly INavigationService _navigationService;

		[ObservableProperty]
		private string firstName;

		[ObservableProperty]
		private string lastName;

		[ObservableProperty]
		private string address;

		[ObservableProperty]
		private string email;


		[ObservableProperty]
		private string nif;

		[ObservableProperty]
		private string phoneNumber;

		[ObservableProperty]
		private string houseNumber;

		[ObservableProperty]
		private string postalCode;

		[ObservableProperty]
		private string remainPostalCode;

		[ObservableProperty]
		private Locality locality;

		//[ObservableProperty]
		//private int cityId;

		//[ObservableProperty]
		//private int countryId;

		[ObservableProperty]
		private ObservableCollection<Locality> localities;

		[ObservableProperty]
		private ObservableCollection<Country> countries;

		[ObservableProperty]
		private ObservableCollection<City> cities;

		[ObservableProperty]
		private Country selectedCountry;

		[ObservableProperty]
		private City selectedCity;

		[ObservableProperty]
		private string addressWaterMeter;

		[ObservableProperty]
		private string houseNumberWaterMeter;

		[ObservableProperty]
		private string postalCodeWaterMeter;

		[ObservableProperty]
		private string remainPostalCodeWaterMeter;

		[ObservableProperty]
		private int localityWaterMeterId;

		//[ObservableProperty]
		//private int cityWaterMeterId;

		//[ObservableProperty]
		//private int countryWaterMeterId;

		[ObservableProperty]
		private Locality localityWM;

		[ObservableProperty]
		private City cityWM;

		[ObservableProperty]
		private ObservableCollection<Locality> localitiesWM;

		[ObservableProperty]
		private ObservableCollection<Country> countriesWM;

		[ObservableProperty]
		private ObservableCollection<City> citiesWM;

		[ObservableProperty]
		private Country selectedCountryWM;

		[ObservableProperty]
		private City selectedCityWM;

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

				Countries = new ObservableCollection<Country>(response.Countries);

				CountriesWM = new ObservableCollection<Country>(response.Countries);
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

				Cities = new ObservableCollection<City>(response.Cities);

				
			}
			if(SelectedCountryWM != null)
			{
				var response = await _apiService.GetCities(SelectedCountry.Id);

				CitiesWM = new ObservableCollection<City>(response.Cities);
			}
		}


		public async void OnCitySelected()
		{
			if (SelectedCity != null)
			{
				var response = await _apiService.GetLocalities(SelectedCity.Id);
				Localities = new ObservableCollection<Locality>(response.Localities);
			}

			if (SelectedCityWM != null)
			{
				var response = await _apiService.GetLocalities(SelectedCity.Id);
				LocalitiesWM = new ObservableCollection<Locality>(response.Localities);
			}
		}

		public async Task SendRequestAsync()
		{
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
					await _navigationService.SetMainPageAsync<AppShell>();
				}
				else
				{

					await Application.Current!.MainPage!.DisplayAlert("Erro", "Pedido não finalizado, por favor tente mais tarde", "OK");
					await _navigationService.SetMainPageAsync<AppShell>();
				}
			}
			catch (Exception ex)
			{
				await Application.Current!.MainPage!.DisplayAlert("Erro", $"Erro {ex.Message}", "OK");
				await _navigationService.SetMainPageAsync<AppShell>();
			}
		}
	}
}
