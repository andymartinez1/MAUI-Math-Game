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

    public string DifficultyDisplay => _game.Difficulty.ToString();

    public string TypeDisplay => _game.Type.ToString();

    public int Score => _game.Score;

    public DateTime DatePlayed => _game.DatePlayed;
}
