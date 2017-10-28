using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

	public Camera c;
	public GameObject line;

	LineRenderer lr;
	Vector3 line_target;

	int draw_line_frames;

	Vector3 world_pos;
	Vector2 target;
	Vector2 pos;

	// Use this for initialization
	void Start () {
		lr = line.GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		world_pos = c.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, c.nearClipPlane));
		target = new Vector2(world_pos.x, world_pos.y);
		pos = new Vector2(transform.position.x, transform.position.y);

		lr.SetPosition(1, transform.position);

		if (Input.GetButtonDown("Fire1")) {
			Shoot();
		}

		lr.enabled = false;
		if (draw_line_frames > 0)
		{
			DrawLine();
			draw_line_frames--;
		}
	}

	void DrawLine()
	{
		lr.enabled = true;
		lr.SetPosition(0, line_target);
	}

	/*
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(world_pos, 1);
	}
	*/

	void Shoot()
	{
		
		Vector2 dir = target - pos;
		RaycastHit2D raycast = Physics2D.Raycast(pos, dir);
		
		//Debug.DrawLine(new Vector3(pos.x, pos.y, 0), new Vector3(pos.x, pos.y, 0) + new Vector3(dir.x, dir.y, 0) * 10, Color.red, Mathf.Infinity);

		if (raycast.collider)
		{
			Debug.Log(raycast.collider.name);
			line_target = raycast.point;
			draw_line_frames = 2;
		} else
		{
			line_target = new Vector3(pos.x, pos.y, 0) + new Vector3(dir.x, dir.y, 0) * 100;
			draw_line_frames = 2;
		}

		
	}
}
