using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenOrc : OrcBase {

	public float speedA=3;

    protected override void performAttack(){
		if(!this.isDead)
			this.animator.SetTrigger("attack");
	//	if (SoundManager.isSoundOn()){
	//		AttackAudio.Play();
	//	}
		float value = this.attackDirection();

		Vector2 vel = body.velocity;
		vel.x = value * speedA;
		this.body.velocity = vel;
    }

    protected override float attackDirection()
    {
        Vector3 rab_pos = HeroRabbit.currentRabbit.transform.position;
        Vector3 my_pos = this.transform.position;

        if (rab_pos.x < my_pos.x)
            return -1;
        else 
            return 1;
    }

    }
