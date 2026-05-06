using MathGame.Models;

namespace MathGame.Views;

public partial class SelectGameMode : ContentPage
{
    private readonly Difficulty _difficulty;

    public SelectGameMode(Difficulty difficulty)
    {
        _difficulty = difficulty;
        InitializeComponent();
    }

    private void OnGameChosen(object? sender, EventArgs e)
    {
        var btn = (Button)sender;

        Navigation.PushAsync(new GamePage(btn.Text, _difficulty));
    }

    private async void OnBackToMenu(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }

    private async void OnPointerEntered(object sender, PointerEventArgs e)
    {
        await ((View)sender).ScaleToAsync(1.1, 100, Easing.Linear);
    }

    private async void OnPointerExited(object sender, PointerEventArgs e)
    {
        await ((View)sender).ScaleToAsync(1.0, 100, Easing.Linear);
    }
}
