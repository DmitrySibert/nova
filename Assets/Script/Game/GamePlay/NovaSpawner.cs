using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NovaSpawner : MonoBehaviour {

    [SerializeField]
    private SuperNova superNova;
    private Dispatcher dispatcher;

	void Start ()
    {
        dispatcher = gameObject.GetComponent<Dispatcher>();
    }
	
	void Update ()
    {
        Event evt = dispatcher.ReceiveEvent();
        if (evt != null) {
            if (evt.Name.Equals("CometDeath")) {
                SpawnNova(evt.Data.Get<Vector3>("position"));
            }
        }
	}

    private void SpawnNova(Vector3 position)
    {
        SuperNova go = Instantiate<SuperNova>(superNova, position, Quaternion.identity);
    }
}
