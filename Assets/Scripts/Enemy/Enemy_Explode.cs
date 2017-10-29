using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Explode : MonoBehaviour {

	bool grounded = false;
	ParticleSystem ps;
	public float max_time;
	private float time = 0;

	bool notGrounded = false;
	bool played = false;


	// Use this for initialization
	void Start () {
		foreach (ParticleSystem comp in gameObject.GetComponentsInChildren(typeof(ParticleSystem))) {
			if (comp.name == "Particle System 2") {
				ps = comp;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		touching_ground ();

		if (grounded == false) {
			notGrounded = true;
		}
		if (grounded == true && notGrounded == true) { // lol
			if (!played) {
				ps.gameObject.GetComponent<BloodParticleScript> ().spawned_sprites = 0;
				ps.gameObject.transform.rotation = Quaternion.Euler (-90, 0, 0);
				ps.Play ();
				GetComponent<SpriteRenderer> ().enabled = false;
				GetComponent<BoxCollider2D> ().enabled = false;
				played = true;
			}
			if (time >= max_time) {
				Destroy (gameObject);
			} else {
				time += Time.deltaTime;
			}
		}
	}

	void touching_ground()
	{
		RaycastHit2D rh = Physics2D.Raycast(transform.position, Vector2.down, 1.6f, ~(1 << 9 | 1 << 8));
		RaycastHit2D rh2 = Physics2D.Raycast(new Vector2(transform.position.x + 0.4f, transform.position.y), Vector2.down, 1.6f, ~(1 << 9 | 1 << 8));
		RaycastHit2D rh3 = Physics2D.Raycast(new Vector2(transform.position.x - 0.4f, transform.position.y), Vector2.down, 1.6f, ~(1 << 9 | 1 << 8));
		if (!rh && !rh2 && !rh3)
			grounded = false;
		else if (rh)
		{
			if (rh.collider.tag == "Platform")
				grounded = true;
			else
				grounded = false;
		}
		else if(rh2)
		{
			if (rh2.collider.tag == "Platform")
				grounded = true;
			else
				grounded = false;
		}
		else if(rh3)
		{
			if (rh3.collider.tag == "Platform")
				grounded = true;
			else
				grounded = false;
		}
	}
}
