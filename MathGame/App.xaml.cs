using MathGame.Data;

namespace MathGame;

public partial class App : Application
{
    public App(GameRepository gameRepository)
    {
        InitializeComponent();

        GameRepository = gameRepository;
    }

    public static GameRepository GameRepository { get; set; }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}
