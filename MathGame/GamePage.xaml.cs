namespace MathGame;

public partial class GamePage : ContentPage
{
    private int firstNum;
    private int score;
    private int secondNum;

    public GamePage(string gameType)
    {
        InitializeComponent();
        GameType = gameType;
        BindingContext = this;
        CreateNewQuestion();
    }

    public string GameType { get; set; }

    private void CreateNewQuestion()
    {
        var gameOperand = GameType switch
        {
            "Addition" => "+",
            "Subtraction" => "-",
            "Multiplication" => "*",
            "Division" => "/",
            _ => "",
        };

        var random = new Random();

        firstNum = GameType != "Division" ? random.Next(1, 9) : random.Next(1, 99);
        secondNum = GameType != "Division" ? random.Next(1, 9) : random.Next(1, 99);

        if (GameType == "Division")
            while (firstNum < secondNum || firstNum % secondNum != 0)
            {
                firstNum = random.Next(1, 99);
                secondNum = random.Next(1, 99);
            }

        QuestionLabel.Text = $"{firstNum} {gameOperand} {secondNum}";
    }

    private void OnAnswerSubmitted(object? sender, EventArgs e)
    {
        var answer = int.Parse(AnswerEntry.Text);
        var isCorrect = false;

        switch (GameType)
        {
            case "Addition":
                isCorrect = answer == firstNum + secondNum;
                ProcessAnswer(isCorrect);
                break;
            case "Subtraction":
                isCorrect = answer == firstNum - secondNum;
                ProcessAnswer(isCorrect);
                break;
            case "Multiplication":
                isCorrect = answer == firstNum * secondNum;
                ProcessAnswer(isCorrect);
                break;
            case "Division":
                isCorrect = answer == firstNum / secondNum;
                ProcessAnswer(isCorrect);
                break;
        }
    }

    private void ProcessAnswer(bool isCorrect)
    {
        if (isCorrect)
            score++;

        AnswerLabel.Text = isCorrect ? "Correct!" : "Incorrect.";
    }
}
