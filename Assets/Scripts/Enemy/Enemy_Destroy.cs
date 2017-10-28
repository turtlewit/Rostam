using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Destroy : MonoBehaviour {

	public ParticleSystem ps;
	public Enemy_Flying_Movement efm;
	// Use this for initialization
	void Start () {
		ps = GetComponentInChildren<ParticleSystem>();
	}
	
	public void Destroy()
	{
		if (enabled)
		{
			if (efm)
			{
				GetComponent<Enemy_Flying_Movement>().enabled = false;
			}
			GetComponent<Enemy_Movement>().enabled = false;
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 8), ForceMode2D.Impulse);
			GetComponent<SpriteRenderer>().color = Color.gray;
			enabled = false;
		} else
		{
			gameObject.SetActive(false);
		}
	}



}
