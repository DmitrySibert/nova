using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierController : MonoBehaviour {

    [SerializeField]
    private Barrier[] barriers;

    private Dispatcher dispatcher;

	private void Start ()
    {
        dispatcher = GetComponent<Dispatcher>();
	}
	
	private void Update ()
    {
        Event evt = dispatcher.ReceiveEvent();
        if (evt.Name.Equals("SpawnerChoosen")) {
            foreach (Barrier barrier in barriers) {
                barrier.IsDeathTouchEnable = false;
            }
            int prevBarrierIdx = evt.Data.Get<int>("PrevSpawnerNumber");
            int curBarrierIdx = evt.Data.Get<int>("CurrentSpawnerNumber");
            barriers[prevBarrierIdx].IsDeathTouchEnable = true;
            barriers[curBarrierIdx].IsDeathTouchEnable = true;
        }

	}
}
