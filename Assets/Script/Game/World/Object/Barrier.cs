using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Barrier : MonoBehaviour {

    [SerializeField]
    private GameObject objectForDeathReaction;
    [SerializeField]
    private bool isDeathTouchEnable;
    [SerializeField]
    private Renderer render;
    [SerializeField]
    private Color commonColor;
    [SerializeField]
    private Color deathTouchColor;

    public bool IsDeathTouchEnable
    {
        set {
            isDeathTouchEnable = value;
            if (isDeathTouchEnable) {
                render.material.color = deathTouchColor;
            } else {
                render.material.color = commonColor;
            }
        }
        get { return isDeathTouchEnable; }
    }

    private EventBus eventBus;
    private Dispatcher dispatcher;
    delegate void EventHandler();
    private Dictionary<string, EventHandler> eventHandlers;

    private Collider2D col2D;

    private void Start ()
    {
        eventBus = FindObjectOfType<EventBus>();
        col2D = GetComponent<Collider2D>();
        dispatcher = GetComponent<Dispatcher>();
        if (isDeathTouchEnable) {
            render.material.color = deathTouchColor;
        } else {
            render.material.color = commonColor;
        }
        InitEventHandlers();
    }

    private void InitEventHandlers()
    {
        eventHandlers = new Dictionary<string, EventHandler>();
        eventHandlers["NullEvent"] = () => { };
        eventHandlers["WentThrowBarrier"] = OnWentThrowBarrier;
        eventHandlers["PlayerTurn"] = OnPlayerTurn;
    }
	
	private void Update ()
    {
        Event evt = dispatcher.ReceiveEvent();
        eventHandlers[evt.Name].Invoke();
	}

    private void OnTriggerExit2D(Collider2D collision)
    {
        eventBus.TriggerEvent(new Event("WentThrowBarrier"));
    }

    private void OnWentThrowBarrier()
    {
        col2D.isTrigger = false;
    }

    private void OnPlayerTurn()
    {
        col2D.isTrigger = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDeathTouchEnable) {
            if (CompareUtils.IsEquals(collision.gameObject, objectForDeathReaction)) {
                eventBus.TriggerEvent(new Event("DeathBarrierTouch"));
            }
        }
    }
}
