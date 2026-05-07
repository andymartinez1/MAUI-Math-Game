using MathGame.Models;

namespace MathGame.Views;

public partial class SelectQuestionsPage : ContentPage
{
    private readonly string _gameType;
    private readonly Difficulty _difficulty;

    public SelectQuestionsPage(string gameType, Difficulty difficulty)
    {
        _gameType = gameType;
        _difficulty = difficulty;
        InitializeComponent();
    }

    private async void OnQuestionsSelected(object? sender, EventArgs e)
    {
        if (!int.TryParse(QuestionSelectionEntry.Text, out var questionCount) || questionCount <= 0)
        {
            await DisplayAlertAsync(
                "Invalid question count",
                "Please enter a whole number greater than 0.",
                "OK"
            );
            return;
        }

        await Navigation.PushAsync(new GamePage(_gameType, _difficulty, questionCount));
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
