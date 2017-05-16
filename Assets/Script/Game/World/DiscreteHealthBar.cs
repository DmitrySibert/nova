using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscreteHealthBar : DiscreteHealthScore {

    [SerializeField]
    private Sprite[] sprites;

    private SpriteRenderer barRenderer;

	public override void Start()
    {
        base.Start();
        barRenderer = gameObject.GetComponent<SpriteRenderer>();
        barRenderer.sprite = sprites[curLife];
    }

    public override void DecreaseBy(int hp)
    {
        base.DecreaseBy(hp);
        barRenderer.sprite = sprites[curLife];
    }

    public override void IncreaseBy(int hp)
    {
        base.IncreaseBy(hp);
        barRenderer.sprite = sprites[curLife];
    }
}
