namespace TestAvalonia2.Messages
{
    public class HistoryMessage
    {
        public string Message { get; set; }

        public HistoryMessage(string message)
        {
            Message = message;
        }
    }
}