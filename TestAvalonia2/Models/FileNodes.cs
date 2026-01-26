using System.Collections.ObjectModel;

namespace TestAvalonia2.Models
{
    internal class FileNodes
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public string FullPath { get; set; }
        public bool IsFile { get; set; }
        public ObservableCollection<FileNodes> Children { get; set; }
        public bool IsExpanded { get; set; }
        public bool IsSelected { get; set; }
    }
}
