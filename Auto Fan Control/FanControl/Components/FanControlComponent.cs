using System;
using System.Threading;
using System.Threading.Tasks;
using Auto_Fan_Control.Bus;
using Auto_Fan_Control.Configuration;
using Auto_Fan_Control.FanControl.Events;

namespace Auto_Fan_Control.FanControl.Components
{
    public class FanControlComponent : IHandler<FanStartEvent>, IDisposable
    {
        private readonly Timer _timer;

        private readonly Options _options;
        private readonly Func<bool, Task> _statusChange;

        public FanControlComponent(Options options, Func<bool, Task> statusChange)
        {
            _timer = new Timer(StopFan);
            _options = options;
            _statusChange = statusChange;
        }

        public async Task Handle(FanStartEvent msg, MessageBus messageBus)
        {
            //TODO Start Fan

            await _statusChange(true);

            int time = (int) (_options.ClockTimeMs * _options.FanControlMultipler);

            _timer.Change(time, -1);
        }

        private async void StopFan(object state)
        {
            //TODO Stop Fan

            await _statusChange(false);
        }

        public void Dispose() => _timer.Dispose();
    }
}