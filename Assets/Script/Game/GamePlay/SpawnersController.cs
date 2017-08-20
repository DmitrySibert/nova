﻿using System.Collections;
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
        private int lastActiveSpawnerIdx;

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
            eventHandlers["PlayerTurn"] = StartPlayerTurn;
            eventHandlers["PlayerTurnEnd"] = OnPlayerTurnEnd;
            eventHandlers["LeftMouseUp"] = () => { };
            eventHandlers["RightArrowUp"] = () => { };
            eventHandlers["LeftArrowUp"] = () => { };
        }

        private void Update()
        {
            Event evt = dispatcher.ReceiveEvent();
            eventHandlers[evt.Name].Invoke();
        }

        private void StartChoosingSpawner()
        {
            FindObjectOfType<EventBus>().TriggerEvent(new Event("StartSpawnerChoice"));
            lastActiveSpawnerIdx = curActiveSpawnerIdx;
            eventHandlers["RightArrowUp"] = SetNextSpawner;
            eventHandlers["LeftArrowUp"] = SetPrevSpawner;
            eventHandlers["SpaceUp"] = EndChoosingSpawner;
            eventHandlers["PlayerTurnEnd"] = EndChoosingSpawner;
        }

        private void SetNextSpawner()
        {
            spawners[curActiveSpawnerIdx].SetActive(false);
            if (curActiveSpawnerIdx < spawners.Length - 1) {
                ++curActiveSpawnerIdx;
            } else {
                curActiveSpawnerIdx = 0;
            }
            spawners[curActiveSpawnerIdx].SetActive(true);
        }

        private void SetPrevSpawner()
        {
            spawners[curActiveSpawnerIdx].SetActive(false);
            if (curActiveSpawnerIdx > 0) {
                --curActiveSpawnerIdx;
            } else {
                curActiveSpawnerIdx = spawners.Length - 1;
            }
            spawners[curActiveSpawnerIdx].SetActive(true);
        }

        private void EndChoosingSpawner()
        {
            eventHandlers["SpaceUp"] = () => { };
            eventHandlers["RightArrowUp"] = () => { };
            eventHandlers["LeftArrowUp"] = () => { };
            eventHandlers["PlayerTurn"] = StartPlayerTurn;
            eventHandlers["PlayerTurnEnd"] = OnPlayerTurnEnd;
            Data data = new Data();
            data["PrevSpawnerNumber"] = lastActiveSpawnerIdx;
            data["CurrentSpawnerNumber"] = curActiveSpawnerIdx;
            FindObjectOfType<EventBus>().TriggerEvent(new Event("SpawnerChoosen", data));
        }

        private void StartPlayerTurn()
        {
            eventHandlers["SpaceUp"] = StartChoosingSpawner;
        }

        private void OnPlayerTurnEnd()
        {
            eventHandlers["SpaceUp"] = () => { };
        }
    }
}
