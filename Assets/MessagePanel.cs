using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : MonoBehaviour {

    [SerializeField]
    private Text messageText;
    [SerializeField]
    private Image messageBackground;
    [SerializeField]
    private float textAlphaTransSec;
    private IEnumerator textAlphaTransCoroutine;

    private Dispatcher dispatcher;
    delegate void EventHandler(Data data);
    private Dictionary<string, EventHandler> eventHandlers;

    private void Start ()
    {
        dispatcher = GetComponent<Dispatcher>();
        InitEventHandlers();
    }

    private void InitEventHandlers()
    {
        eventHandlers = new Dictionary<string, EventHandler>();

        eventHandlers["PlayerTurn"] = (data) => {
            SetText("Player turn");
            ShowText(0f);
            ShadeText(textAlphaTransSec + 0.5f);
        };
        eventHandlers["StartSpawnerChoice"] = (data) => {
            SetText("Choose spawner position");
            ShowText(0f);
        };
        eventHandlers["SpawnerChoosen"] = (data) => {
            ShadeText(0f);
        };
        eventHandlers["GameOver"] = (data) => { SetText("Game over"); };
        eventHandlers["NullEvent"] = (data) => { };
    }

    private void SetText(string text)
    {
        messageText.text = text;
    }

    private void ShowText(float delaySec)
    {
        textAlphaTransCoroutine = MakeTextApparent(messageText, textAlphaTransSec, delaySec);
        StartCoroutine(textAlphaTransCoroutine);
    }

    private void ShadeText(float delaySec)
    {
        textAlphaTransCoroutine = MakeTextTransparent(messageText, textAlphaTransSec, delaySec);
        StartCoroutine(textAlphaTransCoroutine);
    }

    private IEnumerator MakeTextTransparent(Text text, float timeSec, float delaySec)
    {
        if (delaySec > 0f) {
            yield return new WaitForSeconds(delaySec);
        }

        float valPerSec = 1f / timeSec;
        while(text.color.a > 0) {
            float newAlpha = text.color.a - valPerSec * Time.deltaTime;
            if (newAlpha < 0) {
                newAlpha = 0f;
            }    
            Color newColor = new Color(text.color.r, text.color.g, text.color.b, newAlpha);
            text.color = newColor;
            messageBackground.color = newColor;
            yield return null;
        }
    }

    private IEnumerator MakeTextApparent(Text text, float timeSec, float delaySec)
    {
        if (delaySec > 0f) {
            yield return new WaitForSeconds(delaySec);
        }

        float valPerSec = 1f / timeSec;
        while (text.color.a < 1) {
            float newAlpha = text.color.a + valPerSec * Time.deltaTime;
            if (newAlpha > 1f) {
                newAlpha = 1f;
            }
            Color newColor = new Color(text.color.r, text.color.g, text.color.b, newAlpha);
            text.color = newColor;
            messageBackground.color = newColor;
            yield return null;
        }
    }

    private void Update ()
    {
        Event evt = dispatcher.ReceiveEvent();
        eventHandlers[evt.Name].Invoke(evt.Data);
	}
}
