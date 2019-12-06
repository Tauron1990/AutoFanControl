using System.Threading.Tasks;
using Auto_Fan_Control.Bus;
using Auto_Fan_Control.Configuration;
using Auto_Fan_Control.FanControl.Events;

namespace Auto_Fan_Control.FanControl.Components
{
    public class StartUpCoolingComponent : IHandler<TrackingEvent>
    {
        private readonly Options _options;

        public StartUpCoolingComponent(Options options) => _options = options;

        public async Task Handle(TrackingEvent msg, MessageBus messageBus)
        {
            if(msg.Error || msg.State != State.StartUp) return;

            if (msg.Pt1000 >= _options.MaxStartupTemp)
                await messageBus.Publish(new FanStartEvent());
        }
    }
}