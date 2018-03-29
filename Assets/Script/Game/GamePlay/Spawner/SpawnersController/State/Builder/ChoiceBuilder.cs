using Core.StateMachine.Builder;

namespace GamePlay.Spawner.SpawnerController.State.Builder
{
    public class ChoiceBuilder : IBuilder
    {
        private static StateChoice instance = null;

        public object Build()
        {
            if (instance == null)
            {
                instance = new StateChoice();
            }

            return instance;
        }
    }
}