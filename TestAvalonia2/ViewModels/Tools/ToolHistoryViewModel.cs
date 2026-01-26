using CommunityToolkit.Mvvm.ComponentModel;
using Dock.Model.Mvvm.Controls;
using System;
using TestAvalonia2.Messages;
using TestAvalonia2.Services;

namespace TestAvalonia2.ViewModels.Tools
{
    internal partial class ToolHistoryViewModel : Tool
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IFileLogger _fileLogger;
        [ObservableProperty]
        private string _fileLogs;
        public ToolHistoryViewModel(IEventAggregator eventAggregator, IFileLogger fileLogger)
        {
            _fileLogger = fileLogger;
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe<HistoryMessage>(NewMessage);
        }
        private void NewMessage(HistoryMessage message)
        {
            FileLogs += "\r\n" + message.Message.ToString() ;
        }
    }
}
