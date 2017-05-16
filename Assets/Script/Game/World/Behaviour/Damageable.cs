using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour {

    private DiscreteHealthBar health;

	void Start ()
    {
        health = gameObject.GetComponentInChildren<DiscreteHealthBar>();
	}

    public void DealDamage(int points)
    {
        health.DecreaseBy(points);
    }
}
