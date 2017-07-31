using UnityEngine;
using Assets.Script.Game.GamePlay.Score;

public class Mode1 : MonoBehaviour {

    public static Mode1 instance = null;

    [SerializeField]
    private GameState gameStatePref;
    [SerializeField]
    private PcController pcControllerPref;
    

    private void Start () {
        GameState gameState = Instantiate<GameState>(gameStatePref);
        gameState.ScoreCalculator = new ComboScoreCalculator();
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
