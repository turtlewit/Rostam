using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    public bool is_auto;
    public GameObject bullet_shell;
    public float deepen_shot;

    public Game_Manager gm;
    private AudioSource audio_source;
    //0 = kill enemy
    //1 = destroy enemy body
    public AudioClip[] sounds;
    public AudioSource shoot_sound;

	public Camera c;
	public GameObject line;

	public float accuracy;

	public float shoot_knockback;
    public ParticleSystem shoot_ps, explosion_ps;

	LineRenderer lr;
	Vector3 line_target;

	int draw_line_frames;

	Vector3 world_pos;
	Vector2 target;
	Vector2 pos;

    float reload = 0;
    public float shoot_timer;

	// Use this for initialization
	void Start () {
        audio_source = GetComponent<AudioSource>();
		lr = line.GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

		lr.SetPosition(1, transform.position);

        if (Input.GetButtonDown("Fire1") || reload > shoot_timer)
        {
            Shoot();
            reload = 0;
        }
        else if (Input.GetButton("Fire1"))
        {
            print(shoot_timer);
            if (is_auto)
                Shoot();
            else
                reload += Time.deltaTime;
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

	void Shoot()
	{
        audio_source.clip = sounds[2];
        audio_source.pitch = Random.Range(0.9f - deepen_shot, 1.1f - deepen_shot);
        audio_source.Play();
        world_pos = c.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, c.nearClipPlane));
		target = new Vector2(world_pos.x, world_pos.y);
		pos = new Vector2(transform.position.x, transform.position.y);
		Vector2 dir = (target - pos).normalized;

        shoot_ps.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x)));
        shoot_ps.Play();

   

		float n_accuracy = (float)(accuracy * (new System.Random().NextDouble() * new System.Random().NextDouble())); // Great hack to reduce chance of high direction variation
		dir = new Vector2(dir.x + Random.Range(-n_accuracy, n_accuracy), dir.y + Random.Range(-n_accuracy, n_accuracy));

		RaycastHit2D raycast = Physics2D.Raycast(pos, dir, 100f, ~(1 << 9 | 1 << 2));
		
		if (raycast.collider)
		{
            explosion_ps.transform.position = raycast.point;
            explosion_ps.Play();


			if (raycast.collider.gameObject.tag == "Enemy")
			{
				Enemy_Destroy destroy_script = raycast.collider.gameObject.GetComponent<Enemy_Destroy>();
				ParticleSystem ps = destroy_script.ps;


                //Debug.Log(transform.position.x - raycast.point.x);

                if ((transform.position.x - raycast.point.x) > 0)
				{
					ps.transform.rotation = Quaternion.Euler(new Vector3(200, 90, 0));
				} else
				{
					ps.transform.rotation = Quaternion.Euler(new Vector3(-20, 90, 0));
				}
				ps.Play();
                if (destroy_script.enabled)
                {
                    gm.Up_the_Ante();
                    audio_source.clip = sounds[0];
                    audio_source.pitch = Random.Range(0.7f, 1.2f);
                    audio_source.Play();
                }
                else
                {
                    audio_source.clip = sounds[1];
                    audio_source.pitch = Random.Range(0.7f, 1.2f);
                    audio_source.Play();
                }
                destroy_script.Destroy();

				raycast.collider.GetComponent<Rigidbody2D>().AddForce(dir * shoot_knockback, ForceMode2D.Impulse);
			}
			line_target = raycast.point;
			draw_line_frames = 2;
		} else
		{
			line_target = new Vector3(pos.x, pos.y, 0) + new Vector3(dir.x, dir.y, 0) * 100;
			draw_line_frames = 2;
		}

        GameObject g = Instantiate(bullet_shell, transform.position, Quaternion.identity, null);
        g.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 15f));
        g.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-2f, 2f));
        StartCoroutine(c.GetComponent<Camera_Shake>().Shake(1, 3));

    }
}
