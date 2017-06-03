using System;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole : MonoBehaviour {

    [SerializeField]
    private Collider2D eventHorizon;
    [SerializeField]
    private float swallowTime;

    private Transform trans;
    private LinkedList<SuperNova> novas;
    private bool isSwallowingProceed;

    private void Start()
    {
        trans = GetComponent<Transform>();
        novas = new LinkedList<SuperNova>();
        isSwallowingProceed = false;
        FindObjectOfType<EventBus>().TriggerEvent(new Event("BlackholeBirth"));
    }

    private void Update()
    {
        if (isSwallowingProceed) {
            if (IsSwallowingEndUp()) {
                Death();
            }
        }
    }

    private bool IsSwallowingEndUp()
    {
        bool isAllSwallowed = true;
        foreach (SuperNova nova in novas) {
            if (nova != null) {
                isAllSwallowed = false;
                break;
            }
        }

        return isAllSwallowed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isSwallowingProceed = true;
        SuperNova[] novas = FindObjectsOfType<SuperNova>();
        foreach(SuperNova nova in novas) {
            if (nova.GetComponent<Collider2D>().IsTouching(eventHorizon)) {
                InitSwallow(nova);
            }
        }
        Data data = new Data();
        data["quantity"] = novas.Length;
        FindObjectOfType<EventBus>().TriggerEvent(new Event("BlackholeSwallow", data));
    }

    private void InitSwallow(SuperNova nova)
    {
        DestroyNovaPhysics(nova);
        AddSwallowEffects(nova);
        novas.AddLast(nova);
    }

    private void AddSwallowEffects(SuperNova nova)
    {
        Vector3 novaPos = nova.GetComponent<Transform>().position;
        
        Vector3 dir = (trans.position - novaPos).normalized;
        StraightLineMovement movement = nova.gameObject.AddComponent<StraightLineMovement>();
        movement.Direction = dir;
        movement.MoveTime = swallowTime;
        float distance = (trans.position - novaPos).magnitude;
        movement.DistancePerSec = distance / swallowTime;

        UniformSizeDiminish2D diminish = nova.gameObject.AddComponent<UniformSizeDiminish2D>();
        diminish.DiminishTime = swallowTime;
        FadeDestroy fade = nova.gameObject.AddComponent<FadeDestroy>();
        fade.FadeOutTime = swallowTime;
    }

    private void DestroyNovaPhysics(SuperNova nova)
    {
        Collider2D[] colliders = nova.GetComponentsInChildren<Collider2D>();
        Rigidbody2D[] rgBodies = nova.GetComponentsInChildren<Rigidbody2D>();
        DestroyComponents(colliders);
        DestroyComponents(rgBodies);
    }

    private void DestroyComponents(Component[] components)
    {
        foreach(Component comp in components) {
            Destroy(comp);
        }
    }

    private void Death()
    {
        FindObjectOfType<EventBus>().TriggerEvent(new Event("BlackholeDeath"));
        FadeDestroy fade = gameObject.AddComponent<FadeDestroy>();
        fade.FadeOutTime = 1;
    }
}
