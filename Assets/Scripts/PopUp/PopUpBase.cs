using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpBase : MonoBehaviour {

	//public MyButton closeButton;
	public MyButton backButton;
	//public TweenAlpha backgroundAnimation;

	public void close(){
		Time.timeScale = 1;
		NGUITools.Destroy (this.gameObject);
	}


	void Start () {
	//	closeButton.signalOnClick.AddListener (this.close);
		backButton.signalOnClick.AddListener (this.close);

	//	backgroundAnimation.Play ();
		Time.timeScale = 0;
	}

}
