using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatersAD.ViewModels
{
    public class HomePageViewModel
    {
        public ObservableCollection<string> ImageSources { get; set; }

        public HomePageViewModel()
        {
            ImageSources = new ObservableCollection<string>
        {
            "aaaa.jpg", 
            "aaaaa.jpg",
            "aaaaaa.jpg",
        
        };


        }
    }
}
