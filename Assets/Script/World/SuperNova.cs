using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperNova : MonoBehaviour {

    enum NovaState { Extend, Static };
    private Transform m_trans;
    public float size;
    public Gravity m_gravity;
    private NovaState m_curState;
    private Renderer m_render;
    [SerializeField]
    private Color[] m_colors;
    [SerializeField]
    private float m_extend;
    [SerializeField]
    private Transform m_glowTrans;
    [SerializeField]
    private Renderer m_glowRender;
	// Use this for initialization
	void Start () {

        m_trans = gameObject.GetComponent<Transform>();
        m_render = gameObject.GetComponent<Renderer>();
        m_gravity.force = 1300f;
        Color color = m_colors[Random.Range(0, m_colors.Length)];
        m_render.material.color = color;
        m_glowRender.material.color = color;

    }
	
	// Update is called once per frame
	void Update () {
		
        switch(m_curState)
        {
            case NovaState.Extend:
                Extend();
                break;
            case NovaState.Static:
                Static();
                break;
        }
        m_glowTrans.Rotate(new Vector3(0f, 0f, 1f), 30 * Time.deltaTime);
        m_trans.Rotate(new Vector3(0f, 0f, 1f), -10 * Time.deltaTime);
    }

    private void Extend()
    {
        m_trans.localScale = new Vector3(m_trans.localScale.x + m_extend, m_trans.localScale.y + m_extend, m_trans.localScale.z);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(m_curState.Equals(NovaState.Extend))
        {
            m_curState = NovaState.Static;
            //m_trans.localScale = new Vector3(m_trans.localScale.x - m_extend, m_trans.localScale.y - m_extend, m_trans.localScale.z);
        }
    }

    private void Static()
    {
        return;
    }
}
