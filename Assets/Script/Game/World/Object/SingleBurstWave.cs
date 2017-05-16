using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleBurstWave : MonoBehaviour {

    [SerializeField]
    private int damage;

	void Start ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        Damageable damageable = collider.gameObject.GetComponent<Damageable>();
        if (damageable != null)
        {
            damageable.DealDamage(damage);
        }
    }
}
