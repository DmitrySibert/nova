using Core.StateMachine;

namespace GamePlay.Spawner.SpawnerController
{
    public class StateChoice : IState<Event>
    {
        private SpawnersController controller;

        public StateChoice(SpawnersController controller)
        {
            this.controller = controller;
        }

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

        public void OnEnter()
        {
        }

        public void OnExit()
        {
            controller.CommitSpawnerPosition();
        }

        public void Update(Event info)
        {
            if (info.Name.Equals("RightArrowUp"))
            {
                controller.SetNextSpawner();
            }
            if (info.Name.Equals("LeftArrowUp"))
            {
                controller.SetPrevSpawner();
            }
        }
    }
}
