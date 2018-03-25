namespace Core.StateMachine
{
    public abstract class AState<Target, UpdateInfo>
    {
        public virtual void OnEnter(Target target) { }
        public virtual void Update(Target target, UpdateInfo info) { }
        public virtual void OnExit(Target target) { }
    }
}
