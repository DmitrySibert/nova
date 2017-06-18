using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay
{
    using RightExclusiveRange = List<float>;

    public class NovaSpawner : MonoBehaviour {

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
        }

        private void Spawn(Vector3 position, GameObject go)
        {
            Instantiate<GameObject>(go, position, Quaternion.identity);
        }
    }
}
