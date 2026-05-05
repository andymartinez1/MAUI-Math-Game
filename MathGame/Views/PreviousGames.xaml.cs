namespace MathGame;

public partial class PreviousGames : ContentPage
{
    public PreviousGames()
    {
        InitializeComponent();

        GamesList.ItemsSource = App.GameRepository.GetAllGames();
    }

    private void OnDelete(object? sender, EventArgs e)
    {
        var button = (Button)sender;

        App.GameRepository.Delete((int)button.BindingContext);

        GamesList.ItemsSource = App.GameRepository.GetAllGames();
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
