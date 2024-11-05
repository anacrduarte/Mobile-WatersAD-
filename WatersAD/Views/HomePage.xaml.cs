using WatersAD.ViewModels;

namespace WatersAD.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
        BindingContext = new HomePageViewModel();
        StartTextRotation();
    }

    private async void StartTextRotation()
    {
        await Task.Delay(500);
        while (true)
        {
            await ShowText(Text1);
            await Task.Delay(2000); // Exibe o texto por 2 segundos
            await ShowText(Text2);
            await Task.Delay(2000);
            await ShowText(Text3);
            await Task.Delay(2000);
        }
    }

    private async Task ShowText(Label labelToShow)
    {
        // Oculta todos os textos
        Text1.IsVisible = false;
        Text2.IsVisible = false;
        Text3.IsVisible = false;

        // Mostra o texto desejado com uma animação de fade-in
        labelToShow.Opacity = 0;
        labelToShow.IsVisible = true;
        for (double i = 0; i <= 1; i += 0.1)
        {
            labelToShow.Opacity = i;
            await Task.Delay(50); // Pequeno atraso para simular o fade-in
        }
    }
}