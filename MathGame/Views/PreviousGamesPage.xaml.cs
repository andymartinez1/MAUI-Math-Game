using MathGame.ViewModels;

namespace MathGame;

public partial class PreviousGamesPage : ContentPage
{
    public PreviousGamesPage()
    {
        InitializeComponent();

        LoadGames();
    }

    private void LoadGames()
    {
        var games = App.GameRepository.GetAllGames();
        GamesList.ItemsSource = games.Select(g => new GameViewModel(g)).ToList();
    }

    private void OnDelete(object? sender, EventArgs e)
    {
        var button = (Button)sender;

        App.GameRepository.Delete((int)button.BindingContext);

        LoadGames();
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
