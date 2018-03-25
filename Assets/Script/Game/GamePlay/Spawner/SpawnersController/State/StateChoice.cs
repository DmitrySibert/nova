using Core.StateMachine;

namespace GamePlay.Spawner.SpawnerController
{
    public class StateChoice : AState<SpawnersController, Event>
    {
        public override bool Equals(object obj)
        {
            var active = obj as StateChoice;
            return active != null;
        }

        public override int GetHashCode()
        {
            int hash = this.GetType().GetHashCode();
            return hash;
        }


        public override void OnExit(SpawnersController target)
        {
            target.CommitSpawnerPosition();
        }

        public override void Update(SpawnersController target, Event info)
        {
            if (info.Name.Equals("RightArrowUp"))
            {
                target.SetNextSpawner();
            }
            if (info.Name.Equals("LeftArrowUp"))
            {
                target.SetPrevSpawner();
            }
        }
    }
}
