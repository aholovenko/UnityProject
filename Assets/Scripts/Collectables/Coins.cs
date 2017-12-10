using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : Collectable {

    public UILabel coinsLabel;
    public float coins_quantity = 0;

	public AudioClip sound = null;
	AudioSource source=null;

    protected override void OnRabbitHit(HeroRabbit rabbit)
    {
	//	LevelController.Instance.AddCoins(1);
 //       coinsLabel.text = coins_quantity.ToString();
		CollectableHide ();
		source = gameObject.AddComponent<AudioSource> ();
		source.clip = sound;
		source.Play ();
        }
}
