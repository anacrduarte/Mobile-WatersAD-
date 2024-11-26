using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WatersAD.ViewModels
{
    public partial class HomePageViewModel : ObservableObject
    {
        public ObservableCollection<string> ImageSources { get; set; }

        [ObservableProperty]
        private int position;
      

        public IAsyncRelayCommand GoToPreviousCommand { get; }
        public IAsyncRelayCommand GoToNextCommand { get; }

        public HomePageViewModel()
        {
            ImageSources = new ObservableCollection<string>
        {
            "aaaa.jpg",
            "aaaaa.jpg",
            "aaaaaa.jpg"
        };

            GoToPreviousCommand = new AsyncRelayCommand(GoToPrevious);
            GoToNextCommand = new AsyncRelayCommand(GoToNext);
        }

        private async Task GoToPrevious()
        {
            if (Position > 0)
            {
                Position -= 1;
            }
            else
            {
                Position = ImageSources.Count - 1; 
            }
            await Task.Delay(200); 
        }

        private async Task GoToNext()
        {
            if (Position < ImageSources.Count - 1)
            {
                Position += 1;
            }
            else
            {
                Position = 0; 
            }
            await Task.Delay(200); 
        }


    }
}
