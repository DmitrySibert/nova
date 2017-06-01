using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightLineMovement : MonoBehaviour {

    private float distPerSec;
    public float DistancePerSec
    {
        set { distPerSec = value; }
    }

    private float moveTime;
    public float MoveTime
    {
        set { moveTime = value; }
    }

    private Vector3 direction;
    public Vector3 Direction
    {
        set { direction = value; }
    }

    private Transform trans;
    private float curMoveTime;

	private void Start ()
    {
        trans = GetComponent<Transform>();
        curMoveTime = 0;
    }
	
	private void Update ()
    {
        curMoveTime += Time.deltaTime;
        if (curMoveTime >= moveTime) {
            Destroy(this);
        }
        trans.position = new Vector3(
            trans.position.x + direction.x * distPerSec * Time.deltaTime,
            trans.position.y + direction.y * distPerSec * Time.deltaTime,
            trans.position.z
        );
    }
}
