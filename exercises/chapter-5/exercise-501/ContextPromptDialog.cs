using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ContextPromptExtension;

internal sealed class ContextPromptDialog : Window
{
    private readonly ContextPromptOrchestrator orchestrator;
    private readonly TextBox promptBox;
    private readonly ComboBox contextBox;
    private readonly TextBlock statusText;
    private readonly TextBlock statsText;
    private readonly TextBox previewBox;
    private readonly TextBox responseBox;
    private readonly Button runButton;
    private bool isPreviewLoading;

    public ContextPromptDialog(ContextPromptOrchestrator orchestrator)
    {
        this.orchestrator = orchestrator;

        Title = "Context Prompt Demo";
        Width = 920;
        Height = 760;
        WindowStartupLocation = WindowStartupLocation.CenterOwner;
        ResizeMode = ResizeMode.CanResize;
        Background = Brushes.White;

        var root = new Grid
        {
            Margin = new Thickness(16)
        };

        root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // 0 instruction
        root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // 1 promptBox
        root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // 2 contextBox
        root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // 3 buttons
        root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // 4 statusText
        root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // 5 statsText
        root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // 6 previewHeader
        root.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }); // 7 previewBox
        root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // 8 responseHeader
        root.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }); // 9 responseBox

        root.Children.Add(new TextBlock
        {
            Text = "Type a prompt, choose a context level, and see how much better the answer gets when the model sees your code.",
            TextWrapping = TextWrapping.Wrap,
            FontSize = 14,
            Margin = new Thickness(0, 0, 0, 12)
        });

        promptBox = new TextBox
        {
            AcceptsReturn = true,
            TextWrapping = TextWrapping.Wrap,
            VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
            MinHeight = 90,
            FontFamily = new FontFamily("Consolas"),
            Margin = new Thickness(0, 0, 0, 12)
        };
        Grid.SetRow(promptBox, 1);
        root.Children.Add(promptBox);

        contextBox = new ComboBox
        {
            ItemsSource = new[]
            {
                new ContextOption(ContextLevel.NoContext, "No context"),
                new ContextOption(ContextLevel.FimContext, "FIM context"),
                new ContextOption(ContextLevel.IdeContext, "IDE context")
            },
            DisplayMemberPath = nameof(ContextOption.Label),
            SelectedIndex = 0,
            Margin = new Thickness(0, 0, 0, 12),
            MinWidth = 220
        };
        contextBox.SelectionChanged += (_, _) => _ = RefreshPreviewAsync();
        Grid.SetRow(contextBox, 2);
        root.Children.Add(contextBox);

        var buttons = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Right,
            Margin = new Thickness(0, 0, 0, 12)
        };

        runButton = new Button
        {
            Content = "Run",
            Padding = new Thickness(18, 6, 18, 6),
            Margin = new Thickness(0, 0, 8, 0)
        };
        runButton.Click += (_, _) => _ = RunAsync();

        var closeButton = new Button
        {
            Content = "Close",
            Padding = new Thickness(18, 6, 18, 6)
        };
        closeButton.Click += (_, _) => Close();

        buttons.Children.Add(runButton);
        buttons.Children.Add(closeButton);
        Grid.SetRow(buttons, 3);
        root.Children.Add(buttons);

        statusText = new TextBlock
        {
            Text = "Ready.",
            Margin = new Thickness(0, 0, 0, 4),
            Foreground = Brushes.DimGray
        };
        Grid.SetRow(statusText, 4);
        root.Children.Add(statusText);

        statsText = new TextBlock
        {
            Text = "—",
            Margin = new Thickness(0, 0, 0, 12),
            Foreground = Brushes.DimGray,
            FontFamily = new FontFamily("Consolas"),
            FontSize = 12
        };
        Grid.SetRow(statsText, 5);
        root.Children.Add(statsText);

        var previewHeader = new TextBlock
        {
            Text = "Composed prompt preview (updates when context mode changes):",
            Margin = new Thickness(0, 0, 0, 6),
            Foreground = Brushes.DimGray
        };
        Grid.SetRow(previewHeader, 6);
        root.Children.Add(previewHeader);

        previewBox = new TextBox
        {
            AcceptsReturn = true,
            TextWrapping = TextWrapping.Wrap,
            VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
            IsReadOnly = true,
            FontFamily = new FontFamily("Consolas"),
            Background = Brushes.WhiteSmoke,
            MinHeight = 120,
            Margin = new Thickness(0, 0, 0, 12)
        };
        Grid.SetRow(previewBox, 7);
        root.Children.Add(previewBox);

        var responseHeader = new TextBlock
        {
            Text = "Model response:",
            Margin = new Thickness(0, 0, 0, 6),
            Foreground = Brushes.DimGray
        };
        Grid.SetRow(responseHeader, 8);
        root.Children.Add(responseHeader);

        responseBox = new TextBox
        {
            AcceptsReturn = true,
            TextWrapping = TextWrapping.Wrap,
            VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
            IsReadOnly = true,
            FontFamily = new FontFamily("Consolas"),
            Background = Brushes.WhiteSmoke,
            MinHeight = 120
        };
        Grid.SetRow(responseBox, 9);
        root.Children.Add(responseBox);

        Content = root;
        Loaded += (_, _) => _ = RefreshPreviewAsync();
    }

    private async Task RunAsync()
    {
        var prompt = promptBox.Text.Trim();
        if (string.IsNullOrWhiteSpace(prompt))
        {
            MessageBox.Show(this, "Enter a prompt first.", "Context Prompt Demo", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        if (contextBox.SelectedItem is not ContextOption selected)
        {
            MessageBox.Show(this, "Choose a context level.", "Context Prompt Demo", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        SetBusy(true, "Collecting context and asking the model...");
        responseBox.Clear();
        statsText.Text = "—";

        try
        {
            var result = await orchestrator.RunAsync(prompt, selected.Level, CancellationToken.None);
            responseBox.Text = result.Response;

            var inputCost = result.InputTokens * AzureOpenAiChatService.InputCostPer1MTokens / 1_000_000m;
            var outputCost = result.OutputTokens * AzureOpenAiChatService.OutputCostPer1MTokens / 1_000_000m;
            var totalCost = inputCost + outputCost;
            statsText.Text =
                $"Input: {result.InputTokens:N0} tokens (€{inputCost:F6})  |  " +
                $"Output: {result.OutputTokens:N0} tokens (€{outputCost:F6})  |  " +
                $"Total: €{totalCost:F6}";

            statusText.Text = "Done.";
        }
        catch (Exception ex)
        {
            statusText.Text = "The request failed.";
            MessageBox.Show(this, ex.Message, "Context Prompt Demo", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            SetBusy(false, statusText.Text);
        }
    }

    private void SetBusy(bool isBusy, string message)
    {
        runButton.IsEnabled = !isBusy;
        contextBox.IsEnabled = !isBusy;
        promptBox.IsEnabled = !isBusy;
        statusText.Text = message;
    }

    private async Task RefreshPreviewAsync()
    {
        if (!runButton.IsEnabled || isPreviewLoading)
        {
            return;
        }

        if (contextBox.SelectedItem is not ContextOption selected)
        {
            previewBox.Text = "Choose a context level to preview what will be sent.";
            return;
        }

        isPreviewLoading = true;
        var prompt = string.IsNullOrWhiteSpace(promptBox.Text)
            ? "(no prompt entered yet)"
            : promptBox.Text.Trim();

        previewBox.Text = "Collecting context preview...";

        try
        {
            previewBox.Text = await orchestrator.BuildComposedPromptAsync(prompt, selected.Level, CancellationToken.None);
        }
        catch (Exception ex)
        {
            previewBox.Text = $"Could not collect context preview:{Environment.NewLine}{ex.Message}";
        }
        finally
        {
            isPreviewLoading = false;
        }
    }

    private sealed record ContextOption(ContextLevel Level, string Label);
}
