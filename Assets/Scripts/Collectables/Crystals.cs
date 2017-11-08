using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystals : Collectable {

	protected override void OnRabbitHit(HeroRabbit rabbit)
	{
		CollectableHide ();
	}
}
