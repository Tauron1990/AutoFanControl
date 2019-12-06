using System;
using System.Threading.Tasks;
using Auto_Fan_Control.Bus;
using Auto_Fan_Control.FanControl.Events;

namespace Auto_Fan_Control.FanControl.Components
{
    public class TrackingEventDeliveryComponent : IHandler<TrackingEvent>
    {
        private readonly Func<TrackingEvent, Task> _invoker;

        public TrackingEventDeliveryComponent(Func<TrackingEvent, Task> invoker) => _invoker = invoker;

        public Task Handle(TrackingEvent msg, MessageBus messageBus) => _invoker(msg);
    }
}