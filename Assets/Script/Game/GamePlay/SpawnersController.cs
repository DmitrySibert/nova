using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay
{
    public class SpawnersController : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] spawners;

        private Dispatcher dispatcher;
        delegate void EventHandler();
        private Dictionary<string, EventHandler> eventHandlers;

        private int curActiveSpawnerIdx;

        void Start()
        {
            dispatcher = GetComponent<Dispatcher>();
            eventHandlers = new Dictionary<string, EventHandler>();
            InitEventHandlers();

            for (int i = 0; i < spawners.Length; ++i) {
                spawners[i].SetActive(false);
            }
            curActiveSpawnerIdx = 0;
            spawners[curActiveSpawnerIdx].SetActive(true);
        }

        private void InitEventHandlers()
        {
            eventHandlers["NullEvent"] = () => { };
            eventHandlers["Space"] = ActivateSpawnerChoice;
        }

        void Update()
        {
            Event evt = dispatcher.ReceiveEvent();
            eventHandlers[evt.Name].Invoke();
        }

        void ActivateSpawnerChoice()
        {
            eventHandlers["Space"] = StartChoosingSpawner;
        }

        void StartChoosingSpawner()
        {
            eventHandlers["RightArrowUp"] = SetNextSpawner;
            eventHandlers["LeftArrowUp"] = SetPrevSpawner;
            eventHandlers["Space"] = EndChoosingSpawner;
        }

        void SetNextSpawner()
        {
            spawners[curActiveSpawnerIdx].SetActive(false);
            if (curActiveSpawnerIdx < spawners.Length - 1) {
                ++curActiveSpawnerIdx;
            } else {
                curActiveSpawnerIdx = 0;
            }
            spawners[curActiveSpawnerIdx].SetActive(true);
        }

        void SetPrevSpawner()
        {
            spawners[curActiveSpawnerIdx].SetActive(false);
            if (curActiveSpawnerIdx > 0) {
                --curActiveSpawnerIdx;
            } else {
                curActiveSpawnerIdx = spawners.Length - 1;
            }
            spawners[curActiveSpawnerIdx].SetActive(true);
        }

        void EndChoosingSpawner()
        {
            eventHandlers["Space"] = StartChoosingSpawner;
            eventHandlers.Remove("RightArrowUp");
            eventHandlers.Remove("LeftArrowUp");
        }
    }
}
