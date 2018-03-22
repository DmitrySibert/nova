namespace Core.StateMachine
{
    public class EventBasedCSM<Target> : CommonStateMachine<Target, int, Event>
    {
        public EventBasedCSM(Target target, AState<Target, Event> initialState) : base(target, initialState)
        {
        }
    }
}

