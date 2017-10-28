using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour {

    private Rigidbody2D rb;
    public float speed, jump_power;
    private bool grounded = false;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
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
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - 0.2f);
        }

        if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
	}


    void touching_ground()
    {
        RaycastHit2D rh = Physics2D.Raycast(transform.position, Vector2.down, .55f);
        if(rh)
        {
            if (rh.collider.tag == "Platform")
                grounded = true;
        }
    }
}
