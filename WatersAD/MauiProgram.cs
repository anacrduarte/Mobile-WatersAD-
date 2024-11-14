using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using WatersAD.Services;
using WatersAD.Validator;
using WatersAD.ViewModels;
using WatersAD.Views;

namespace WatersAD
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
				.UseMauiCommunityToolkit()
				.ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("DMSans-Regular.ttf", "DMRegular");
                    fonts.AddFont("DMSans-Medium.ttf", "DMMedium");
                    fonts.AddFont("DMSans-Bold.ttf", "DMBold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<ApiService>();
            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddSingleton<IDataValidator, DataValidator>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<ProfileViewModel>();
            builder.Services.AddTransient<TiersViewModel>();
            builder.Services.AddTransient<HomePageViewModel>();
            builder.Services.AddTransient<ProfileSettingsViewModel>();
			builder.Services.AddTransient<ChangePasswordViewModel>();
			builder.Services.AddTransient<RecoverPasswordViewModel>();
			builder.Services.AddSingleton<INavigationService, NavigationService>();
			builder.Services.AddTransient<ProfilePage>();

			builder.Services.AddSingleton<AppShell>();
			builder.Services.AddSingleton<LoginPage>();
			builder.Services.AddSingleton<ChangePasswordPage>();
			builder.Services.AddSingleton<ForgotPasswordPage>();
			builder.Services.AddSingleton<ProfileSettingsPage>();
			builder.Services.AddSingleton<QuestionsPage>();
			return builder.Build();
        }
    }
}
