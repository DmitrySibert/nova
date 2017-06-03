using UnityEngine;

public class SuperNova : MonoBehaviour {

    [SerializeField]
    private Color[] colors;
    [SerializeField]
    private float extend;
    [SerializeField]
    private Transform glowTrans;
    [SerializeField]
    private Renderer glowRender;
    [SerializeField]
    private DiscreteHealthScore health;
    [SerializeField]
    private SingleBurstWave singleBurstWave;
    public float size;

    private Transform trans;
    private Renderer render;
    private EventBus eventBus;

    void Start ()
    {
        trans = gameObject.GetComponent<Transform>();
        render = gameObject.GetComponent<Renderer>();
        eventBus = FindObjectOfType<EventBus>();
        InitSuperNova();
    }

    private void InitSuperNova()
    {
        Color color = colors[Random.Range(0, colors.Length)];
        render.material.color = color;
        glowRender.material.color = color;
        health.GetComponent<Renderer>().material.color = color;
        eventBus.TriggerEvent(new Event("SupernovaBirth"));
    }
	
	void Update ()
    {
        glowTrans.Rotate(new Vector3(0f, 0f, 1f), 30 * Time.deltaTime);
        trans.Rotate(new Vector3(0f, 0f, 1f), -10 * Time.deltaTime);
        if(health.CurrentLife == 0) {
            Death();
        }
    }

    private void Death()
    {
        SingleBurstWave burstWave = Instantiate<SingleBurstWave>(singleBurstWave, trans.position, Quaternion.identity);
        LimitedSizeExtend extend = burstWave.gameObject.AddComponent<LimitedSizeExtend>();
        extend.limit = render.bounds.size * 2;
        extend.extendFreqSec = 2;
        eventBus.TriggerEvent(new Event("SupernovaDeath"));
        Destroy(gameObject);
    }
}
