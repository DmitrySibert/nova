using Core.StateMachine;

namespace GamePlay.Spawner.SpawnerController
{
    public class StateOff : AState<SpawnersController, Event>
    {
        public override bool Equals(object obj)
        {
            var active = obj as StateOff;
            return active != null;
        }

        public override int GetHashCode()
        {
            int hash = this.GetType().GetHashCode();
            return hash;
        }
    }
}
