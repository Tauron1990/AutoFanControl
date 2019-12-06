using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Auto_Fan_Control.Bus;
using Auto_Fan_Control.Configuration;
using Auto_Fan_Control.FanControl.Events;

namespace Auto_Fan_Control.FanControl.Components
{
    public class GoStandByComponent : IHandler<TrackingEvent>
    {
        private readonly Options _options;
        private State _lastState = State.Idle;
        private readonly Stopwatch _stopwatch = new Stopwatch();

        public GoStandByComponent(Options options) => _options = options;

        public async Task Handle(TrackingEvent msg, MessageBus messageBus)
        {
            if(msg.Error) return;

            try
            {
                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (_lastState)
                {
                    case State.StandBy when msg.State == State.StandBy:
                        if (_stopwatch.IsRunning && _stopwatch.Elapsed.Seconds < _options.GoStandbyTime)
                            await messageBus.Publish(new FanStartEvent());
                        else if (_stopwatch.IsRunning)
                            _stopwatch.Stop();
                        else
                            _stopwatch.Reset();
                        break;
                    case State.Power when msg.State == State.StandBy:
                        await messageBus.Publish(new FanStartEvent());
                        _stopwatch.Start();
                        break;
                }
            }
            finally
            {
                _lastState = msg.State;
            }
        }
    }
}