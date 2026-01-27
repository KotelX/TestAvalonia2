using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using System;
using TestAvalonia2.Services;
using TestAvalonia2.Themes;
using TestAvalonia2.ViewModels;
using TestAvalonia2.Views;

namespace TestAvalonia2
{
    public partial class App : Application
    {
        public IServiceProvider Services { get; private set; }
        public static IThemeManager? ThemeManager;
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            var services = new ServiceCollection();
            ConfigureServices(services);
            Services = services.BuildServiceProvider();
            ThemeManager = Services.GetRequiredService<IThemeManager>();
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

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IEventAggregator, EventAggregator>();
            services.AddSingleton<IThemeManager, FluentThemeManager>();

            services.AddSingleton<MainWindowViewModel>();
        }
    }
}