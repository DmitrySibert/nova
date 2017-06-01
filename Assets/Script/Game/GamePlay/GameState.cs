using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {

    private Dispatcher dispatcher;
    private EventBus eventBus;
    private IEnumerator playerTurn;

	// Use this for initialization
	void Start ()
    {
        Debug.Log("GameState initialized");
        dispatcher = gameObject.GetComponent<Dispatcher>();
        eventBus = FindObjectOfType<EventBus>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Event evt = dispatcher.ReceiveEvent();
        if (evt != null)
        {
            if (evt.Name.Equals("SupernovaBirth"))
            {
                playerTurn = PlayerTurn(1f);
                StartCoroutine(playerTurn);
            }
        }
    }

    private IEnumerator PlayerTurn(float intervalSec)
    {
        yield return new WaitForSeconds(intervalSec);
        eventBus.TriggerEvent(new Event("PlayerTurn"));
    }
}
