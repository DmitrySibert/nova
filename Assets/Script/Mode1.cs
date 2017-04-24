using UnityEngine;

public class Mode1 : MonoBehaviour {

    public static Mode1 instance = null;

    [SerializeField]
    private GameState gameStatePref;
    [SerializeField]
    private PcController pcControllerPref;
    // Use this for initialization
    void Start () {

        GameState gameState = Instantiate<GameState>(gameStatePref);
        PcController pcCont = Instantiate<PcController>(pcControllerPref);
        pcCont.gameState = gameState;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }
}
