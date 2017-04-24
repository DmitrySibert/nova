using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {

    [SerializeField]
    private SuperNova m_superNova;

	// Use this for initialization
	void Start ()
    {
        Debug.Log("GameState initialized");
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SpawnSuperNova(Vector2 pos)
    {
        m_superNova.size = Random.Range(1, 5) * 10;
        Instantiate<SuperNova>(m_superNova, pos, Quaternion.identity);
    }
}
