using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Spawner
{
    using RightExclusiveRange = List<float>;

    public class NovaSpawner : MonoBehaviour {

        public GameObject singleBurstWave;
        [SerializeField]
        private GameObject[] objectsForSpawn;
        [SerializeField]
        private float[] objectsSpawnVariance;

        private Dispatcher dispatcher;
        delegate void EventHandler(Data data);
        private Dictionary<string, EventHandler> eventHandlers;

        private Dictionary<RightExclusiveRange, GameObject> objectsVarianceRange;
        private GameObject nextSpawn;

        private void Start()
        {
            dispatcher = gameObject.GetComponent<Dispatcher>();
            eventHandlers = new Dictionary<string, EventHandler>();
            objectsVarianceRange = new Dictionary<List<float>, GameObject>();
            float rangeLeftTangent = 0;
            for (uint i = 0; i < objectsSpawnVariance.Length; ++i)
            {
                RightExclusiveRange range = new RightExclusiveRange(2);
                range.Add(rangeLeftTangent);
                range.Add(rangeLeftTangent + objectsSpawnVariance[i]);
                objectsVarianceRange[range] = objectsForSpawn[i];
                rangeLeftTangent += objectsSpawnVariance[i];
            }
            nextSpawn = GetNextSpawn();
            InitEventHandlers();
        }

        private void InitEventHandlers()
        {
            eventHandlers["CometDeath"] = OnCometDeath;
        }

        void Update()
        {
            if (!dispatcher.IsEmpty())
            {
                Event evt = dispatcher.ReceiveEvent();
                eventHandlers[evt.Name].Invoke(evt.Data);
            }
        }

        private GameObject GetNextSpawn()
        {
            GameObject nextSpawn = objectsForSpawn[objectsForSpawn.Length - 1];
            float randVal = Random.Range(0f, 1f);
            foreach(RightExclusiveRange range in objectsVarianceRange.Keys)
            {
                if (randVal >= range[0] && randVal < range[1])
                {
                    nextSpawn = objectsVarianceRange[range];
                } 
            }
            Data data = new Data();
            data["SpawnGameObject"] = nextSpawn;
            FindObjectOfType<EventBus>().TriggerEvent(new Event("NextSpawn", data));

            return nextSpawn;
        }

        private GameObject Spawn(Vector3 position, GameObject go)
        {
            return Instantiate<GameObject>(go, position, Quaternion.identity);
        }

        private void OnCometDeath(Data data)
        {
            Spawn(data.Get<Vector3>("position"), nextSpawn);
            nextSpawn = GetNextSpawn();
        }
    }
}
