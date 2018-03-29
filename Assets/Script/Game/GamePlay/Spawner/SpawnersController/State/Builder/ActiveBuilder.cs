using Core.StateMachine.Builder;

namespace GamePlay.Spawner.SpawnerController.State.Builder
{
    public class ActiveBuilder : IBuilder
    {
        private static StateActive instance = null;

        public object Build()
        {
            if (instance == null)
            {
                instance = new StateActive();
            }

            return instance;
        }
    }
}
