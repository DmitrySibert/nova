using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour {

    [SerializeField]
    private Canvas m_gameMenuCanvas;

    private EventBus m_eventBus;
    private Dispatcher m_dispatcher;

    private void Start()
    {
        m_eventBus = FindObjectOfType<EventBus>();
        m_dispatcher = GetComponent<Dispatcher>();
    }

    private void Update()
    {
        if(!m_dispatcher.IsEmpty())
        {
            Event evt = m_dispatcher.ReceiveEvent();
            if(evt.Name.Equals("Pause"))
            {
                OnPause();
            }
            if(evt.Name.Equals("Unpause"))
            {
                OnUnpause();
            }
        }
    }

    private void OnPause()
    {
        m_gameMenuCanvas.enabled = true;
    }

    private void OnUnpause()
    {
        m_gameMenuCanvas.enabled = false;
    }

    public void OnRestartButton()
    {
        m_eventBus.TriggerEvent(new Event("RestartButtonClicked"));
    }

    public void OnExitMenuButton()
    {
        m_eventBus.TriggerEvent(new Event("ExitMenuClicked"));
    }
}
