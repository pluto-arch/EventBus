using System;

namespace Pluto.EventBus.Abstractions.Events
{
    /// <summary>
    /// 集成事件基类
    /// </summary>
    public class IntegrationEvent
    {
        public Guid Id { get; set; }

        public IntegrationEvent()
        {
            Id=Guid.NewGuid();
        }
    }
}
