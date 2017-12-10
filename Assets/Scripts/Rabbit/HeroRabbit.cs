using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabbit : MonoBehaviour {

    public bool isDead = false;
    public float speed = 100;
    bool isGrounded = false;
    bool JumpActive = false;
    float JumpTime = 0f;
    public float MaxJumpTime = 2f;
    public float JumpSpeed = 2f;

    Rigidbody2D body = null;
    Animator animator = null;
    SpriteRenderer sr = null;

    public Transform heroParent = null;

    public static HeroRabbit currentRabbit = null;

    private void Awake()
    {
        currentRabbit = this;
    }

    // Use this for initialization
    void Start () {
        body = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        sr = this.GetComponent<SpriteRenderer>();
        //Зберігаємо позицію кролика на початку
        LevelController.current.setStartPosition(transform.position);
        this.heroParent = this.transform.parent;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float value = Input.GetAxis("Horizontal");
        if (Mathf.Abs(value) > 0)
        {
            Vector2 vel = body.velocity;
            vel.x = value * speed;
            body.velocity = vel;
        }

        if (value < 0)
        {
            sr.flipX = true;
        }
        else if (value > 0)
        {
            sr.flipX = false;
        }
        
        if (Mathf.Abs(value) > 0)
        {
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        }

        Vector3 from = transform.position + Vector3.up * 0.3f;
        Vector3 to = transform.position + Vector3.down * 0.1f;

        //Намалювати лінію (для розробника)
      //  Debug.DrawLine(from, to, Color.red);

        if (this.isDead)
            return;

        int layer_id = 1 << LayerMask.NameToLayer("Ground");
        //Перевіряємо чи проходить лінія через Collider з шаром Ground
        RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
        if (hit)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            this.JumpActive = true;
        }
        if (this.JumpActive)
        {
            //Якщо кнопку ще тримають
            if (Input.GetButton("Jump"))
            {
                this.JumpTime += Time.deltaTime;
                if (this.JumpTime < this.MaxJumpTime)
                {
                    Vector2 vel = body.velocity;
                    vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);
                    body.velocity = vel;
                }
            }
            else
            {
                this.JumpActive = false;
                this.JumpTime = 0;
            }
        }
        
        if (this.isGrounded)
        {
            animator.SetBool("jump", false);
        }
        else
        {
            animator.SetBool("jump", true);
        }

		if(hit) {
			//Перевіряємо чи ми опинились на платформі
			if(hit.transform != null
				&& hit.transform.GetComponent<MovingPlatform>() != null){
				//Приліпаємо до платформи
			SetNewParent(this.transform, hit.transform);
			}
		} else {
			//Ми в повітрі відліпаємо під платформи
			SetNewParent(this.transform, this.heroParent);
		}
				}

	private bool isBig=false;

	public void bombHit(){
		if (isBig) {
			transform.localScale = new Vector3 (1f, 1f, 0f);
			isBig = false;
		} else
			die ();
		
	}

	static void SetNewParent(Transform obj, Transform new_parent) {
		if(obj.transform.parent != new_parent) {
			//Засікаємо позицію у Глобальних координатах
			Vector3 pos = obj.transform.position;
			//Встановлюємо нового батька
			obj.transform.parent = new_parent;
			//Після зміни батька координати кролика зміняться
			//Оскільки вони тепер відносно іншого об’єкта
			//повертаємо кролика в ті самі глобальні координати
			obj.transform.position = pos;
		}
	}

	public void mushroomHit(){
		if (!isBig) {
			transform.localScale = new Vector3 (1.5f, 1.5f, 0f);
			isBig = true;
		}
	}
    
   // StickTo isStickedTo = null;

    public void die()
    {
        animator.SetBool("die", true);
        GetComponent<BoxCollider2D>().enabled = false;
        body.isKinematic = true;
        body.velocity = Vector2.zero;

        LevelController.current.onRabbitDeath(this);

    }

    public void reset()
    {
        animator.SetBool("die", false);

        GetComponent<BoxCollider2D>().enabled = true;
        body.isKinematic = false;
    }

}
