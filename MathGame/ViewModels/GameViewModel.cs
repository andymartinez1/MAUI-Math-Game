using MathGame.Models;

namespace MathGame.ViewModels;

public class GameViewModel
{
    private readonly Game _game;

    public GameViewModel(Game game)
    {
        _game = game;
    }

    public int Id => _game.Id;

    public int TotalQuestions => _game.Questions;

    public string DifficultyDisplay => _game.Difficulty.ToString();

    public string TypeDisplay => _game.Type.ToString();

    public string Score => $"{_game.Score} out of {TotalQuestions}";

    public DateTime DatePlayed => _game.DatePlayed;
}
