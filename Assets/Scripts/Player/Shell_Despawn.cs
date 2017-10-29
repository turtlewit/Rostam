using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell_Despawn : MonoBehaviour {

    private SpriteRenderer sr;
    private float timer;
	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if(timer > 10f)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - 0.1f);
            if (sr.color.a <= 0)
                Destroy(gameObject);
        }
	}
}
