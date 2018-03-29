using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PcController : MonoBehaviour {

    public GameState gameState;
    private EventBus m_eventBus;

	void Start ()
    {
        Debug.Log("PcController initialized");
        m_eventBus = FindObjectOfType<EventBus>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Data data = new Data();
            data["clickPoint"] = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_eventBus.TriggerEvent(new Event("LeftMouseUp", data));
        }
        if (Input.GetMouseButtonUp(1))
        {
            Data data = new Data();
            data["clickPoint"] = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_eventBus.TriggerEvent(new Event("RightMouseUp", data));
        }
        if (Input.GetKeyUp("space"))
        {
            m_eventBus.TriggerEvent(new Event("SpaceUp"));
        }
        if (Input.GetKeyUp("left"))
        {
            m_eventBus.TriggerEvent(new Event("LeftArrowUp"));
        }
        if (Input.GetKeyUp("right"))
        {
            m_eventBus.TriggerEvent(new Event("RightArrowUp"));
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            m_eventBus.TriggerEvent(new Event("EscapeUp"));
        }
    }
}
