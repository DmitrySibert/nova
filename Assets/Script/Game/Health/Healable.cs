using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healable : MonoBehaviour {

    private DiscreteHealthBar health;

    void Start()
    {
        health = gameObject.GetComponentInChildren<DiscreteHealthBar>();
    }

    public void Heal(int points)
    {
        health.IncreaseBy(points);
    }
}
