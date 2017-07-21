using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameZoneBarrier : MonoBehaviour {

    void OnTriggerExit2D(Collider2D collider)
    {
        Data data = new Data();
        data["OutGameObject"] = collider.gameObject;
        FindObjectOfType<EventBus>().TriggerEvent(new Event("OutOfZone", data));
    }
}
