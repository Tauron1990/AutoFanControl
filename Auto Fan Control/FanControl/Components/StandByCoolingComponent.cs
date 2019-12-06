using System.Threading.Tasks;
using Auto_Fan_Control.Bus;
using Auto_Fan_Control.Configuration;
using Auto_Fan_Control.FanControl.Events;

namespace Auto_Fan_Control.FanControl.Components
{
    public class StandByCoolingComponent : IHandler<TrackingEvent>
    {
        private readonly Options _options;

        public StandByCoolingComponent(Options options) => _options = options;

        public async Task Handle(TrackingEvent msg, MessageBus messageBus)
        {
            if(msg.Error) return;
            if(msg.State != State.StandBy || msg.Pt1000 < _options.MaxStandbyTemp) return;

            await messageBus.Publish(new FanStartEvent());
        }
    }
}