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
        private Dictionary<RightExclusiveRange, GameObject> objectsVarianceRange;
        private GameObject nextSpawn;

        private void Start()
        {
            dispatcher = gameObject.GetComponent<Dispatcher>();
            objectsVarianceRange = new Dictionary<List<float>, GameObject>();
            float rangeLeftTangent = 0;
            for (uint i = 0; i < objectsSpawnVariance.Length; ++i) {
                RightExclusiveRange range = new RightExclusiveRange(2);
                range.Add(rangeLeftTangent);
                range.Add(rangeLeftTangent + objectsSpawnVariance[i]);
                objectsVarianceRange[range] = objectsForSpawn[i];
                rangeLeftTangent += objectsSpawnVariance[i];
            }
            nextSpawn = GetNextSpawn();
        }

        private GameObject GetNextSpawn()
        {
            GameObject nextSpawn = objectsForSpawn[objectsForSpawn.Length - 1];
            float randVal = Random.Range(0f, 1f);
            foreach(RightExclusiveRange range in objectsVarianceRange.Keys) {
                if (randVal >= range[0] && randVal < range[1]) {
                    nextSpawn = objectsVarianceRange[range];
                } 
            }
            Data data = new Data();
            data["SpawnGameObject"] = nextSpawn;
            FindObjectOfType<EventBus>().TriggerEvent(new Event("NextSpawn", data));

            return nextSpawn;
        }

        void Update()
        {
            Event evt = dispatcher.ReceiveEvent();
            if (evt.Name.Equals("CometDeath")) {
                Spawn(evt.Data.Get<Vector3>("position"), nextSpawn);
                nextSpawn = GetNextSpawn();
            }
            if (evt.Name.Equals("RightMouseUp")) {
                Vector3 vec = new Vector3(evt.Data.Get<Vector3>("clickPoint").x, evt.Data.Get<Vector3>("clickPoint").y, 0);
                GameObject burstWave = Spawn(vec, singleBurstWave);
                LimitedSizeExtend extend = burstWave.gameObject.AddComponent<LimitedSizeExtend>();
                extend.limit = new Vector3(20,20,20);
                extend.extendFreqSec = 2;
            }
        }

        private GameObject Spawn(Vector3 position, GameObject go)
        {
            return Instantiate<GameObject>(go, position, Quaternion.identity);
        }
    }
}
