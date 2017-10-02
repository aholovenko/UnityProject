using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    public static LevelController current;

    private void Awake()
    {
        current = this;
    }

    Vector3 startingPosition;

    public void setStartPosition(Vector3 pos)
    {
        startingPosition = pos;
    }

    public void onRabbitDeath(HeroRabbit rabbit)
    {
        //При смерті кролика повертаємо на початкову позицію
        rabbit.isDead = true;
        StartCoroutine(later(rabbit));
    }


    IEnumerator later(HeroRabbit rabbit) {
        yield return new WaitForSeconds(2);

        rabbit.transform.position = this.startingPosition;
        rabbit.isDead = false;
        rabbit.reset();

    }
  


    }
