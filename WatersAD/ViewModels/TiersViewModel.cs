using CommunityToolkit.Mvvm.ComponentModel;
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
                // Limpa a coleção existente
                AllTiers.Clear();

                // Adiciona todos os tiers recebidos à coleção
                foreach (var tier in response.Tiers)
                {
                    AllTiers.Add(tier);
                }

                // Se você precisa usar o primeiro, ainda pode fazer isso
                //var firstTier = response.Tiers.First();
                //TierName = firstTier.TierName;
                //TierPrice = firstTier.TierPrice;
                //UpperLimit = firstTier.UpperLimit;
            }

        }
    }
}
