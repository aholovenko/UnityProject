using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcBase : MonoBehaviour
{

    protected Rigidbody2D body = null;
    protected Animator animator = null;
    protected SpriteRenderer sr = null;

    private bool isDead = false;

    public float speed = 3;

    Vector3 pointA;
    Vector3 pointB;
    public Vector3 range = new Vector3(3, 0, 0);

    // Use this for initialization
    void Start()
    {
        body = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        sr = this.GetComponent<SpriteRenderer>();
        pointA = this.transform.position;
        pointB = pointA + range;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float value = this.getDirection();
        Vector2 vel = body.velocity;
        vel.x = value * speed;
        body.velocity = vel;

        if (value > 0)
        {
            sr.flipX = true;
        }
        else if (value < 0)
        {
            sr.flipX = false;
        }

        this.performAttack();
    }

    private enum Mode
    {
        GoToA,
        GoToB,
        Attack
    }

    Mode mode = Mode.GoToA;

    protected virtual bool guarding()
    {
        return true;
    }

    private float getDirection()
    {
        if (isDead)
            return 0;

        Vector3 my_pos = this.transform.position;

        if (guarding())
        {
            if (mode == Mode.GoToA && hasArrived(my_pos, this.pointA))
            {
                mode = Mode.GoToB;
            }
            else if (mode == Mode.GoToB && hasArrived(my_pos, this.pointB))
            {
                mode = Mode.GoToA;
            }

            Vector3 target = Vector3.zero;

            if (mode == Mode.GoToA)
                target = this.pointA;
            else if (mode == Mode.GoToB)
                target = this.pointB;

            if (my_pos.x < target.x)
                return 1;
            else if (my_pos.x > target.x)
                return -1;
        }
        return 0;
    }

    protected virtual void performAttack()
    {
        if (mode != Mode.Attack)
            return;
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

    bool hasArrived(Vector3 a, Vector3 b)
    {
        return Mathf.Abs(a.x - b.x) < 0.1f;
    }

    void onCollideWithRabbit(HeroRabbit rabbit)
    {
        float rab_y = rabbit.transform.position.y;
        float my_y = this.transform.position.y;

        if (my_y < rab_y && rab_y - my_y > 0.5f)
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
        yield return new WaitForSeconds(1);

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
