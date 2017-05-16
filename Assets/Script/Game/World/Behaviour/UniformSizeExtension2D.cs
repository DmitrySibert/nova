using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class UniformSizeExtension2D : MonoBehaviour {

    [SerializeField]
    private float scalePerSecond;

    private Transform trans;

	void Start ()
    {
        trans = gameObject.GetComponent<Transform>();
    }
	
	void Update ()
    {
        Extend();
    }

    private void Extend()
    {
        trans.localScale = new Vector3(
            trans.localScale.x + scalePerSecond * Time.deltaTime,
            trans.localScale.y + scalePerSecond * Time.deltaTime,
            trans.localScale.z
        );
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(this);
    }
}
