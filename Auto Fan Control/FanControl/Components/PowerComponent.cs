using System.Threading.Tasks;
using Auto_Fan_Control.Bus;
using Auto_Fan_Control.FanControl.Events;

namespace Auto_Fan_Control.FanControl.Components
{
    public class PowerComponent : IHandler<TrackingEvent>
    {
        public async Task Handle(TrackingEvent msg, MessageBus messageBus)
        {
            if (msg.State == State.Power)
                await messageBus.Publish(new FanStartEvent());
        }
    }
}