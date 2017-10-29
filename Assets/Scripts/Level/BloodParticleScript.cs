using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticleScript : MonoBehaviour {

	public GameObject sprite_parent;
	public GameObject blood;
	public int max_sprites;

	public AudioSource aus;

	public int spawned_sprites = 0;
	private bool spawn_sprites = true;

	public List<ParticleCollisionEvent> collision_events;

	ParticleSystem ps;

	// Use this for initialization
	void Start () {
		sprite_parent = transform.parent.transform.parent.GetComponent<Player_Access> ().sprite_parent;

		aus = GetComponent<AudioSource> ();

		ps = GetComponent<ParticleSystem> ();
		collision_events = new List<ParticleCollisionEvent> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (sprite_parent == transform.parent.transform.parent.GetComponent<Player_Access> ().sprite_parent) {
			sprite_parent = transform.parent.transform.parent.GetComponent<Player_Access> ().sprite_parent;
		}
		
	}
	void OnParticleCollision(GameObject other){
		ParticlePhysicsExtensions.GetCollisionEvents (ps, other, collision_events);
		/*
		if (spawn_sprites) {
			for (int i = 0; i < collision_events.Count; i++) {
				GameObject obj = Instantiate (blood, collision_events [i].intersection, Quaternion.Euler (new Vector3 (Random.Range (0, 360), Random.Range (0, 360), 0)));
				obj.transform.localScale = new Vector3 (Random.Range (0.1f, 0.2f), Random.Range (0.1f, 0.2f), 1);
				obj.transform.SetParent (sprite_parent.transform);
				spawned_sprites++;
				if (spawned_sprites >= max_sprites) {
					spawn_sprites = false;
				}
			}
		}*/
		if (spawn_sprites) {
			GameObject obj = Instantiate (blood, collision_events [0].intersection, Quaternion.Euler (new Vector3 (0, 0, Random.Range (0, 360))));
			obj.transform.localScale = new Vector3 (Random.Range (0.1f, 0.2f), Random.Range (0.1f, 0.2f), 1);
			obj.transform.SetParent (sprite_parent.transform);
			spawned_sprites++;
			aus.PlayOneShot (aus.clip);
			if (spawned_sprites >= max_sprites) {
				spawn_sprites = false;
			}
		}
	}
}
