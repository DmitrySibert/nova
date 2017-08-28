using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.Game.GamePlay.Score;

public class GameState : MonoBehaviour {

    private Dispatcher dispatcher;
    private EventBus eventBus;
    private IEnumerator playerTurn;
    private bool isPlayerTurnTimerOn;

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

	private void Start ()
    {
        dispatcher = gameObject.GetComponent<Dispatcher>();
        eventHandlers = new Dictionary<string, EventHandler>();
        InitEventHandlers();
        eventBus = FindObjectOfType<EventBus>();
        playerTurn = PlayerTurn(1f);
        isPlayerTurnTimerOn = false;
        eventBus.TriggerEvent(new Event("PlayerTurn"));
        curScores = 0;
        SendScoresUpdatedMsg();
        scoreCalculator.Start();
    }

    private void InitEventHandlers()
    {
        eventHandlers["NullEvent"] = (data) => { };
        eventHandlers["LeftMouseUp"] = OnLeftMouseUp;
        eventHandlers["SupernovaBirth"] = SupernovaBirthHandler;
        eventHandlers["SupernovaDeath"] = SupernovaDeathHandler;
        eventHandlers["BlackholeDeath"] = BlackholeDeathHandler;
        eventHandlers["PulsarBirth"] = PulsarBirthHandler;
        eventHandlers["DeathBarrierTouch"] = OnDeathBarrierTouch;
    }
	
	private void Update ()
    {
        Event evt = dispatcher.ReceiveEvent();
        eventHandlers[evt.Name].Invoke(evt.Data);
    }

    private void SupernovaBirthHandler(Data data)
    {
        StartPlayerTurnTimer();
    }

    private void SupernovaDeathHandler(Data data)
    {
        scoreCalculator.AddScores(singleScoreVal);
        if (isPlayerTurnTimerOn) {
            StartPlayerTurnTimer();
        }
    }

    private void BlackholeDeathHandler(Data data)
    {
        StartPlayerTurnTimer();
    }

    private void PulsarBirthHandler(Data data)
    {
        StartPlayerTurnTimer();
    }

    private void OnLeftMouseUp(Data data)
    {
        eventBus.TriggerEvent(new Event("PlayerTurnEnd"));
        eventHandlers["LeftMouseUp"] = (dataParam) => { };
    }

    private void StartPlayerTurnTimer()
    {
        isPlayerTurnTimerOn = true;
        StopCoroutine(playerTurn);
        playerTurn = PlayerTurn(2f);
        StartCoroutine(playerTurn);
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

    private IEnumerator PlayerTurn(float intervalSec)
    {
        yield return new WaitForSeconds(intervalSec);
        eventBus.TriggerEvent(new Event("PlayerTurn"));
        eventHandlers["LeftMouseUp"] = OnLeftMouseUp;
        curScores += scoreCalculator.End();
        SendScoresUpdatedMsg();
        scoreCalculator.Start();
        isPlayerTurnTimerOn = false;
    }
}
