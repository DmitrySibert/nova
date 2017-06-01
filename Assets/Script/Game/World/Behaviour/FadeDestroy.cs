using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeDestroy : MonoBehaviour {

    [SerializeField]
    private float fadeOutTime;

    private float fadeAlphaPerSec;
    private float curLifeTime;

    public float FadeOutTime
    {
        set { fadeOutTime = value; }
    }

    private Renderer[] allRenderers;

	void Start ()
    {
        fadeAlphaPerSec = 1f / fadeOutTime;
        curLifeTime = 0;
        allRenderers = gameObject.GetComponentsInChildren<Renderer>();
    }
	
	void Update ()
    {
        curLifeTime += Time.deltaTime;
        if (curLifeTime >= fadeOutTime) {
            DestroyObject(gameObject);
        }
        foreach (Renderer renderer in allRenderers) {
            Color colorOld = renderer.material.color;
            float newAlpha = colorOld.a - fadeAlphaPerSec * Time.deltaTime;
            if (newAlpha < 0) {
                newAlpha = 0;
            }
            renderer.material.color = new Color(colorOld.r, colorOld.g, colorOld.b, newAlpha);
        }
	}

}
