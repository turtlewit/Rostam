using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Destroy : MonoBehaviour {

	public GameObject sprite_parent;
	public GameObject blood;

	public Color initial_color;

	public int number_of_decals;

	public float min_sprite_scale;
	public float max_sprite_scale;

	public ParticleSystem ps;
	public Enemy_Flying_Movement efm;
	// Use this for initialization
	void Start () {
		sprite_parent = GetComponentInParent<Player_Access> ().sprite_parent;
		ps = GetComponentInChildren<ParticleSystem>();
	}
	
	public void Destroy()
	{
		if (enabled)
		{
            GetComponent<Animator>().SetBool("dead", true);
			if (efm)
			{
				GetComponent<Enemy_Flying_Movement>().enabled = false;
			}
			GetComponent<Enemy_Movement>().enabled = false;
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 8), ForceMode2D.Impulse);
			GetComponent<SpriteRenderer>().color = Color.gray;
			GetComponent<Enemy_Explode> ().enabled = true;
			enabled = false;
		} else
		{
			for (int i = 0; i < number_of_decals; i++){
				GameObject obj = Instantiate (blood, transform.position, Quaternion.Euler (0, 0, Random.Range (0, 360)));
				float s = Random.Range (min_sprite_scale, max_sprite_scale);
				obj.transform.localScale = new Vector3 (s, s, 1);
				obj.transform.SetParent (sprite_parent.transform);
				obj.GetComponent<SpriteRenderer> ().color = initial_color;
			}
			Destroy (gameObject);
		}
	}



}
