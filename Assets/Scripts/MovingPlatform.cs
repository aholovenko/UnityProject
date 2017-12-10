using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	public Vector3 MoveBy;
	public Vector3 speed;
	public float delay=0;
	private float arrivedTime;

    Vector3 pointA;
    Vector3 pointB;
	Vector3 target;

	private Mode mode;

	// Use this for initialization
	void Start () {
        pointA = this.transform.position;
        pointB = this.pointA + MoveBy;	
		mode = Mode.toB;
	}

	private enum Mode{
		toA,
		toB
	}
	
	// Update is called once per frame
	void Update () {
		if (arrivedTime + delay > Time.time)
			return;
		
		if (mode == Mode.toB && hasArrived (transform.position, pointB)) {
			arrivedTime = Time.time;
			mode = Mode.toA;
			target = pointA;
			this.transform.Translate (-speed * Time.deltaTime);
		}
		if (mode == Mode.toA && hasArrived (transform.position, pointA)) {
			arrivedTime = Time.time;
			mode = Mode.toB;
			target = pointB;
			this.transform.Translate (speed * Time.deltaTime);
		}

	}

	bool hasArrived(Vector3 a, Vector3 b)
	{
		return Mathf.Abs(a.x - b.x) < 0.1f;
	}
}
