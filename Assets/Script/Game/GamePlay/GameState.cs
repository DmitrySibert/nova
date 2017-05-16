using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {

    [SerializeField]
    private SuperNova superNova;

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

    public void SpawnSuperNova(Vector2 pos)
    {
        superNova.size = Random.Range(1, 5) * 10;
        Instantiate<SuperNova>(superNova, pos, Quaternion.identity);
    }
}
