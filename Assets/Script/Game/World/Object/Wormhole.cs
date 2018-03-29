using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wormhole : MonoBehaviour {

    [SerializeField]
    private GameObject m_spawnObject;
    [SerializeField]
    private Transform m_spawnPoint;
    [SerializeField]
    private float m_spawnPower;

    private Dispatcher m_dispatcher;
    delegate void EventHandler(Data data);
    private Dictionary<string, EventHandler> m_eventHandlers;

    private Collider2D m_collider;
    private float m_timeCount;
    private bool m_isActive;
    private bool m_isPaused;

    // Use this for initialization
    void Start()
    {
        m_dispatcher = gameObject.GetComponent<Dispatcher>();
        m_eventHandlers = new Dictionary<string, EventHandler>();
        m_isActive = true;
        m_isPaused = false;
        InitEventHandlers();
    }

    private void InitEventHandlers()
    {
        m_eventHandlers["LeftMouseUp"] = OnLeftMouseUp;
        m_eventHandlers["PlayerTurn"] = OnPlayerTurn;
        m_eventHandlers["Pause"] = OnPause;
        m_eventHandlers["Unpause"] = OnUnpause;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!m_dispatcher.IsEmpty())
        {
            Event evt = m_dispatcher.ReceiveEvent();
            if (m_isPaused && !evt.Name.Equals("Unpause"))
            {
                return;
            }
            m_eventHandlers[evt.Name].Invoke(evt.Data);
        }
    }

    public void SpawnComet(Vector2 dirPoint)
    {
        FindObjectOfType<EventBus>().TriggerEvent(new Event("CometLaunch"));
        Vector2 spawnPoint = m_spawnPoint.position;
        GameObject comet = Instantiate<GameObject>(m_spawnObject, spawnPoint, Quaternion.identity);
        Rigidbody2D cometRb = comet.GetComponent<Rigidbody2D>();
        Vector2 force = m_spawnPower * (dirPoint - spawnPoint).normalized;
        cometRb.AddForce(force, ForceMode2D.Impulse);
    }

    private void OnLeftMouseUp(Data data)
    {
        if (!m_isActive)
        {
            return;
        }
        m_isActive = false;
        Vector2 v2 = data.Get<Vector3>("clickPoint");
        SpawnComet(v2);
    }

    private void OnPlayerTurn(Data data)
    {
        m_isActive = true;
    }

    private void OnPause(Data data)
    {
        m_isPaused = true;
    }

    private void OnUnpause(Data data)
    {
        m_isPaused = false;
    }
}
