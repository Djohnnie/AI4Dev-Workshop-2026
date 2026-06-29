#nullable disable

namespace LuckySlots;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;

    private Label titleLabel = null!;
    private Label subtitleLabel = null!;
    private Label reelOneLabel = null!;
    private Label reelTwoLabel = null!;
    private Label reelThreeLabel = null!;
    private Label creditsLabel = null!;
    private Label creditsValueLabel = null!;
    private Label betLabel = null!;
    private Label betValueLabel = null!;
    private Label payoutLabel = null!;
    private Label statusLabel = null!;
    private ComboBox betComboBox = null!;
    private Button spinButton = null!;
    private Button resetButton = null!;
    private System.Windows.Forms.Timer spinTimer = null!;
    private TableLayoutPanel reelsLayout = null!;
    private FlowLayoutPanel controlsPanel = null!;
    private Panel infoPanel = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components is not null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        titleLabel = new Label();
        subtitleLabel = new Label();
        reelOneLabel = new Label();
        reelTwoLabel = new Label();
        reelThreeLabel = new Label();
        creditsLabel = new Label();
        creditsValueLabel = new Label();
        betLabel = new Label();
        betValueLabel = new Label();
        payoutLabel = new Label();
        statusLabel = new Label();
        betComboBox = new ComboBox();
        spinButton = new Button();
        resetButton = new Button();
        spinTimer = new System.Windows.Forms.Timer(components);
        reelsLayout = new TableLayoutPanel();
        controlsPanel = new FlowLayoutPanel();
        infoPanel = new Panel();
        reelsLayout.SuspendLayout();
        controlsPanel.SuspendLayout();
        infoPanel.SuspendLayout();
        SuspendLayout();
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(30, 30, 46);
        ClientSize = new Size(960, 640);
        Controls.Add(statusLabel);
        Controls.Add(infoPanel);
        Controls.Add(controlsPanel);
        Controls.Add(reelsLayout);
        Controls.Add(subtitleLabel);
        Controls.Add(titleLabel);
        Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        ForeColor = Color.White;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "LuckySlots";
        // 
        // titleLabel
        // 
        titleLabel.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point);
        titleLabel.Location = new Point(24, 20);
        titleLabel.Name = "titleLabel";
        titleLabel.Size = new Size(450, 50);
        titleLabel.Text = "LuckySlots";
        // 
        // subtitleLabel
        // 
        subtitleLabel.ForeColor = Color.Gainsboro;
        subtitleLabel.Location = new Point(27, 72);
        subtitleLabel.Name = "subtitleLabel";
        subtitleLabel.Size = new Size(720, 25);
        subtitleLabel.Text = "A .NET 10 WinForms slot machine with credits, bets, animation, and payout rules.";
        // 
        // reelsLayout
        // 
        reelsLayout.BackColor = Color.FromArgb(44, 44, 70);
        reelsLayout.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        reelsLayout.ColumnCount = 3;
        reelsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
        reelsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
        reelsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
        reelsLayout.Controls.Add(reelOneLabel, 0, 0);
        reelsLayout.Controls.Add(reelTwoLabel, 1, 0);
        reelsLayout.Controls.Add(reelThreeLabel, 2, 0);
        reelsLayout.Location = new Point(27, 120);
        reelsLayout.Name = "reelsLayout";
        reelsLayout.RowCount = 1;
        reelsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        reelsLayout.Size = new Size(906, 180);
        // 
        // reelOneLabel
        // 
        reelOneLabel.Dock = DockStyle.Fill;
        reelOneLabel.Font = new Font("Segoe UI", 26F, FontStyle.Bold, GraphicsUnit.Point);
        reelOneLabel.ForeColor = Color.Gold;
        reelOneLabel.Location = new Point(4, 1);
        reelOneLabel.Name = "reelOneLabel";
        reelOneLabel.Size = new Size(295, 178);
        reelOneLabel.Text = "CHERRY";
        reelOneLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // reelTwoLabel
        // 
        reelTwoLabel.Dock = DockStyle.Fill;
        reelTwoLabel.Font = new Font("Segoe UI", 26F, FontStyle.Bold, GraphicsUnit.Point);
        reelTwoLabel.ForeColor = Color.Gold;
        reelTwoLabel.Location = new Point(306, 1);
        reelTwoLabel.Name = "reelTwoLabel";
        reelTwoLabel.Size = new Size(295, 178);
        reelTwoLabel.Text = "LEMON";
        reelTwoLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // reelThreeLabel
        // 
        reelThreeLabel.Dock = DockStyle.Fill;
        reelThreeLabel.Font = new Font("Segoe UI", 26F, FontStyle.Bold, GraphicsUnit.Point);
        reelThreeLabel.ForeColor = Color.Gold;
        reelThreeLabel.Location = new Point(608, 1);
        reelThreeLabel.Name = "reelThreeLabel";
        reelThreeLabel.Size = new Size(294, 178);
        reelThreeLabel.Text = "BELL";
        reelThreeLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // controlsPanel
        // 
        controlsPanel.Controls.Add(betLabel);
        controlsPanel.Controls.Add(betComboBox);
        controlsPanel.Controls.Add(spinButton);
        controlsPanel.Controls.Add(resetButton);
        controlsPanel.Location = new Point(27, 320);
        controlsPanel.Name = "controlsPanel";
        controlsPanel.Size = new Size(906, 52);
        controlsPanel.WrapContents = false;
        // 
        // betLabel
        // 
        betLabel.AutoSize = true;
        betLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
        betLabel.Location = new Point(3, 12);
        betLabel.Margin = new Padding(3, 12, 12, 0);
        betLabel.Name = "betLabel";
        betLabel.Size = new Size(76, 21);
        betLabel.Text = "Bet size:";
        // 
        // betComboBox
        // 
        betComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        betComboBox.FormattingEnabled = true;
        betComboBox.Items.AddRange([1, 5, 10]);
        betComboBox.Location = new Point(94, 10);
        betComboBox.Name = "betComboBox";
        betComboBox.Size = new Size(100, 25);
        betComboBox.SelectedIndex = 0;
        // 
        // spinButton
        // 
        spinButton.BackColor = Color.MediumSeaGreen;
        spinButton.FlatStyle = FlatStyle.Flat;
        spinButton.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
        spinButton.ForeColor = Color.White;
        spinButton.Location = new Point(214, 3);
        spinButton.Margin = new Padding(20, 3, 3, 3);
        spinButton.Name = "spinButton";
        spinButton.Size = new Size(140, 42);
        spinButton.Text = "Spin";
        spinButton.UseVisualStyleBackColor = false;
        spinButton.Click += spinButton_Click;
        // 
        // resetButton
        // 
        resetButton.BackColor = Color.SlateBlue;
        resetButton.FlatStyle = FlatStyle.Flat;
        resetButton.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
        resetButton.ForeColor = Color.White;
        resetButton.Location = new Point(360, 3);
        resetButton.Name = "resetButton";
        resetButton.Size = new Size(140, 42);
        resetButton.Text = "Reset Game";
        resetButton.UseVisualStyleBackColor = false;
        resetButton.Click += resetButton_Click;
        // 
        // infoPanel
        // 
        infoPanel.BackColor = Color.FromArgb(44, 44, 70);
        infoPanel.Controls.Add(payoutLabel);
        infoPanel.Controls.Add(betValueLabel);
        infoPanel.Controls.Add(creditsValueLabel);
        infoPanel.Controls.Add(creditsLabel);
        infoPanel.Location = new Point(27, 392);
        infoPanel.Name = "infoPanel";
        infoPanel.Size = new Size(906, 154);
        // 
        // creditsLabel
        // 
        creditsLabel.AutoSize = true;
        creditsLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
        creditsLabel.Location = new Point(22, 22);
        creditsLabel.Name = "creditsLabel";
        creditsLabel.Size = new Size(68, 21);
        creditsLabel.Text = "Credits:";
        // 
        // creditsValueLabel
        // 
        creditsValueLabel.AutoSize = true;
        creditsValueLabel.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
        creditsValueLabel.ForeColor = Color.Gold;
        creditsValueLabel.Location = new Point(96, 16);
        creditsValueLabel.Name = "creditsValueLabel";
        creditsValueLabel.Size = new Size(51, 30);
        creditsValueLabel.Text = "100";
        // 
        // betValueLabel
        // 
        betValueLabel.AutoSize = true;
        betValueLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
        betValueLabel.Location = new Point(190, 22);
        betValueLabel.Name = "betValueLabel";
        betValueLabel.Size = new Size(87, 21);
        betValueLabel.Text = "Current: 1";
        // 
        // payoutLabel
        // 
        payoutLabel.Location = new Point(22, 64);
        payoutLabel.Name = "payoutLabel";
        payoutLabel.Size = new Size(850, 74);
        payoutLabel.Text = "Payouts\r\n• 7 / 7 / 7 = 50x bet\r\n• Any 3 matching symbols = 10x bet\r\n• Any 2 matching symbols = 3x bet";
        // 
        // statusLabel
        // 
        statusLabel.BackColor = Color.FromArgb(62, 62, 96);
        statusLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
        statusLabel.Location = new Point(27, 566);
        statusLabel.Name = "statusLabel";
        statusLabel.Padding = new Padding(16, 12, 16, 12);
        statusLabel.Size = new Size(906, 50);
        statusLabel.Text = "New game. Spin when ready.";
        // 
        // spinTimer
        // 
        spinTimer.Interval = 100;
        spinTimer.Tick += spinTimer_Tick;
        // 
        // MainForm finalize
        // 
        reelsLayout.ResumeLayout(false);
        controlsPanel.ResumeLayout(false);
        controlsPanel.PerformLayout();
        infoPanel.ResumeLayout(false);
        infoPanel.PerformLayout();
        ResumeLayout(false);
    }
}
