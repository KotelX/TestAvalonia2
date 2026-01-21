using Avalonia.Controls;
using Avalonia.Layout;
using Dock.Model.Controls;
using Dock.Model.Core;

namespace TestAvalonia2.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly IFactory? _factory;
        public IRootDock Layout {  get; set; }
        public MainWindowViewModel()
        {
            _factory = new DockFactory();

            var layout = _factory.CreateLayout();
            _factory.InitLayout(layout);
            Layout = layout;

            if (Layout is { } root)
            {
                root.Navigate.Execute("Document1");
            }
        }
    }
}
