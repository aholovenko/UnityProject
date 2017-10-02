using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    protected virtual void OnRabbitHit(HeroRabbit rabbit)
    {
       Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        HeroRabbit rabbit = collider.GetComponent<HeroRabbit>();
        if (rabbit != null)
            if(!rabbit.isDead)
                OnRabbitHit(rabbit);
    }
}
