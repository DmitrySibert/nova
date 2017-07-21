using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierController : MonoBehaviour {

    [SerializeField]
    private GameObject[] barriers;

    private Dispatcher dispatcher;

	private void Start ()
    {
        dispatcher = GetComponent<Dispatcher>();
	}
	
	private void Update ()
    {
        Event evt = dispatcher.ReceiveEvent();
        if (evt.Name.Equals("SpawnerChoosen")) {
            foreach (GameObject barrier in barriers) {
            }
            int prevBarrierIdx = evt.Data.Get<int>("PrevSpawnerNumber");
            int curBarrierIdx = evt.Data.Get<int>("CurrentSpawnerNumber");
            barriers[prevBarrierIdx].SetActive(true);
            barriers[curBarrierIdx].SetActive(true);
        }

	}
}
