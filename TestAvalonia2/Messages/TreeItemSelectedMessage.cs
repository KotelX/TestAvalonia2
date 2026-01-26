namespace TestAvalonia2.Messages
{
    public class TreeItemSelectedMessage
    {
        public string ItemPath { get; set; }

        public TreeItemSelectedMessage(string itemPath)
        {
            ItemPath = itemPath;
        }
    }
}