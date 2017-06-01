using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniformSizeDiminish2D : MonoBehaviour {

    [SerializeField]
    private float diminishTime;

    public float DiminishTime
    {
        set { diminishTime = value; }
    }

    private float diminishPerSecX;
    private float diminishPerSecY;
    private Transform trans;

    void Start()
    {
        trans = gameObject.GetComponent<Transform>();
        diminishPerSecX = trans.localScale.x / diminishTime;
        diminishPerSecY = trans.localScale.y / diminishTime;
    }

    void Update()
    {
        DiminishPerSec();
    }

    private void DiminishPerSec()
    {
        trans.localScale = new Vector3(
            trans.localScale.x - diminishPerSecX * Time.deltaTime,
            trans.localScale.y - diminishPerSecY * Time.deltaTime,
            trans.localScale.z
        );
    }
}
