using MathGame.Models;

namespace MathGame.Views;

public partial class SelectGameModePage : ContentPage
{
    private readonly Difficulty _difficulty;

    public SelectGameModePage(Difficulty difficulty)
    {
        _difficulty = difficulty;
        InitializeComponent();
    }

    private async void OnGameChosen(object? sender, EventArgs e)
    {
        if (sender is not Button btn || string.IsNullOrWhiteSpace(btn.Text))
            return;

        await Navigation.PushAsync(new SelectQuestionsPage(btn.Text, _difficulty));
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
