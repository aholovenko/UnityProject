using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeOrc : OrcBase {

    public GameObject weapon;
    public float MinDistanceToAttack = 2;
    public float MinInterval = 0.5f;
    float last;

    bool isTimeToAttack()
    {
        Vector3 rab_pos = HeroRabbit.currentRabbit.transform.position;
        Vector3 my_pos = this.transform.position;

        if (Mathf.Abs(rab_pos.x - my_pos.x) < this.MinDistanceToAttack)
            if (Time.time - last > MinInterval)
            {
                last = Time.time;
                return true;
            }
        return false;
    }

     protected override void performAttack()
     {
        if (isTimeToAttack())
        {
            GameObject carrot = GameObject.Instantiate(weapon);

            carrot.transform.position = this.transform.position;

            Carrots car = carrot.GetComponent<Carrots>();

            Vector3 pos = HeroRabbit.currentRabbit.transform.position;

            Vector3 my_pos = this.transform.position;

            this.animator.SetTrigger("attack");

            if (my_pos.x > pos.x)
            {
                car.launch(-1);
            }
            else if (my_pos.x < pos.x)
                car.launch(1);
        }
     }


     IEnumerator later()
     {
        yield return new WaitForSeconds(0.2f);
     }
}
