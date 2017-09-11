using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float value = Input.GetAxis("Horizontal");

        Animator animator = GetComponent<Animator>();
        if (value > 0)
            animator.SetBool("run", true);
        else if(value<0)
            animator.SetBool("run", false);
    }
}
