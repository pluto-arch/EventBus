using System.Threading.Tasks;
using Pluto.EventBus.Abstractions.Events;

namespace Pluto.EventBus.Abstractions
{
    /// <summary>
    /// 集成事件处理类
    /// </summary>
    /// <typeparam name="TIntegrationEvent"></typeparam>
    public interface IIntegrationEventHandler<in TIntegrationEvent> 
        : IIntegrationEventHandler where TIntegrationEvent : IntegrationEvent
    {
        /// <summary>
        /// 处理事件
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        Task Handle(TIntegrationEvent @event);
    }

    public interface IIntegrationEventHandler
    {
    }
}
