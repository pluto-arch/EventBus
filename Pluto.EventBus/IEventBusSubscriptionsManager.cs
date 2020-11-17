﻿using Pluto.EventBus.Abstractions;
using Pluto.EventBus.Events;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pluto.EventBus
{
    /// <summary>
    /// 事件总线订阅管理
    /// </summary>
    public interface IEventBusSubscriptionsManager
    {
        bool IsEmpty { get; }
        event EventHandler<string> OnEventRemoved;
        /// <summary>
        /// 添加订阅
        /// </summary>
        /// <typeparam name="T">事件类型</typeparam>
        /// <typeparam name="TH">事件处理程序</typeparam>
        void AddSubscription<T, TH>()
           where T : IntegrationEvent
           where TH : IIntegrationEventHandler<T>;

        /// <summary>
        /// 移除订阅
        /// </summary>
        /// <typeparam name="T">事件类型</typeparam>
        /// <typeparam name="TH">事件处理程序</typeparam>
        void RemoveSubscription<T, TH>()
             where TH : IIntegrationEventHandler<T>
             where T : IntegrationEvent;
      
        /// <summary>
        /// 事件T是否有订阅
        /// </summary>
        /// <typeparam name="T">事件类型</typeparam>
        /// <returns></returns>
        bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent;

        /// <summary>
        /// 事件eventName是否有订阅
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <returns></returns>
        bool HasSubscriptionsForEvent(string eventName);

        /// <summary>
        /// 根据名称获取事件类型
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <returns></returns>
        Type GetEventTypeByName(string eventName);

        /// <summary>
        /// 清除订阅
        /// </summary>
        void Clear();

        /// <summary>
        /// 获取事件的处理程序
        /// </summary>
        /// <typeparam name="T">事件类型</typeparam>
        /// <returns></returns>
        IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent;


        /// <summary>
        /// 获取事件的处理程序
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <returns></returns>
        IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);

        /// <summary>
        /// 获取事件的key
        /// </summary>
        /// <typeparam name="T">事件类型</typeparam>
        /// <returns></returns>
        string GetEventKey<T>();

    }
}
