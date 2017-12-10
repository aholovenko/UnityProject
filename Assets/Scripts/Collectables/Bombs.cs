using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombs : Collectable {

	public AudioClip sound = null;
	AudioSource source=null;

	void Start(){
		source = gameObject.AddComponent<AudioSource> ();
		source.clip = sound;
	}

    protected override void OnRabbitHit(HeroRabbit rabbit)
    {
		CollectableHide();
        rabbit.bombHit();
		source.Play ();
    }
}
