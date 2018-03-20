
namespace Core.StateMachine
{
    public interface IStateMachine<State, Trigger, UpdateInfo>
    {
        void UpdateCurrentState(UpdateInfo info);
        void ApplyTrigger(Trigger trigger);
        bool AddTransition(State srcState, Trigger trigger, State dstState);
    }
}
