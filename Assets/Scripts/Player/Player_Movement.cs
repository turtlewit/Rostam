using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour {

    private Rigidbody2D rb;
    public float speed, jump_power;
    private bool grounded = false;
    int shake_count = 0;
    public Animator anim;

	public SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        touching_ground();
		if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-1 * Time.deltaTime * speed, rb.velocity.y);
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(1 * Time.deltaTime * speed, rb.velocity.y);
        }
        if((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && grounded)
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

        if(!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
	}


    void touching_ground()
    {
        RaycastHit2D rh = Physics2D.Raycast(transform.position, Vector2.down, 1.6f, ~(1 << 9 | 1 << 2));
        RaycastHit2D rh3 = Physics2D.Raycast(new Vector2(transform.position.x - 0.4f, transform.position.y), Vector2.down, 1.6f, ~(1 << 9 | 1 << 2));
        if (!rh && !rh3)
            grounded = false;
        else if (rh)
        {
            if (rh.collider.tag == "Platform" || rh.collider.tag == "Enemy")
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
}
