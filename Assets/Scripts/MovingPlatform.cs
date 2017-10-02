using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public Vector3 MoveBy;
    Vector3 pointA;
    Vector3 pointB;
	// Use this for initialization
	void Start () {
        pointA = this.transform.position;
        pointB = this.pointA + MoveBy;		
	}
	
	// Update is called once per frame
	void Update () {
	}
}
