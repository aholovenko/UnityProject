using System.Collections;
using System.Collections.Generic;
using UnityEngine;	
using UnityEngine.SceneManagement;

public class EnterLevel : MonoBehaviour {
    
	public string SceneName;

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.GetComponent<HeroRabbit>()!=null)
			SceneManager.LoadScene (SceneName);
	}

}
