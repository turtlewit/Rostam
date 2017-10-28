using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Arm_Controller : MonoBehaviour {

    private Vector2 world_pos, target, pos;
    public float offset;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        world_pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        target = new Vector2(world_pos.x, world_pos.y);
        pos = new Vector2(transform.position.x, transform.position.y);
        Vector2 dir = (target - pos).normalized;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, (Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x)) + offset));

    }
}
