using Core.StateMachine.Builder;

namespace GamePlay.Spawner.SpawnerController.State.Builder
{
    public class OffBuilder : IBuilder
    {
        private static StateOff instance = null;

        public object Build()
        {
            if(instance == null)
            {
                instance = new StateOff();
            }

            return instance;
        }
    }
}
