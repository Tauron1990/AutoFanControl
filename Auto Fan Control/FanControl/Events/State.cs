namespace Auto_Fan_Control.FanControl.Events
{
    public enum State
    {
        Error = 0,
        Idle,
        Ready,
        Ignition,
        StartUp,
        StandBy,
        Power,
        Cooldown,
        TestRun,
    }
}