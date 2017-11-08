using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombs : Collectable {

    protected override void OnRabbitHit(HeroRabbit rabbit)
    {
		CollectableHide();
        rabbit.bombHit();
    }
}
