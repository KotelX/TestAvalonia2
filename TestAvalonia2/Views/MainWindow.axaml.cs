using Avalonia.Controls;

namespace TestAvalonia2.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeThemes();
        }
        private void InitializeThemes()
        {
            var dark = false;

            if (ThemeButton is not null)
            {
                ThemeButton.Click += (_, _) =>
                {
                    dark = !dark;
                    App.ThemeManager?.Switch(dark ? 1 : 0);
                };
            }
        }

        private void MenuItem_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            this.Close();
        }
    }
}