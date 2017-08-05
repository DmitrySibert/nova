using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    private EventBus eventBus;

	private void Start ()
    {
        eventBus = FindObjectOfType<EventBus>();
    }
	
	public void OnStartButton()
    {
        eventBus.TriggerEvent(new Event("StartButtonClicked"));
    }

    public void OnHelpButton()
    {
        eventBus.TriggerEvent(new Event("HelpButtonClicked"));
    }

    public void OnExitButton()
    {
        eventBus.TriggerEvent(new Event("ExitButtonClicked"));
    }
}
