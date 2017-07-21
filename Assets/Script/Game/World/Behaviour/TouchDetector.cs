using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDetector : MonoBehaviour {

    [SerializeField]
    private GameObject[] objectsDetecting;
    [SerializeField]
    private string eventName;

    private void OnCollisionEnter2D(Collision2D col)
    {
        foreach(GameObject detectingGo in objectsDetecting) {
            if (detectingGo.name.Equals(col.gameObject.name)) {
                FindObjectOfType<EventBus>().TriggerEvent(new Event(eventName));
                break;
            }
        }
    }
}
