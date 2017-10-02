using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenOrc : OrcBase {

    protected override void performAttack()
    {

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
