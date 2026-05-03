namespace MathGame;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnGameChosen(object? sender, EventArgs e)
    {
        var btn = (Button)sender;

        Navigation.PushAsync(new GamePage(btn.Text));
    }

    private void OnViewPreviousGamesChosen(object? sender, EventArgs e)
    {
        Navigation.PushAsync(new PreviousGames());
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
