using WatersAD.ViewModels;

namespace WatersAD.Views;

public partial class HomePage : ContentPage
{
    private readonly HomePageViewModel _model;

    public HomePage(HomePageViewModel model)
	{
		InitializeComponent();
       
        _model = model;
        BindingContext = _model;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await StartTextRotation();

    }

    private async Task StartTextRotation()
    {
        await Task.Delay(500);
        while (true)
        {
            await ShowText(Text1);
            await Task.Delay(2000);
            await ShowText(Text2);
            await Task.Delay(2000);
            await ShowText(Text3);
            await Task.Delay(2000);
        }
    }


    private async Task ShowText(Label labelToShow)
    {

        Text1.IsVisible = false;
        Text2.IsVisible = false;
        Text3.IsVisible = false;


        labelToShow.Opacity = 0;
        labelToShow.IsVisible = true;
        for (double i = 0; i <= 1; i += 0.1)
        {
            labelToShow.Opacity = i;
            await Task.Delay(50);
        }
    }
}