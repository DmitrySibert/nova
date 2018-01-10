using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.Game.GamePlay.Score;
using Assets.Script.Game.GamePlay;

public class GameState : MonoBehaviour {

    private Dispatcher dispatcher;
    private EventBus eventBus;

    delegate void EventHandler(Data data);
    private Dictionary<string, EventHandler> eventHandlers;

    private IScoreCalculator scoreCalculator;
    public IScoreCalculator ScoreCalculator
    {
        set { scoreCalculator = value; }
    }
    private float curScores;
    [SerializeField]
    private float singleScoreVal;

    [SerializeField]
    private TurnEventChecker turnEventChecker;

	private void Start ()
    {
        dispatcher = gameObject.GetComponent<Dispatcher>();
        eventHandlers = new Dictionary<string, EventHandler>();
        InitEventHandlers();
        eventBus = FindObjectOfType<EventBus>();
        eventBus.TriggerEvent(new Event("PlayerTurn"));
        curScores = 0;
        SendScoresUpdatedMsg();
        scoreCalculator.Start();
        turnEventChecker = Instantiate<TurnEventChecker>(turnEventChecker);
    }


    private void InitEventHandlers()
    {
        eventHandlers["SupernovaDeath"] = OnSupernovaDeath;
        eventHandlers["LeftMouseUp"] = OnLeftMouseUp;
        eventHandlers["DeathBarrierTouch"] = OnDeathBarrierTouch;
        eventHandlers["EventCheckerEmpty"] = OnEventCheckerEmpty;
    }
	
	private void Update ()
    {
        if (!dispatcher.IsEmpty()) {
            Event evt = dispatcher.ReceiveEvent();
            eventHandlers[evt.Name].Invoke(evt.Data);
        }
    }

    private void OnLeftMouseUp(Data data)
    {
        eventBus.TriggerEvent(new Event("PlayerTurnEnd"));
        eventHandlers["LeftMouseUp"] = (dataParam) => { };
    }

    private void OnSupernovaDeath(Data data)
    {
        scoreCalculator.AddScores(singleScoreVal);
    }

    private void OnDeathBarrierTouch(Data data)
    {
        Data gameOverData = new Data();
        gameOverData["Scores"] = curScores;
        eventBus.TriggerEvent(new Event("GameOver", gameOverData));
    }

    private void SendScoresUpdatedMsg()
    {
        Data data = new Data();
        data["Scores"] = curScores;
        eventBus.TriggerEvent(new Event("ScoresUpdated", data));
    }

    private void PlayerTurn()
    {
        eventBus.TriggerEvent(new Event("PlayerTurn"));
        eventHandlers["LeftMouseUp"] = OnLeftMouseUp;
        curScores += scoreCalculator.End();
        SendScoresUpdatedMsg();
        scoreCalculator.Start();
    }

    private void OnEventCheckerEmpty(Data data)
    {
        PlayerTurn();
    }
}
