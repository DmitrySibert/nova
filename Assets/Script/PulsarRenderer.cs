using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsarRenderer : MonoBehaviour {

    [SerializeField]
    private Transform rendererX;
    [SerializeField]
    private Transform rendererY;
    [SerializeField]
    private float pulseRateX;
    [SerializeField]
    private float pulseRateY;
    [SerializeField]
    private float highLimit;
    [SerializeField]
    private float lowLimit;

    void Start ()
    {
    }
	
	void Update ()
    {
        float newScaleX = rendererX.localScale.x + pulseRateX * Time.deltaTime;
        if (newScaleX >= highLimit)
        {
            newScaleX = highLimit;
            pulseRateX *= -1;
        }
        if (newScaleX <= lowLimit)
        {
            newScaleX = lowLimit;
            pulseRateX *= -1;
        }
        rendererX.localScale = new Vector3(newScaleX, rendererX.localScale.y, rendererX.localScale.z);

        float newScaleY = rendererY.localScale.y + pulseRateY * Time.deltaTime;
        if (newScaleY >= highLimit)
        {
            newScaleY = highLimit;
            pulseRateY *= -1;
        }
        if (newScaleY <= lowLimit)
        {
            newScaleY = lowLimit;
            pulseRateY *= -1;
        }
        rendererY.localScale = new Vector3(rendererY.localScale.x, newScaleY, rendererY.localScale.z);
    }

}
