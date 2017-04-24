using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PcController : MonoBehaviour {

    public GameState gameState;
    private Dispatcher m_dispatcher;

	void Start ()
    {
        Debug.Log("PcController initialized");
        m_dispatcher = gameObject.GetComponent<Dispatcher>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonUp(0))
        {
            m_dispatcher.TriggerEvent("LeftMouseUp");
        }
        if (Input.GetMouseButtonUp(1))
        {
            m_dispatcher.TriggerEvent("RightMouseUp");
        }
    }
}
