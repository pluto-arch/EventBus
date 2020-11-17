using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pluto.EventBus
{
    /// <summary>
    /// 订阅信息
    /// </summary>
    public class SubscriptionInfo
    {
        public Type HandlerType { get; }
        private SubscriptionInfo(Type handlerType)
        {
            HandlerType = handlerType;
        }

        public static SubscriptionInfo Typed(Type handlerType)
        {
            return new SubscriptionInfo(handlerType);
        }
    }
}
