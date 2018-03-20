namespace Core.StateMachine
{
    public class EventBasedCSM : CommonStateMachine<int, Event>
    {
        public EventBasedCSM(IState<Event> initialState) : base(initialState)
        {
        }
    }
}
