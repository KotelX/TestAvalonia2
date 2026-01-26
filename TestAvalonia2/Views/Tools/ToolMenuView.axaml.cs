using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace TestAvalonia2.Views.Tools;

public partial class ToolMenuView : UserControl
{
    public ToolMenuView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}