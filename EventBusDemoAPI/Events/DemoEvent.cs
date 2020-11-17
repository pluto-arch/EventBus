using Pluto.EventBus.Events;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBusDemoAPI.Events
{
    public class DemoEvent: IntegrationEvent
    {
        public string Name { get; set; }
    }
}
