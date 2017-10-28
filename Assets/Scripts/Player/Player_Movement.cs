using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour {

    private Rigidbody2D rb;
    public float speed, jump_power;
    private bool grounded = false, invulerable = false;
    private Camera_Shake c;
    int shake_count = 0;
    public Animator anim;

	public SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        c = Camera.main.GetComponent<Camera_Shake>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        touching_ground();
		if(Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-1 * Time.deltaTime * speed, rb.velocity.y);
        }
        if(Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(1 * Time.deltaTime * speed, rb.velocity.y);
        }
        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(new Vector2(0, jump_power), ForceMode2D.Impulse);
            grounded = false;
        }
        if(rb.velocity.y < 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - 0.3f);
        }
        if (rb.velocity.x != 0)
            anim.SetBool("moving", true);
        else
            anim.SetBool("moving", false);

        if((!Input.GetKey(KeyCode.D)) && (!Input.GetKey(KeyCode.A)))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
	}


    void touching_ground()
    {
        RaycastHit2D rh = Physics2D.Raycast(transform.position, Vector2.down, 1.5f);
        RaycastHit2D rh2 = Physics2D.Raycast(new Vector2(transform.position.x + 0.4f, transform.position.y), Vector2.down, 1.5f);
        RaycastHit2D rh3 = Physics2D.Raycast(new Vector2(transform.position.x - 0.4f, transform.position.y), Vector2.down, 1.5f);
        if (!rh && !rh2 && !rh3)
            grounded = false;
        else if (rh)
        {
            if (rh.collider.tag == "Platform" || rh.collider.tag == "Enemy")
                grounded = true;
            else
                grounded = false;
        }
        else if(rh2)
        {
            if (rh2.collider.tag == "Platform" || rh2.collider.tag == "Enemy")
                grounded = true;
            else
                grounded = false;
        }
        else if(rh3)
        {
            if (rh3.collider.tag == "Platform" || rh3.collider.tag == "Enemy")
                grounded = true;
            else
                grounded = false;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Enemy" && !invulerable && col.gameObject.GetComponent<Enemy_Destroy>().enabled)
        {
            invulerable = true;
            rb.AddForce(new Vector2(-(col.transform.position - transform.position).x * 2, 5), ForceMode2D.Impulse);
            StartCoroutine(c.Shake(4));
            StartCoroutine(iframes(2));
        }
    }

    private IEnumerator iframes(float time)
    {
        float timer = Time.time;
        float end_time = timer + time;
        bool flash_on = false;
        //SpriteRenderer sr = GetComponent<SpriteRenderer>();

        while(timer < end_time)
        {
            sr.color = flash_on ? new Color(sr.color.r, sr.color.g, sr.color.b, .2f) : new Color(sr.color.r, sr.color.g, sr.color.b, 1);
            timer += 0.07f;
            flash_on = !flash_on;
            yield return new WaitForSeconds(0.07f);
        }
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
        invulerable = false;
    }
}
