using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow_with_Mouse : MonoBehaviour {

    public GameObject thing_to_track;
    public float track_speed;
    private Vector3 position_to_track = Vector3.zero;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        position_to_track = thing_to_track.transform.position;
        position_to_track -= mouse;
        position_to_track = new Vector3(position_to_track.x, position_to_track.y, -10);


        transform.position = Vector2.Lerp(transform.position, position_to_track, track_speed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}
