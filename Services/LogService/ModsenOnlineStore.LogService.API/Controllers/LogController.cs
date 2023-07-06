using Microsoft.AspNetCore.Mvc;
using ModsenOnlineStore.LogService.Application.Interfaces;
using ModsenOnlineStore.LogService.Infrastructure.Services;


namespace ModsenOnlineStore.LogService.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly ILogService logService;
        public LogController(ILoggerFactory loggerFactory, ILogRepository repository, ILogService logService)
        {
            loggerFactory.AddProvider(new DBLoggerProvider(repository));
            logger = loggerFactory.CreateLogger("");

            this.logService = logService;
        }

        [HttpPost]
        public IActionResult AddLogAsync(int eventId, string context)
        {
            Func<string, Exception?, string> formatter = delegate (string state, Exception? exception)
            {
                var message = $"context: {context},";
                if (exception == null) message += "no errors";
                else message += $"error: {exception.Message}";

                return message;
            };

            LogLevel logLevel = LogLevel.Information;

            logger.Log(logLevel, eventId, context, null, formatter);

            return Ok("log added");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLogsAsync()
        {
            var response = await logService.GetAllLogsAsync();

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLogByIdAsync(int id)
        {
            var response = await logService.GetLogByIdAsync(id);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Data);
        }
    }
}
