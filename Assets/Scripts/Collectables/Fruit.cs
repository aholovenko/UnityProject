using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : Collectable {

	public AudioClip sound = null;
	AudioSource source=null;

	protected override void OnRabbitHit(HeroRabbit rabbit)
	{
		CollectableHide ();
		source = gameObject.AddComponent<AudioSource> ();
		source.clip = sound;
		source.Play ();
	}
}
