using FamilyToDo.Data;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace FamilyToDo.Services
{
    internal interface IScopedProcessingService
    {
        void DoWork();
    }

    internal class ScopedProcessingService : IScopedProcessingService
    {
        private readonly ILogger _logger;
        private readonly ApplicationDbContext context;

        public ScopedProcessingService(ILogger<ScopedProcessingService> logger, ApplicationDbContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public void DoWork()
        {
            _logger.LogInformation("Timed Background Service is working.");
            System.Collections.Generic.List<Models.RepeatingTodo> repeatingTimers = context.RepeatingTodos.ToList();
            _logger.LogInformation($"repeating todos: {repeatingTimers}");
        }
    }
}
