using EventBusDemoAPI.Events;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Pluto.EventBus.Abstractions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBusDemoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IEventBus _eventBus;


        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IEventBus eventBus)
        {
            _logger = logger;
            _eventBus = eventBus;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            _eventBus.Publish(new DemoEvent {Name="123123"});
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
