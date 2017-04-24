using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comet : MonoBehaviour {

    public float lifeTime;
    public SuperNova m_superNova;
    private Transform m_trans;
    private float m_sizeDecRate;

	// Use this for initialization
	void Start ()
    {
        m_trans = gameObject.GetComponent<Transform>();
        m_sizeDecRate = m_trans.localScale.x * 0.7f / lifeTime;
    }
	
	// Update is called once per frame
	void Update ()
    {
        lifeTime -= Time.deltaTime;
        m_trans.localScale = new Vector3(
            m_trans.localScale.x - m_sizeDecRate * Time.deltaTime,
            m_trans.localScale.y - m_sizeDecRate * Time.deltaTime,
            m_trans.localScale.z
        );
        if (lifeTime <= 0f)
        {
            m_superNova.size = Random.Range(1, 5) * 10;
            Instantiate<SuperNova>(m_superNova, m_trans.position, Quaternion.identity);
            DestroyObject(gameObject);
        }
    }
}
