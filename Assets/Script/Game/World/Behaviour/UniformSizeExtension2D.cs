using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class UniformSizeExtension2D : MonoBehaviour {

    [SerializeField]
    private float scalePerSecond;

    private Transform trans;
    private Vector3 prevScale;

	void Start()
    {
        trans = gameObject.GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        Extend();
    }

    private void Extend()
    {
        trans.localScale = new Vector3(
            trans.localScale.x + scalePerSecond * Time.fixedDeltaTime,
            trans.localScale.y + scalePerSecond * Time.fixedDeltaTime,
            trans.localScale.z
        );
    }

    private void StopExtending()
    {
        trans.localScale = new Vector3(
            trans.localScale.x - scalePerSecond * Time.fixedDeltaTime,
            trans.localScale.y - scalePerSecond * Time.fixedDeltaTime,
            trans.localScale.z
        );
        enabled = false;
        Destroy(this);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        StopExtending();
        FindObjectOfType<EventBus>().TriggerEvent(new Event("SizeExtensionEnd"));
    }
}
