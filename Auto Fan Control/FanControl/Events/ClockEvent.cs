namespace Auto_Fan_Control.FanControl.Events
{
    public class ClockEvent
    {
        public ClockEvent(ClockState clockState)
        {
            ClockState = clockState;
        }

        public ClockState ClockState { get; }
    }
}