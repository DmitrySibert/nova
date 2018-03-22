using Core.StateMachine;

namespace GamePlay.Spawner.SpawnerController
{
    public class StateActive : AState<SpawnersController, Event>
    {
        public override bool Equals(object obj)
        {
            var active = obj as StateActive;
            return active != null;
        }

        public override int GetHashCode()
        {
            int hash = this.GetType().GetHashCode();
            return hash;
        }

        public override void Update(SpawnersController target, Event info)
        {
            if (info.Name.Equals("SpaceUp"))
            {
                target.StartChoosingSpawner();
            }
        }


    }
}
