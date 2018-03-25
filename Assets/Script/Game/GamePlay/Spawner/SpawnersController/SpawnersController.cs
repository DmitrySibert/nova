using System.Collections.Generic;
using UnityEngine;
using Core.StateMachine;
using Assets.Script.Core;

namespace GamePlay.Spawner.SpawnerController
{
    public class SpawnersController : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] spawners;

        public StateFactoryMono m_stateFactory;

        private Dispatcher dispatcher;
        delegate void EventHandler();
        private Dictionary<string, EventHandler> eventHandlers;

        private EventBasedCSM<SpawnersController> stateMachine;

        private int curActiveSpawnerIdx;
        private int lastActiveSpawnerIdx;

        void Start()
        {
            dispatcher = GetComponent<Dispatcher>();
            for (int i = 0; i < spawners.Length; ++i) {
                spawners[i].SetActive(false);
            }
            curActiveSpawnerIdx = 0;
            spawners[curActiveSpawnerIdx].SetActive(true);

            var stateActive = m_stateFactory.CreateState<AState<SpawnersController, Event>>("Active");
            var stateChoice = m_stateFactory.CreateState<AState<SpawnersController, Event>>("Choice");
            var stateOff = m_stateFactory.CreateState<AState<SpawnersController, Event>>("Off");

            stateMachine = new EventBasedCSM<SpawnersController>(this, stateOff);
            stateMachine.AddTransition(stateOff, "PlayerTurn".GetHashCode(), stateActive);
            stateMachine.AddTransition(stateActive, "SpaceUp".GetHashCode(), stateChoice);
            stateMachine.AddTransition(stateChoice, "SpaceUp".GetHashCode(), stateOff);
            stateMachine.AddTransition(stateChoice, "PlayerTurnEnd".GetHashCode(), stateOff);
            stateMachine.AddTransition(stateActive, "PlayerTurnEnd".GetHashCode(), stateOff);
        }

        private void Update()
        {
            if (!dispatcher.IsEmpty()) {
                Event evt = dispatcher.ReceiveEvent();
                stateMachine.UpdateCurrentState(evt);
                stateMachine.ApplyTrigger(evt.Name.GetHashCode());
            }
        }

        public void StartChoosingSpawner()
        {
            FindObjectOfType<EventBus>().TriggerEvent(new Event("StartSpawnerChoice"));
            lastActiveSpawnerIdx = curActiveSpawnerIdx;
        }

        public void SetNextSpawner()
        {
            spawners[curActiveSpawnerIdx].SetActive(false);
            if (curActiveSpawnerIdx < spawners.Length - 1) {
                ++curActiveSpawnerIdx;
            } else {
                curActiveSpawnerIdx = 0;
            }
            spawners[curActiveSpawnerIdx].SetActive(true);
        }

        public void SetPrevSpawner()
        {
            spawners[curActiveSpawnerIdx].SetActive(false);
            if (curActiveSpawnerIdx > 0) {
                --curActiveSpawnerIdx;
            } else {
                curActiveSpawnerIdx = spawners.Length - 1;
            }
            spawners[curActiveSpawnerIdx].SetActive(true);
        }

        public void CommitSpawnerPosition()
        {
            Data data = new Data();
            data["PrevSpawnerNumber"] = lastActiveSpawnerIdx;
            data["CurrentSpawnerNumber"] = curActiveSpawnerIdx;
            FindObjectOfType<EventBus>().TriggerEvent(new Event("SpawnerChoosen", data));
        }
    }
}
