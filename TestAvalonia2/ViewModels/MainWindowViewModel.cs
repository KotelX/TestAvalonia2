using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Dock.Model.Controls;
using Dock.Model.Core;

namespace TestAvalonia2.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        private IRootDock? _layout;
        private readonly IFactory? _factory;

        public MainWindowViewModel()
        {
            _factory = new DockFactory();

            var layout = _factory?.CreateLayout();
            if (layout is not null)
            {
                _factory?.InitLayout(layout);
            }
            Layout = layout;
        }

        [RelayCommand]
        private void RestoreLayout(object item)
        {
            if (Layout is not null)
            {
                if (Layout.Close.CanExecute(null))
                {
                    Layout.Close.Execute(null);
                }
            }

            var layout = _factory?.CreateLayout();
            if (layout is not null)
            {
                _factory?.InitLayout(layout);
                Layout = layout;
            }
        }
    }
}