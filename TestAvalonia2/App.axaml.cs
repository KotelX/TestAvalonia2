using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using TestAvalonia2.Themes;
using TestAvalonia2.ViewModels;
using TestAvalonia2.Views;

namespace TestAvalonia2
{
    public partial class App : Application
    {
        public static IThemeManager? ThemeManager;
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            ThemeManager = new FluentThemeManager();
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel()
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}