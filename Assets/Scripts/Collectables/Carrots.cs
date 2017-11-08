using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrots : Collectable{

    float direction = 0;
    float speed = 3;

    private void Start()
    {
        StartCoroutine(later());
    }

    IEnumerator later()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }

    public void launch (float direction) {
		this.direction = direction;
		if (direction < 0) {
			speed = -speed;
			GetComponent<SpriteRenderer> ().flipX = true;
		}
	}

    private void Update()
    {
        Vector3 pos = this.transform.position;

        this.transform.position = pos + Vector3.right * this.direction*Time.deltaTime*speed;
    }

    protected override void OnRabbitHit(HeroRabbit rabbit)
    {
        rabbit.die();
		CollectableHide ();
    }
}
