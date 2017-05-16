using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wormhole : MonoBehaviour {

    [SerializeField]
    private GameObject m_spawnObject;
    [SerializeField]
    private Transform m_spawnPoint;
    [SerializeField]
    private float m_spawnPower;

    private Collider2D m_collider;
    private float m_timeCount;
    private Dispatcher m_dispatcher;
    private bool m_isActive;

    // Use this for initialization
    void Start()
    {
        m_dispatcher = gameObject.GetComponent<Dispatcher>();
        m_isActive = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Event evt = m_dispatcher.ReceiveEvent();
        if (evt != null)
        {
            if (evt.Name.Equals("LeftMouseUp") && m_isActive)
            {
                m_isActive = false;
                Vector2 v2 = evt.Data.Get<Vector3>("clickPoint");
                SpawnComet(v2);
            }
            if (evt.Name.Equals("PlayerTurn"))
            {
                m_isActive = true;
            }
        }
    }

    public void SpawnComet(Vector2 dirPoint)
    {
        Vector2 spawnPoint = m_spawnPoint.position;
        GameObject comet = Instantiate<GameObject>(m_spawnObject, spawnPoint, Quaternion.identity);
        Rigidbody2D cometRb = comet.GetComponent<Rigidbody2D>();
        Vector2 force = m_spawnPower * (dirPoint - spawnPoint).normalized;
        cometRb.AddForce(force, ForceMode2D.Impulse);
    }
}
