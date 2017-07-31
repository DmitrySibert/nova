using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationDisplay : MonoBehaviour {

    [SerializeField]
    private GameObject[] displayObjects;
    [SerializeField]
    private string[] objectTextNames;
    [SerializeField]
    private Text nextSpawnObjName;

    [SerializeField]
    private Text scoresText;

    private Dispatcher dispatcher;
    private Dictionary<GameObject, string> objectNames;

	private void Start ()
    {
        dispatcher = GetComponent<Dispatcher>();
        objectNames = new Dictionary<GameObject, string>();
        for(uint i = 0; i < displayObjects.Length; ++i) {
            objectNames[displayObjects[i]] = objectTextNames[i];
        }
    }
	
	private void Update ()
    {
        Event evt = dispatcher.ReceiveEvent();
        if (evt.Name.Equals("NextSpawn")) {
            GameObject spawn = evt.Data.Get<GameObject>("SpawnGameObject");
            string name = objectNames[spawn];
            nextSpawnObjName.text = objectNames[spawn];
        }
        if (evt.Name.Equals("ScoresUpdated")) {
            float scores = evt.Data.Get<float>("Scores");
            scoresText.text = scores.ToString("0");
        }
    }
}
