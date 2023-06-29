using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModsenOnlineStore.LogService.Domain.Entities
{
    public class Log
    {
        public int Id { get; set; }
        public string logLevel { get; set; } = String.Empty;  // Trace/Debug/Information/Warning/Error/Critical/None
        public int eventId { get; set; }                      // = Convert.ToInt32(MyLogEvents.GetItems);
        // public Exception exception { get; set; } // not in use now
        public DateTime dateTime { get; set; }
        public string message { get; set; } = String.Empty;
        public int userId { get; set; }


    }

    public enum MyLogEvents
    {
        GetItems = 1,  // for http get request
        GetSingleItem, // for http get request
        UpdateItem,    // for http put request
        InsertItem,    // for http post request
        DeleteItem,    // for http get request
    }
}

