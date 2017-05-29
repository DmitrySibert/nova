using System;
using System.Collections.Generic;
using UnityEngine;

public class DiscreteHealthScore : MonoBehaviour {

    [SerializeField]
    protected int maxLife;

    protected int curLife;
    public int CurrentLife
    {
        get { return curLife; }
    }

    virtual public void Start()
    {
        curLife = maxLife;
    }
	
	void Update ()
    {
	}

    virtual public void DecreaseBy(int hp)
    {
        curLife -= hp;
        if (curLife < 0)
        {
            curLife = 0;
        }
    }

    virtual public void IncreaseBy(int hp)
    {
        curLife += hp;
        if (curLife > maxLife)
        {
            curLife = maxLife;
        }
    }
}
