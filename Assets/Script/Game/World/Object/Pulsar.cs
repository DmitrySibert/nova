using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulsar : MonoBehaviour {

    [SerializeField]
    private GameObject pulse;
    [SerializeField]
    private Renderer render;

    private Transform trans;
    private Dispatcher dispatcher;
    private DiscreteHealthScore health;

	void Start ()
    {
        trans = gameObject.GetComponent<Transform>();
        dispatcher = gameObject.GetComponent<Dispatcher>();
        health = gameObject.GetComponent<DiscreteHealthScore>();
	}
	
	void Update ()
    {
        Event evt = dispatcher.ReceiveEvent();
        if (evt.Name.Equals("PlayerTurn")) {
            SpawnPulse();
            health.DecreaseBy(1);
        }

        if (health.CurrentLife == 0) {
            Death();
        }
	}

    private void SpawnPulse()
    {
        GameObject pulseGo = Instantiate<GameObject>(pulse, trans.position, Quaternion.identity);
        Bounds pulseBounds = pulseGo.GetComponent<Renderer>().bounds;
        Vector3 oldSize = pulseBounds.size;
        pulseBounds.Encapsulate(render.bounds);
        Vector3 newSize = pulseBounds.size;
        float scale = newSize.x / oldSize.x;
        pulseGo.GetComponent<Transform>().localScale *= scale;

        LimitedSizeExtend extend = pulseGo.gameObject.GetComponent<LimitedSizeExtend>();
        extend.limit = render.bounds.size * 2;
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
