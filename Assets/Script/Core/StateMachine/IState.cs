namespace Core.StateMachine
{
    public interface IState<UpdateInfo>
    {
        void OnEnter();
        void Update(UpdateInfo info);
        void OnExit();
    }
}
