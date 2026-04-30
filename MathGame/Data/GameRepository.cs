using MathGame.Models;
using SQLite;

namespace MathGame.Data;

public class GameRepository
{
    private readonly string _dbPath;
    private SQLiteConnection _connection;

    public GameRepository(string dbPath)
    {
        _dbPath = dbPath;
    }

    public void Init()
    {
        _connection = new SQLiteConnection(_dbPath);
        _connection.CreateTable<Game>();
    }

    public void AddGame(Game game)
    {
        Init();
        _connection.Insert(game);
    }

    public List<Game> GetAllGames()
    {
        Init();
        return _connection.Table<Game>().ToList();
    }

    public void Delete(int id)
    {
        Init();
        _connection.Delete(new Game { Id = id });
    }
}
