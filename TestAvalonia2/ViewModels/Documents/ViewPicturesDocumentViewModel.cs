using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using Dock.Model.Mvvm.Controls;
using System;
using System.IO;
using TestAvalonia2.Messages;
using TestAvalonia2.Services;

namespace TestAvalonia2.ViewModels.Documents
{
    internal partial class ViewPicturesDocumentViewModel : Document, IDisposable
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IFileLogger _fileLogger;

        [ObservableProperty]
        private string _filePath;
        [ObservableProperty]
        private Bitmap? _imageSource;
        public ViewPicturesDocumentViewModel(IEventAggregator eventAggregator, IFileLogger fileLogger)
        {
            _eventAggregator = eventAggregator;
            _fileLogger = fileLogger;
            _eventAggregator.Subscribe<TreeItemSelectedMessage>(OnTreeItemSelected);
        }
        private void OnTreeItemSelected(TreeItemSelectedMessage message)
        {
            if (FilePath is not null) _eventAggregator.Publish(new HistoryMessage("Picture:" + FilePath?.ToString() + " clouse."));
            FilePath = message.ItemPath;
            LoadImage(message.ItemPath);
            _eventAggregator.Publish(new HistoryMessage("Picture:" + FilePath.ToString() + " open."));
        }
        private void LoadImage(string path)
        {
            try
            {
                ImageSource?.Dispose();

                if (File.Exists(path))
                {
                    ImageSource = new Bitmap(path);
                }
                else
                {
                    ImageSource = null;
                }
            }
            catch (Exception ex)
            {
                ImageSource = null;
            }
        }
        public void Dispose()
        {
            _eventAggregator.Unsubscribe<TreeItemSelectedMessage>(OnTreeItemSelected);
        }
    }
}
