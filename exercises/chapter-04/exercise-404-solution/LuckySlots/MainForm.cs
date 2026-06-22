namespace LuckySlots;

public partial class MainForm : Form
{
    private readonly Random _random = new();
    private readonly string[] _symbols = ["CHERRY", "LEMON", "BELL", "7"];

    private int _credits = StartingCredits;
    private int _spinTicksRemaining;
    private bool _isSpinning;

    private const int StartingCredits = 100;

    public MainForm()
    {
        InitializeComponent();
        UpdateDisplay();
    }

    private void spinButton_Click(object? sender, EventArgs e)
    {
        if (_isSpinning)
        {
            return;
        }

        var bet = GetSelectedBet();
        if (_credits < bet)
        {
            statusLabel.Text = "Not enough credits for that bet. Lower the bet or reset the game.";
            return;
        }

        _credits -= bet;
        _isSpinning = true;
        _spinTicksRemaining = 12;

        spinButton.Enabled = false;
        betComboBox.Enabled = false;
        statusLabel.Text = "Spinning...";

        spinTimer.Start();
        UpdateDisplay();
    }

    private void resetButton_Click(object? sender, EventArgs e)
    {
        spinTimer.Stop();
        _isSpinning = false;
        _credits = StartingCredits;
        _spinTicksRemaining = 0;

        reelOneLabel.Text = "CHERRY";
        reelTwoLabel.Text = "LEMON";
        reelThreeLabel.Text = "BELL";
        betComboBox.SelectedIndex = 0;

        statusLabel.Text = "New game. Spin when ready.";
        UpdateDisplay();
    }

    private void spinTimer_Tick(object? sender, EventArgs e)
    {
        reelOneLabel.Text = GetRandomSymbol();
        reelTwoLabel.Text = GetRandomSymbol();
        reelThreeLabel.Text = GetRandomSymbol();

        _spinTicksRemaining--;
        if (_spinTicksRemaining > 0)
        {
            return;
        }

        spinTimer.Stop();
        _isSpinning = false;

        var result = EvaluateSpin(
            reelOneLabel.Text,
            reelTwoLabel.Text,
            reelThreeLabel.Text,
            GetSelectedBet());

        _credits += result.Winnings;
        statusLabel.Text = result.Message;

        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        var bet = GetSelectedBet();

        creditsValueLabel.Text = _credits.ToString();
        betValueLabel.Text = bet.ToString();

        var canAffordBet = _credits >= bet;
        betComboBox.Enabled = !_isSpinning;
        spinButton.Enabled = !_isSpinning && canAffordBet && _credits > 0;

        if (_credits <= 0 && !_isSpinning)
        {
            statusLabel.Text = "Game Over. You are out of credits. Press Reset Game to start again.";
        }
    }

    private int GetSelectedBet()
    {
        return betComboBox.SelectedItem is int bet ? bet : 1;
    }

    private string GetRandomSymbol() => _symbols[_random.Next(_symbols.Length)];

    private static SpinResult EvaluateSpin(string reelOne, string reelTwo, string reelThree, int bet)
    {
        var reels = new[] { reelOne, reelTwo, reelThree };

        if (reels.All(symbol => symbol == "7"))
        {
            var winnings = bet * 50;
            return new SpinResult(winnings, $"Jackpot! Three 7s pays {winnings} credits.");
        }

        if (reels.Distinct().Count() == 1)
        {
            var winnings = bet * 10;
            return new SpinResult(winnings, $"Big win! Three matching symbols pays {winnings} credits.");
        }

        if (reelOne == reelTwo || reelOne == reelThree || reelTwo == reelThree)
        {
            var winnings = bet * 3;
            return new SpinResult(winnings, $"Nice! Two matching symbols pays {winnings} credits.");
        }

        return new SpinResult(0, $"No match. You lost {bet} credits.");
    }

    private readonly record struct SpinResult(int Winnings, string Message);
}
