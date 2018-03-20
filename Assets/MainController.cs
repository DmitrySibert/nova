using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour {

    public static MainController instance = null;

    private Dispatcher dispatcher;
    delegate void EventHandler();
    private Dictionary<string, EventHandler> eventHandlers;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(this.gameObject);
        }
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    private void Start ()
    {
        dispatcher = GetComponent<Dispatcher>();
        InitEventHandlers();
        SceneManager.LoadScene("MainMenu");
    }

    private void InitEventHandlers()
    {
        eventHandlers = new Dictionary<string, EventHandler>();
        eventHandlers["StartButtonClicked"] = OnStartButtonClicked;
        eventHandlers["HelpButtonClicked"] = OnHelpButtonClicked;
        eventHandlers["ExitButtonClicked"] = OnExitButtonClicked;
        eventHandlers["GameOver"] = OnGameOver;
        eventHandlers["LeftMouseUp"] = () => { };
    }


    private void Update ()
    {
        if (!dispatcher.IsEmpty()) {
            Event evt = dispatcher.ReceiveEvent();
            eventHandlers[evt.Name].Invoke();
        }
	}

    private void OnStartButtonClicked()
    {
        SceneManager.LoadScene("Main");
    }

    private void OnHelpButtonClicked()
    {

    }

    private void OnExitButtonClicked()
    {
        Application.Quit();
    }

    private void OnGameOver()
    {
        eventHandlers["LeftMouseUp"] = LoadMainMenu;
    }

    private void LoadMainMenu()
    {
        eventHandlers["LeftMouseUp"] = () => { };
        SceneManager.LoadScene("MainMenu");
    }
}
