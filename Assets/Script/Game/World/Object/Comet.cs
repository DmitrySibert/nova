using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comet : MonoBehaviour {

    [SerializeField]
    private float lifeTime;
    [SerializeField]
    private int damage;

    private Transform trans;
    private EventBus evtBus;
    private Vector3 prevFramePos;
     
	void Start ()
    {
        trans = gameObject.GetComponent<Transform>();
        evtBus = FindObjectOfType<EventBus>();
    }
	
	void Update ()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0f) {
            Data data = new Data();
            data["position"] = prevFramePos;
            evtBus.TriggerEvent(new Event("CometDeath", data));
            DestroyObject(gameObject);
        } else {
            prevFramePos = trans.position;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Damageable damageable = col.gameObject.GetComponent<Damageable>();
        if (damageable != null) {
            damageable.DealDamage(damage);
        }
    }
}
