using UnityEngine;
using Core.StateMachine;
using GamePlay.Spawner.SpawnerController;
using GamePlay.Spawner.SpawnerController.State.Builder;

namespace Assets.Script.Core
{
    public class StateFactoryMono : MonoBehaviour
    {
        private StateFactory m_stateFactory;

        private void Awake()
        {
            m_stateFactory = new StateFactory();
            m_stateFactory.Bind<AState<SpawnersController, Event>>("Active", new ActiveBuilder());
            m_stateFactory.Bind<AState<SpawnersController, Event>>("Choice", new ChoiceBuilder());
            m_stateFactory.Bind<AState<SpawnersController, Event>>("Off", new OffBuilder());
        }

        public T CreateState<T>(string name)
        {
            return m_stateFactory.CreateState<T>(name);
        }
    }
}
