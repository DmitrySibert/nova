using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PcController : MonoBehaviour {

    public GameState gameState;
    private EventBus eventBus;

	void Start ()
    {
        Debug.Log("PcController initialized");
        eventBus = FindObjectOfType<EventBus>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonUp(0)) {
            Data data = new Data();
            data["clickPoint"] = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            eventBus.TriggerEvent(new Event("LeftMouseUp", data));
        }
        if (Input.GetMouseButtonUp(1)) {
            Data data = new Data();
            data["clickPoint"] = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            eventBus.TriggerEvent(new Event("RightMouseUp", data));
        }
        if (Input.GetKeyUp("space")) {
            eventBus.TriggerEvent(new Event("SpaceUp"));
        }
        if (Input.GetKeyUp("left")) {
            eventBus.TriggerEvent(new Event("LeftArrowUp"));
        }
        if (Input.GetKeyUp("right")) {
            eventBus.TriggerEvent(new Event("RightArrowUp"));
        }
    }
}
