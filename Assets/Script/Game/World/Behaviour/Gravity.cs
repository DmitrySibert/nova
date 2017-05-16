using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour {

    public float force;
    private Transform m_tr;
	// Use this for initialization
	void Start () {
        m_tr = gameObject.GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.attachedRigidbody != null)
        {
            Vector2 objPos = other.attachedRigidbody.position;
            Vector2 forceDir = ((Vector2)m_tr.position - objPos).normalized;
            other.attachedRigidbody.AddForce(forceDir * force * Time.deltaTime);
        }
    }
}
