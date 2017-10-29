using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalScript : MonoBehaviour {

	public float time;
	private float current_time;

	// Use this for initialization
	void Start () {
		current_time = time;
	}
	
	// Update is called once per frame
	void Update () {
		if (current_time <= 0) {
			Destroy (gameObject);
		} else {
			current_time -= Time.deltaTime;
		}
	}
}
