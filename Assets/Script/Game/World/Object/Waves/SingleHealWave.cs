using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleHealWave : MonoBehaviour {

    [SerializeField]
    private int healPoints;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Healable healable = collider.gameObject.GetComponent<Healable>();
        if (healable != null) {
            healable.Heal(healPoints);
        }
    }
}
