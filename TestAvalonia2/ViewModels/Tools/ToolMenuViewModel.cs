using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Dock.Model.Mvvm.Controls;
using System.Collections.ObjectModel;
using System.IO;
using TestAvalonia2.Messages;
using TestAvalonia2.Models;

namespace TestAvalonia2.ViewModels.Tools
{
    internal partial class ToolMenuViewModel : Tool
    {
        public ObservableCollection<FileNodes> RootNodes { get; set; }
        private readonly IEventAggregator _eventAggregator;

        [ObservableProperty]
        private object _selectedTreeItem;

        public ToolMenuViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            RootNodes = new ObservableCollection<FileNodes>();
            LoadFilesStructure();
        }

        [RelayCommand]
        private void TreeItemDoubleClick(object item)
        {
            if (item is FileNodes fileNode)
                _eventAggregator.Publish(new TreeItemSelectedMessage(fileNode.FullPath));
        }


        private void LoadFilesStructure()
        {
            string assetsPath = @"C:\Users\user\source\repos\TestAvalonia2\TestAvalonia2\Assets\Files\";
            var rootNode = new FileNodes
            {
                Name = "Assets/Files",
                FullPath = assetsPath,
                IsFile = false,
                Children = new ObservableCollection<FileNodes>()
            };
            RootNodes.Add(rootNode);
            LoadDirectory(rootNode, assetsPath);
        }

        private void LoadDirectory(FileNodes parentNode, string directoryPath)
        {
            foreach (var dir in Directory.GetDirectories(directoryPath))
            {
                var dirName = Path.GetFileName(dir);
                var dirNode = new FileNodes
                {
                    Name = dirName,
                    FullPath = dir,
                    IsFile = false,
                    Children = new ObservableCollection<FileNodes>()
                };
                parentNode.Children.Add(dirNode);
                LoadDirectory(dirNode, dir);
            }

            foreach (var file in Directory.GetFiles(directoryPath))
            {
                var fileInfo = new FileInfo(file);
                var fileNode = new FileNodes
                {
                    Name = fileInfo.Name,
                    FullPath = file,
                    IsFile = true,
                    Children = new ObservableCollection<FileNodes>()
                };
                parentNode.Children.Add(fileNode);
            }
        }
    }
}