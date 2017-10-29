using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalScript : MonoBehaviour {

	public float time;
	public float fadetime;
	private float current_time;

	private float alpha = 1;

	SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		current_time = time;
		sr = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (current_time <= 0) {
			sr.color = new Color (sr.color.r, sr.color.g, sr.color.b, alpha);
			alpha -= (1 / fadetime) * Time.deltaTime;
			if (alpha <= 0) {
				Destroy (gameObject);
			}
		} else {
			current_time -= Time.deltaTime;
		}
	}
}
