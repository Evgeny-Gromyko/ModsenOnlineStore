namespace ModsenOnlineStore.LogService.Domain.Entities
{
    public class Log
    {
        public int Id { get; set; }
        public string LogLevel { get; set; } = String.Empty;  
        public int EventId { get; set; }                      
        // public Exception? Exception { get; set; }
        public DateTime DateTime { get; set; }
        public string Message { get; set; } = String.Empty;
        public int UserId { get; set; }
    }

    public enum MyLogEvents // for EventId
    {
        GetItems = 1,  // for http get request
        GetSingleItem, // for http get request
        UpdateItem,    // for http put request
        InsertItem,    // for http post request
        DeleteItem,    // for http get request
    }
}

