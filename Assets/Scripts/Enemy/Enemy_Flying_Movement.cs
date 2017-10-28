using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Flying_Movement : MonoBehaviour {

	public GameObject player;
	private Rigidbody2D rb;
	public float speed;

	// Use this for initialization
	void Start () {
		player = transform.parent.GetComponent<Player_Access>().player;
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (player.transform.position.x - 1.1f > transform.position.x)
		{
			rb.velocity = new Vector2(1 * Time.deltaTime * speed, rb.velocity.y);

		}
		else if (player.transform.position.x + 1.1f < transform.position.x)
		{
			rb.velocity = new Vector2(-1 * Time.deltaTime * speed, rb.velocity.y);
		}
		else
		{
			rb.velocity = new Vector2(0, rb.velocity.y);
		}

		if (player.transform.position.y - 1.1f > transform.position.y)
		{
			rb.velocity = new Vector2(rb.velocity.x, 1 * Time.deltaTime * speed);

		}
		else if (player.transform.position.y + 1.1f < transform.position.y)
		{
			rb.velocity = new Vector2(rb.velocity.x, -1 * Time.deltaTime * speed);
		}
		else
		{
			rb.velocity = new Vector2(rb.velocity.x, 0);
		}
	}
}
