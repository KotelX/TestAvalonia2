using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace TestAvalonia2.Views.Documents;

public partial class DocumentView : UserControl
{
    public DocumentView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}