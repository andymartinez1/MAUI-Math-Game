using SQLite;

namespace MathGame.Models;

public class Game
{
    [PrimaryKey]
    [AutoIncrement]
    public int Id { get; set; }

    public Difficulty Difficulty { get; set; }

    public GameOperation Type { get; set; }

    public int Score { get; set; }

    public DateTime DatePlayed { get; set; }
}

public enum GameOperation
{
    Addition,
    Subtraction,
    Multiplication,
    Division,
}

public enum Difficulty
{
    Beginner,
    Intermediate,
    Advanced,
}
