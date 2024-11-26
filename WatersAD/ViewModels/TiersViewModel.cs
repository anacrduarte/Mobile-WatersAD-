using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using WatersAD.Models;
using WatersAD.Services;

namespace WatersAD.ViewModels
{
    public partial class TiersViewModel : ObservableObject
    {
        private readonly ApiService _apiService;
        [ObservableProperty]
        private double tierPrice;

        [ObservableProperty]
        private string tierName = null!;

        [ObservableProperty]
        private double upperLimit;

        public ObservableCollection<Tiers> AllTiers { get; set; } = new ObservableCollection<Tiers>();

        public TiersViewModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async void Initialize()
        {

            var response = await _apiService.GetTiers();

            if (response.Tiers != null && response.Tiers.Any())
            {

                AllTiers.Clear();

                foreach (var tier in response.Tiers)
                {
                    AllTiers.Add(tier);
                }

            }

        }
    }
}
