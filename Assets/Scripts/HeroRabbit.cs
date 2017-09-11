using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabbit : MonoBehaviour {

    public float speed = 1;

    Rigidbody2D body = null;

	// Use this for initialization
	void Start () {
        body = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float val = Input.GetAxis("Horizontal");

        if (Mathf.Abs(val) > 0)
        {
            Vector2 vel = body.velocity;
            vel.x = speed * val;
            body.velocity = vel;
        }

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (val > 0)
            sr.flipX = false;
        else if (val < 0)
            sr.flipX = true;
    }
}
