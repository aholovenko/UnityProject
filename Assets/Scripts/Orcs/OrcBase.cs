using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcBase : MonoBehaviour
{

    protected Rigidbody2D body = null;
    protected Animator animator = null;
    protected SpriteRenderer sr = null;

    protected bool isDead = false;
	private bool needToAttack=false;
	private Mode mode;

    public float speed = 2;
	//protected float speedAttack=4;

    Vector3 pointA;
    Vector3 pointB;
    public Vector3 range = new Vector3(3, 0, 0);

	private enum Mode
	{
		GoToA,
		GoToB,
		Attack
	}

	void Awake(){
		body = this.GetComponent<Rigidbody2D> ();
		animator = this.GetComponent<Animator> ();
		sr = this.GetComponent<SpriteRenderer> ();
		mode = Mode.GoToB;
	}

    // Use this for initialization
    void Start()
    {
        pointA = this.transform.position;
        pointB = pointA + range;
    }
		

    // Update is called once per frame
    void FixedUpdate()
    {
		if (isDead)
			return;

		if (rabbitIsNear () && mode != Mode.Attack) {
			mode = Mode.Attack;
//			animator.SetTrigger ("attack");
			performAttack ();
		}
		else if (!rabbitIsNear () && mode == Mode.Attack) {
			mode = Mode.GoToA;
//			animator.SetTrigger ("attack");
		}

	//	move ();
 
		KeepInBounds();

    }

	protected bool rabbitIsNear(){
		Vector3 rab_pos = HeroRabbit.currentRabbit.transform.position;
		return rab_pos.x >= pointA.x && rab_pos.x <= pointB.x;
	}


	private void move(float value){
		Vector2 vel = body.velocity;
		float dir = getDirection();

		if (Math.Abs (dir) > 0) {
			vel.x = dir * speed;
			body.velocity = vel;
			if (dir > 0)
				sr.flipX = true;
			else
				sr.flipX = false;
			animator.SetBool("run",true);
		}
	}

	private void KeepInBounds()
	{
		Vector3 my_pos = this.transform.position;
		if (mode == Mode.GoToA && hasArrived(my_pos, this.pointA))
		{
			mode = Mode.GoToB;
		}
		else if (mode == Mode.GoToB && hasArrived(my_pos, this.pointB))
		{
			mode = Mode.GoToA;
		}
	}

    
    private float getDirection()
    {
        if (isDead)
            return 0;

		if (mode == Mode.GoToA)
                return -1;
		else if (mode == Mode.GoToB)
                return 1;

		Vector3 rab_pos = HeroRabbit.currentRabbit.transform.position;

		if (mode == Mode.Attack)
		{
			if (transform.position.x < rab_pos.x)
			{
				return 1;
			}
			else
			{
				return -1;
			}
		}

        return 0;
    }

	protected virtual void performAttack (){
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
