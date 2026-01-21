using Dock.Model.Controls;
using Dock.Model.Core;

namespace TestAvalonia2.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IFactory? _factory;

        public IRootDock? Layout { get; set; }

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
    }
}