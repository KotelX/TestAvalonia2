using CommunityToolkit.Mvvm.Input;
using Dock.Model.Mvvm.Controls;
using TestAvalonia2.Services;
using TestAvalonia2.ViewModels.Documents;

namespace TestAvalonia2.ViewModels.Docks
{
    internal class CustomDocumentDock : DocumentDock
    {
        private readonly IEventAggregator _eventAggregator = new EventAggregator();
        private readonly IFileLogger _fileLogger = new FileLogger();
        public CustomDocumentDock(IEventAggregator eventAggregator, IFileLogger fileLogger)
        {
            _eventAggregator = eventAggregator;
            _fileLogger = fileLogger;
            CreateDocument = new RelayCommand(CreateNewDocument);
        }

        private void CreateNewDocument()
        {
            if (!CanCreateDocument)
            {
                return;
            }

            var index = VisibleDockables?.Count + 1;
            var document = new DocumentViewModel() { Id = $"Document{index}", Title = $"Document{index}" };

            Factory?.AddDockable(this, document);
            Factory?.SetActiveDockable(document);
            Factory?.SetFocusedDockable(this, document);
        }
    }
}
