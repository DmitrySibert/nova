using UnityEngine;
using Assets.Script.Game.GamePlay.Score;

public class Mode1 : MonoBehaviour {

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
}
