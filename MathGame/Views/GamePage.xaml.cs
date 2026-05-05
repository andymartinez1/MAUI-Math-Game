using MathGame.Models;

namespace MathGame;

public partial class GamePage : ContentPage
{
    private const int TotalQuestions = 3;
    private static readonly Random Random = new();
    private readonly GameOperation _gameOperation;
    private int _firstNum;
    private int _roundsLeft = TotalQuestions;
    private int _score;
    private int _secondNum;

    public GamePage(string gameType)
    {
        InitializeComponent();
        GameType = gameType;
        _gameOperation = SelectGameType(gameType);
        BindingContext = this;
        CreateNewQuestion();
    }

    public string GameType { get; set; }

    private void CreateNewQuestion()
    {
        var gameOperator = _gameOperation switch
        {
            GameOperation.Addition => "+",
            GameOperation.Subtraction => "-",
            GameOperation.Multiplication => "*",
            GameOperation.Division => "/",
            _ => string.Empty,
        };

        if (_gameOperation == GameOperation.Division)
        {
            do
            {
                _firstNum = Random.Next(1, 99);
                _secondNum = Random.Next(1, 99);
            } while (_firstNum < _secondNum || _firstNum % _secondNum != 0);
        }
        else
        {
            _firstNum = Random.Next(1, 9);
            _secondNum = Random.Next(1, 9);
        }

        QuestionLabel.Text = $"{_firstNum} {gameOperator} {_secondNum}";
    }

    private void OnAnswerSubmitted(object? sender, EventArgs e)
    {
        if (!int.TryParse(AnswerEntry.Text, out var answer))
        {
            AnswerLabel.Text = "Please enter a valid number.";
            AnswerEntry.Text = string.Empty;
            return;
        }

        var isCorrect = _gameOperation switch
        {
            GameOperation.Addition => answer == _firstNum + _secondNum,
            GameOperation.Subtraction => answer == _firstNum - _secondNum,
            GameOperation.Multiplication => answer == _firstNum * _secondNum,
            GameOperation.Division => answer == _firstNum / _secondNum,
            _ => false,
        };

        ProcessAnswer(isCorrect);
        _roundsLeft--;
        AnswerEntry.Text = "";

        if (_roundsLeft > 0)
            CreateNewQuestion();
        else
            GameOver();
    }

    private void ProcessAnswer(bool isCorrect)
    {
        if (isCorrect)
            _score++;

        AnswerLabel.Text = isCorrect ? "Correct!" : "Incorrect.";
    }

    private void GameOver()
    {
        QuestionArea.IsVisible = false;
        BackToMenu.IsVisible = true;
        GameOverLabel.Text = $"Game over. Your score is: {_score} out of {TotalQuestions}!";

        App.GameRepository.AddGame(
            new Game
            {
                DatePlayed = DateTime.Now,
                Type = _gameOperation,
                Score = _score,
            }
        );
    }

    private void OnBackToMenu(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MainPage());
    }

    private static GameOperation SelectGameType(string gameType)
    {
        return gameType switch
        {
            "Addition" => GameOperation.Addition,
            "Subtraction" => GameOperation.Subtraction,
            "Multiplication" => GameOperation.Multiplication,
            "Division" => GameOperation.Division,
            _ => throw new ArgumentException($"Unknown game type: {gameType}", nameof(gameType)),
        };
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
