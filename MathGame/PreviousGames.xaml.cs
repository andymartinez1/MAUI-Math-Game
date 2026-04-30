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
}
