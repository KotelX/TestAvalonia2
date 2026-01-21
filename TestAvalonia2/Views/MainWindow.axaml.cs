using Avalonia.Controls;
using Dock.Model;
using Dock.Model.Core;
using Dock.Serializer;
using System.Collections.ObjectModel;
using TestAvalonia2.ViewModels;

namespace TestAvalonia2.Views
{
    public partial class MainWindow : Window
    {
        private IDockSerializer? _serializer;
        private IDockState? _dockState;
        public MainWindow()
        {
            InitializeComponent();
            InitializeDockState();
        }
        private void InitializeDockState()
        {
            _serializer = new DockSerializer(typeof(ObservableCollection<>));
            _dockState = new DockState();

            if (DataContext is MainWindowViewModel mainWindowViewModel)
            {
                var layout = mainWindowViewModel.Layout;
                if (layout != null)
                {
                    _dockState.Save(layout);
                }
            }
        }
    }
}