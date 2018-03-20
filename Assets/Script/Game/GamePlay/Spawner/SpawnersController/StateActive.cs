using Core.StateMachine;

namespace GamePlay.Spawner.SpawnerController
{
    public class StateActive : IState<Event>
    {
        private SpawnersController controller;

        public StateActive(SpawnersController controller)
        {
            this.controller = controller;
        }

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

        public void OnEnter()
        {
        }

        public void OnExit()
        {
        }

        public void Update(Event info)
        {
            if (info.Name.Equals("SpaceUp"))
            {
                controller.StartChoosingSpawner();
            }
        }


    }
}
