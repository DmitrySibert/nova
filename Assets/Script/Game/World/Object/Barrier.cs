using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Barrier : MonoBehaviour {

    [SerializeField]
    private GameObject objectForDeathReaction;
    [SerializeField]
    private bool isDeathTouchEnable;
    [SerializeField]
    private Renderer renderer;
    [SerializeField]
    private Color commonColor;
    [SerializeField]
    private Color deathTouchColor;

    public bool IsDeathTouchEnable
    {
        set {
            isDeathTouchEnable = value;
            if (isDeathTouchEnable) {
                renderer.material.color = deathTouchColor;
            } else {
                renderer.material.color = commonColor;
            }
        }
        get { return isDeathTouchEnable; }
    }

    private EventBus eventBus;
    private Dispatcher dispatcher;
    delegate void EventHandler();
    private Dictionary<string, EventHandler> eventHandlers;

    private void Start ()
    {
        eventBus = FindObjectOfType<EventBus>();
        dispatcher = GetComponent<Dispatcher>();
        if (isDeathTouchEnable) {
            renderer.material.color = deathTouchColor;
        } else {
            renderer.material.color = commonColor;
        }
        InitEventHandlers();
    }

    private void InitEventHandlers()
    {
        eventHandlers = new Dictionary<string, EventHandler>();
        eventHandlers["NullEvent"] = () => { };
        eventHandlers["WentThrowBarrier"] = OnWentThrowBarrier;
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
        Collider2D collider = GetComponent<Collider2D>();
        collider.isTrigger = false;
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
