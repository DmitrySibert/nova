using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedSizeExtend : MonoBehaviour {

    public Vector3 limit;
    public float extendFreqSec;

    private Vector3 extendValue;
    private Transform objTrans;

	void Start ()
    {
        objTrans = gameObject.GetComponent<Transform>();
        Renderer rend = gameObject.GetComponent<Renderer>();
        extendValue.x = (limit.x - rend.bounds.size.x) / extendFreqSec;
        extendValue.y = (limit.y - rend.bounds.size.y) / extendFreqSec;
    }
	
	void Update ()
    {
        Vector3 scale = objTrans.localScale;
        objTrans.localScale = new Vector3(
            scale.x + extendValue.x * Time.deltaTime,
            scale.y + extendValue.y * Time.deltaTime,
            scale.z + extendValue.z * Time.deltaTime
        );
        if (IsLimitReach())
        {
            Destroy(gameObject);
        }
	}

    private bool IsLimitReach()
    {
        Vector3 size = gameObject.GetComponent<Renderer>().bounds.size;
        return size.x >= limit.x && size.y >= limit.y;
    }
}
