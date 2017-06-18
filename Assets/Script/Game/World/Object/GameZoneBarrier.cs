using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameZoneBarrier : MonoBehaviour {

    private Dispatcher dispatcher;
    private Collider2D col;

	void Start ()
    {
        dispatcher = gameObject.GetComponent<Dispatcher>();
        col = gameObject.GetComponent<PolygonCollider2D>();
    }
	
	void Update ()
    {
        Event evt = dispatcher.ReceiveEvent();
        if (evt.Name.Equals("PlayerTurn"))
        {
            col.isTrigger = true;
        }
	}

    void OnTriggerExit2D(Collider2D collider)
    {
        col.isTrigger = false;
    }
}
