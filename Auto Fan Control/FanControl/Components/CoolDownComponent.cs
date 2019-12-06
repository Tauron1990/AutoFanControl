using System.Threading.Tasks;
using Auto_Fan_Control.Bus;
using Auto_Fan_Control.FanControl.Events;

namespace Auto_Fan_Control.FanControl.Components
{
    public sealed class CoolDownComponent : IHandler<TrackingEvent>
    {
        public async Task Handle(TrackingEvent msg, MessageBus messageBus)
        {
            if(msg.Error) return;
            if (msg.State == State.Cooldown)
                await messageBus.Publish(new FanStartEvent());
        }
    }
}