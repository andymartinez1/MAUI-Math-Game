using MathGame.Models;

namespace MathGame.Views;

public partial class GamePage : ContentPage
{
    private static readonly Random Random = new();
    private readonly Difficulty _difficulty;
    private readonly GameOperation _gameOperation;
    private readonly int _totalQuestions;
    private int _firstNum;
    private int _roundsLeft;
    private int _score;
    private int _secondNum;

    public GamePage(string gameType, Difficulty difficulty, int questionCount)
    {
        InitializeComponent();
        GameType = gameType;
        _difficulty = difficulty;
        _gameOperation = SelectGameType(gameType);
        _totalQuestions = Math.Max(1, questionCount);
        _roundsLeft = _totalQuestions;
        BindingContext = this;
        CreateNewQuestion();
    }

    public string GameType { get; set; }

    private void CreateNewQuestion()
    {
        var (min, max) = _difficulty switch
        {
            Difficulty.Beginner => (1, 10),
            Difficulty.Intermediate => (1, 25),
            Difficulty.Advanced => (1, 100),
            _ => (1, 10),
        };

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
                _firstNum = Random.Next(min, max);
                _secondNum = Random.Next(min, max);
            } while (_secondNum == 0 || _firstNum < _secondNum || _firstNum % _secondNum != 0);
        }
        else
        {
            _firstNum = Random.Next(min, max);
            _secondNum = Random.Next(min, max);
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
        GameOverLabel.Text = $"Game over. Your score is: {_score} out of {_totalQuestions}!";

        App.GameRepository.AddGame(
            new Game
            {
                DatePlayed = DateTime.Now,
                Difficulty = _difficulty,
                Type = _gameOperation,
                Questions = _totalQuestions,
                Score = _score,
            }
        );
    }

    private async void OnBackToMenu(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
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
