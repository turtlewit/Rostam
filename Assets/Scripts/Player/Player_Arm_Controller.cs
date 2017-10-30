using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Arm_Controller : MonoBehaviour {

    private Vector2 world_pos, target, pos;
    public float offset, offset2;
    public float top, bottom;
    public GameObject arm, arm_flip;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        world_pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        target = new Vector2(world_pos.x, world_pos.y);
        pos = new Vector2(transform.position.x, transform.position.y);
        Vector2 dir = (target - pos).normalized;

        if(transform.parent.parent.localScale.x == 1)
        {
            if (transform.rotation.eulerAngles.z > 120 && transform.rotation.eulerAngles.z < 300)
            {
                arm.SetActive(false);
                arm_flip.SetActive(true);
            }
            else
            {
                arm_flip.SetActive(false);
                arm.SetActive(true);
            }
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, (Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x)) + offset));

        }
        else
        {
            print("y");
            if (transform.rotation.eulerAngles.z > top && transform.rotation.eulerAngles.z < bottom)
            {
                arm.SetActive(false);
                arm_flip.SetActive(true);
            }
            else
            {
                arm_flip.SetActive(false);
                arm.SetActive(true);
            }
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, (Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x)) + offset + offset2));

        }


    }
}
