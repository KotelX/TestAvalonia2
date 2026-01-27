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

            _layout = _factory?.CreateLayout();
            if (_layout is not null)
            {
                _factory?.InitLayout(_layout);
            }
        }

        [RelayCommand]
        private void RestoreLayout(object item)
        {
;
        }
    }
}