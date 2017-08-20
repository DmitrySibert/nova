using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierController : MonoBehaviour {

    [SerializeField]
    private Barrier[] barriers;

    private Dispatcher dispatcher;
    private int prevBarrierIdx;
    private int turnsCount;
    private bool isWaitForDisablePrevBarrier;

	private void Start ()
    {
        dispatcher = GetComponent<Dispatcher>();
        isWaitForDisablePrevBarrier = false;
    }
	
	private void Update ()
    {
        Event evt = dispatcher.ReceiveEvent();
        if (evt.Name.Equals("SpawnerChoosen")) {
            foreach (Barrier barrier in barriers) {
                barrier.IsDeathTouchEnable = false;
            }
            prevBarrierIdx = evt.Data.Get<int>("PrevSpawnerNumber");
            int curBarrierIdx = evt.Data.Get<int>("CurrentSpawnerNumber");
            barriers[prevBarrierIdx].IsDeathTouchEnable = true;
            barriers[curBarrierIdx].IsDeathTouchEnable = true;
            turnsCount = 2;
            isWaitForDisablePrevBarrier = true;
        } else if (evt.Name.Equals("PlayerTurn")) {
            if (isWaitForDisablePrevBarrier) {
                DisablePrevBarrierDeathTouch();
            }
        }

	}

    private void DisablePrevBarrierDeathTouch()
    {
        --turnsCount;
        if (turnsCount == 0) {
            isWaitForDisablePrevBarrier = false;
            barriers[prevBarrierIdx].IsDeathTouchEnable = false;
        }
    }
}
