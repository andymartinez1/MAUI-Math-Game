using MathGame.Models;

namespace MathGame.Views;

public partial class SelectDifficultyPage : ContentPage
{
    public SelectDifficultyPage()
    {
        InitializeComponent();
    }

    private void OnViewPreviousGamesChosen(object? sender, EventArgs e)
    {
        Navigation.PushAsync(new PreviousGamesPage());
    }

    private async void OnPointerEntered(object sender, PointerEventArgs e)
    {
        await ((View)sender).ScaleToAsync(1.1, 100, Easing.Linear);
    }

    private async void OnPointerExited(object sender, PointerEventArgs e)
    {
        await ((View)sender).ScaleToAsync(1.0, 100, Easing.Linear);
    }

    private async void OnDifficultyChosen(object? sender, EventArgs e)
    {
        if (sender is not Button button)
            return;

        var difficulty = button.Text switch
        {
            "Beginner" => Difficulty.Beginner,
            "Intermediate" => Difficulty.Intermediate,
            "Advanced" => Difficulty.Advanced,
            _ => Difficulty.Beginner,
        };

        await Navigation.PushAsync(new SelectGameMode(difficulty));
    }
}
