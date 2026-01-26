using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace TestAvalonia2.Views.Documents;

public partial class ViewPicturesDocumentView : UserControl
{
    public ViewPicturesDocumentView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}