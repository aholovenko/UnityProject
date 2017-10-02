using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : MonoBehaviour {

    float speed = 5;

    Rigidbody2D body = null;
    public Animator animator = null;
    SpriteRenderer sr = null;
    bool isDead = false;
    public Vector3 pointA;
    public Vector3 pointB;
    public Vector3 diff= new Vector3(10,0,0);

    private void Start()
    {
        body = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        sr = this.GetComponent<SpriteRenderer>();
        pointA = this.transform.position;
        pointB = pointA + diff;

    }

    protected virtual float attackDirection()
    {
        Vector3 rab_pos = HeroRabbit.currentRabbit.transform.position;
        Vector3 my_pos = this.transform.position;
        if (Mathf.Abs(rab_pos.x - my_pos.x) < 0.2f)
            return 0;
        if (rab_pos.x < my_pos.x)
            return -1;
        else
            return 1;
    }


    protected virtual void performAttack()
    {
        Vector3 pos = HeroRabbit.currentRabbit.transform.position;
        Vector3 my_pos = this.transform.position;
        if(Mathf.Abs(pos.x-my_pos.x)<1f)
        this.animator.SetTrigger("attack");
    }

    protected virtual bool patrolAB()
    {
        return true;
    }

    public bool isArrived(Vector3 current, Vector3 target)
    {
        current.z = 0;
        target.z = 0;

        current.y = 0;
        target.y = 0;

        return Vector3.Distance(current, target) < 0.1f;
    }
    
    public enum Mode
    {
        GoToA,
        GoToB,
        Attack
    }

    Mode mode = Mode.GoToA;

    float getDirection()
    {
        if (isDead)
            return 0;

        if (patrolAB()){
            Vector3 my_pos = this.transform.position;

            if (mode == Mode.GoToA && isArrived(my_pos, this.pointA))
            {
                mode = Mode.GoToB;
            }
            else if (mode == Mode.GoToB && isArrived(my_pos, this.pointB))
            {
                mode = Mode.GoToA;
            }

            Vector3 target = Vector3.zero;

            if (mode == Mode.GoToA)
                target = this.pointA;
            else if (mode == Mode.GoToB)
                target = this.pointB;

            if (my_pos.x < target.x)
                return -1;
            else if (my_pos.x > target.x)
                return 1;
        }
        return 0;
    }

    private void FixedUpdate()
    {
        float value = this.getDirection();

        Vector2 velocity = body.velocity;
        velocity.x = speed * value;
        body.velocity = velocity;

        if (value > 0)
            sr.flipX = true;
        else if (value < 0)
            sr.flipX = false;

        this.performAttack();
    }

    void onCollideWithRabbit(HeroRabbit rabbit)
    {
        float rab_y = rabbit.transform.position.y;
        float my_y = this.transform.position.y;

        if(my_y<rab_y&&rab_y-my_y>0.5f)
            this.orcDie();
    }

    void orcDie()
    {
        this.animator.SetBool("die", true);

        this.isDead = true;

        body.isKinematic = true;
        this.GetComponent<BoxCollider2D>().enabled = false;

        StartCoroutine(hideMeLater());
    }

    IEnumerator hideMeLater()
    {
        yield return new WaitForSeconds(3);

        Destroy(this.gameObject);

    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (isDead)
            return;

        HeroRabbit rabbit = collider.GetComponent<HeroRabbit>();
        if (rabbit != null)
        {
            onCollideWithRabbit(rabbit);
        }
    }
    


}
