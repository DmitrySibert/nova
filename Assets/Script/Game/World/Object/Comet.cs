using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comet : MonoBehaviour {

    [SerializeField]
    private float lifeTime;
    [SerializeField]
    private int damage;
    [SerializeField]
    public SuperNova superNova;

    private Transform trans;
    private float sizeDecRate;
    private EventBus evtBus;
     
	// Use this for initialization
	void Start ()
    {
        trans = gameObject.GetComponent<Transform>();
        evtBus = FindObjectOfType<EventBus>();
        sizeDecRate = trans.localScale.x * 0.7f / lifeTime;
    }
	
	// Update is called once per frame
	void Update ()
    {
        lifeTime -= Time.deltaTime;
        trans.localScale = new Vector3(
            trans.localScale.x - sizeDecRate * Time.deltaTime,
            trans.localScale.y - sizeDecRate * Time.deltaTime,
            trans.localScale.z
        );
        if (lifeTime <= 0f)
        {
            superNova.size = Random.Range(1, 5) * 10;
            Data data = new Data();
            data["position"] = trans.position;
            evtBus.TriggerEvent(new Event("CometDeath", data));
            DestroyObject(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Damageable damageable = col.gameObject.GetComponent<Damageable>();
        if (damageable != null)
        {
            damageable.DealDamage(damage);
        }
    }
}
