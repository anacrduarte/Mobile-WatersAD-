using WatersAD.Services;
using WatersAD.Validator;
using WatersAD.ViewModels;
using WatersAD.Views;

namespace WatersAD
{
    public partial class AppShell : Shell
    {
        private readonly ApiService _apiService;
        private readonly IDataValidator _validator;
        private readonly AuthService _authService;
        private readonly INavigationService _navigationService;

        public AppShell(ApiService apiService, IDataValidator validator, AuthService authService, INavigationService navigationService)
        {
            InitializeComponent();
            _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
            _validator = validator;
            _authService = authService;
            _navigationService = navigationService;
            //Routing.RegisterRoute("homepage", typeof(HomePage));
            //Routing.RegisterRoute("servicePage", typeof(ServicePriceListPage));
            //Routing.RegisterRoute("requestPage", typeof(RequestWaterMeterPage));
            //Routing.RegisterRoute("perfilPage", typeof(ProfileSettingsPage));
            //Routing.RegisterRoute("loginPage", typeof(LoginPage));
            //Routing.RegisterRoute("addConsumptionPage", typeof(AddConsumptionPage));
            //Routing.RegisterRoute("invoicesPage", typeof(ConsumptionsAndInvoicesPage));
            //Routing.RegisterRoute("infoPage", typeof(InformationPage));

            ConfigureShell();
        }
        //private void ConfigureShell()
        //{
        //    bool isUserLoggedIn = _authService.IsLoggedIn();

        //    if (isUserLoggedIn)
        //    {
        //        Items.Add(new TabBar
        //        {
        //            Items =
        //    {
        //        new ShellContent
        //        {
        //            Title = "Home",
        //            Icon="home",
        //            Route = "homepage",
        //            ContentTemplate = new DataTemplate(() => new HomePage(new HomePageViewModel()))
        //        },
        //        new ShellContent
        //        {
        //            Title = "Preços",
        //            Icon="pricelist",
        //            Route = "servicePage",
        //            ContentTemplate = new DataTemplate(() => new ServicePriceListPage(new TiersViewModel(_apiService)))
        //        },
        //        new ShellContent
        //        {
        //            Title = "Leitura",
        //            Icon="watermeter",
        //            Route = "addConsumptionPage",
        //            ContentTemplate = new DataTemplate(() => new AddConsumptionPage(new AddCosumptionViewModel(_apiService, _navigationService)))
        //        },
        //        new ShellContent
        //        {
        //            Title = "Consumos",
        //            Icon="watertap",
        //            Route = "invoicesPage",
        //            ContentTemplate = new DataTemplate(() => new ConsumptionsAndInvoicesPage(new ConsumptionInvoiceViewModel(_apiService, _navigationService)))
        //        },
        //        new ShellContent
        //        {
        //            Title = "Conta",
        //            Icon="account",
        //            Route = "perfilPage",
        //            ContentTemplate = new DataTemplate(() => new ProfileSettingsPage(new ProfileSettingsViewModel(_authService, _apiService, _validator, _navigationService)))
        //        }
        //    }
        //        });
        //    }
        //    else
        //    {
        //        Items.Add(new TabBar
        //        {
        //            Items =
        //    {
        //        new ShellContent
        //        {
        //            Title = "Home",
        //            Icon="home",
        //            Route = "homepage",
        //            ContentTemplate = new DataTemplate(() => new HomePage(new HomePageViewModel()))
        //        },
        //        new ShellContent
        //        {
        //            Title = "Preços",
        //            Icon="pricelist",
        //            Route = "servicePage",
        //            ContentTemplate = new DataTemplate(() => new ServicePriceListPage(new TiersViewModel(_apiService)))
        //        },
        //        new ShellContent
        //        {
        //            Title = "Contador",
        //            Icon="watertap",
        //            Route = "requestPage",
        //            ContentTemplate = new DataTemplate(() => new RequestWaterMeterPage(new RequestWaterMeterViewModel(_apiService, _navigationService)))
        //        },
        //        new ShellContent
        //        {
        //            Title = "Info",
        //            Icon="info",
        //            Route = "infoPage",
        //            ContentTemplate = new DataTemplate(() => new InformationPage())
        //        },
        //        new ShellContent
        //        {
        //            Title = "Login",
        //            Icon="login",
        //            Route = "loginPage",
        //            ContentTemplate = new DataTemplate(() => new LoginPage(new LoginViewModel(_apiService, _validator, _authService, _navigationService)))
        //        }
        //    }
        //        });
        //    }
        //}


        private void ConfigureShell()
        {
            var homePage = new HomePage(new HomePageViewModel());
            var servicePage = new ServicePriceListPage(new TiersViewModel(_apiService));
            var requestPage = new RequestWaterMeterPage(new RequestWaterMeterViewModel(_apiService, _navigationService));
            var perfilPage = new ProfileSettingsPage(new ProfileSettingsViewModel(_authService, _apiService, _validator, _navigationService));
            var loginPage = new LoginPage(new LoginViewModel(_apiService, _validator, _authService, _navigationService));
            var addConsumptionPage = new AddConsumptionPage(new AddCosumptionViewModel(_apiService, _navigationService));
            var invoicesPage = new ConsumptionsAndInvoicesPage(new ConsumptionInvoiceViewModel(_apiService, _navigationService));
            var infoPage = new InformationPage();


            bool isUserLoggedIn = _authService.IsLoggedIn();

            if (isUserLoggedIn)
            {
                Items.Add(new TabBar
                {
                    Items =
                    {
                        new ShellContent {Title = "Home", Icon="home",Content = homePage  },
                        new ShellContent { Title = "Preços", Icon="pricelist",Content = servicePage },
                        new ShellContent { Title = "Leitura",Icon="watermeter",Content = addConsumptionPage},
                             new ShellContent { Title = "Consumos",Icon="watertap",Content = invoicesPage },

                        new ShellContent { Title = "Conta", Icon="account", Content = perfilPage }



                     }
                });
            }
            else
            {
                Items.Add(new TabBar
                {
                    Items =
                    {
                        new ShellContent {Title = "Home", Icon="home",Content = homePage  },
                        new ShellContent { Title = "Preços", Icon="pricelist",Content = servicePage },
                        new ShellContent { Title = "Contador",Icon="watertap",Content = requestPage },
                         new ShellContent { Title = "Info", Icon="info", Content = infoPage },
                        new ShellContent { Title = "Login", Icon="login", Content = loginPage }
                     }
                });

            }

        }

    }
}
