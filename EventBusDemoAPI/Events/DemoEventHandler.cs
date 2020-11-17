using Microsoft.Extensions.Logging;
using Pluto.EventBus.Abstractions;
using System.Threading.Tasks;

namespace EventBusDemoAPI.Events
{
    public class DemoEventHandler : IIntegrationEventHandler<DemoEvent>
    {
        private readonly ILogger<DemoEventHandler> _logger;
        public DemoEventHandler(ILogger<DemoEventHandler> logger)
        {
            _logger = logger;
        }


        public Task Handle(DemoEvent @event)
        {
            _logger.LogInformation(@event.Name);
            return Task.CompletedTask;
        }
    }
}
