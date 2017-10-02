using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : Collectable {

    public UILabel coinsLabel;
    public float coins_quantity = 0;

    protected override void OnRabbitHit(HeroRabbit rabbit)
    {
        coinsLabel.text = coins_quantity.ToString();
        Destroy(this.gameObject);
    
        }
}
