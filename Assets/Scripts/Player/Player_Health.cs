using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Health : MonoBehaviour {

    public int health;
    private Rigidbody2D rb;
    bool invulerable = false;
    private Camera_Shake c;
    public SpriteRenderer[] sr;
    public SpriteRenderer old_player_sprite;
    public GameObject[] health_sprites;
    int health_sprite_index = 0;
    public Sprite character_down;
    public Animator anim;
    public GameObject arm;
    private AudioSource hurt;

	// Use this for initialization
	void Start () {
        c = Camera.main.GetComponent<Camera_Shake>();
        rb = GetComponent<Rigidbody2D>();
        hurt = GetComponent<AudioSource>();   
	}

    IEnumerator death()
    {
        //Loads continue scene
        GetComponent<Player_Movement>().enabled = false;
        anim.enabled = false;
        arm.SetActive(false);
        for(int i = 0; i <sr.Length; ++i)
        {
            sr[i].enabled = false;
        }
        old_player_sprite.enabled = true;
        old_player_sprite.sprite = character_down;

        yield return new WaitForSeconds(3f);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        SceneManager.LoadScene(2);
    }


    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Enemy" && !invulerable && col.gameObject.GetComponent<Enemy_Destroy>().enabled && health > 0)
        {

            invulerable = true;
            health--;
            hurt.Play();
            if (health == 0)
                StartCoroutine(death());
            
            health_sprites[health_sprite_index++].GetComponent<Animator>().SetBool("hurt", true);
            rb.AddForce(new Vector2(-(col.transform.position - transform.position).x * 2, 5), ForceMode2D.Impulse);
            StartCoroutine(c.Shake(4));
            StartCoroutine(iframes(2));
        }
    }

    private IEnumerator iframes(float time)
    {
        float timer = Time.time;
        float end_time = timer + time;
        bool flash_on = false;
        //SpriteRenderer sr = GetComponent<SpriteRenderer>();

        while (timer < end_time)
        {
            for(int i = 0; i < sr.Length; ++i)
            {
                sr[i].color = flash_on ? new Color(sr[i].color.r, sr[i].color.g, sr[i].color.b, .2f) : new Color(sr[i].color.r, sr[i].color.g, sr[i].color.b, 1);
            }
            flash_on = !flash_on;
            timer += 0.07f;
            yield return new WaitForSeconds(0.07f);
        }
        for (int i = 0; i < sr.Length; ++i)
        {
            sr[i].color = new Color(sr[i].color.r, sr[i].color.g, sr[i].color.b, 1);
        }
        invulerable = false;
    }
}
