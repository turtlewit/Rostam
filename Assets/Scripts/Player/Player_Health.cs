using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Health : MonoBehaviour {

    public int health;
    private Rigidbody2D rb;
    bool invulerable = false;
    private Camera_Shake c;
    public SpriteRenderer sr;
    public GameObject[] health_sprites;
    int health_sprite_index = 0;

	// Use this for initialization
	void Start () {
        c = Camera.main.GetComponent<Camera_Shake>();
        rb = GetComponent<Rigidbody2D>();
	}

    void death()
    {
        //Loads continue scene      
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        SceneManager.LoadScene(2);
    }


    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Enemy" && !invulerable && col.gameObject.GetComponent<Enemy_Destroy>().enabled)
        {
            health_sprites[health_sprite_index++].SetActive(false);
            invulerable = true;
            health--;
            if (health == 0)
                death();
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
            sr.color = flash_on ? new Color(sr.color.r, sr.color.g, sr.color.b, .2f) : new Color(sr.color.r, sr.color.g, sr.color.b, 1);
            timer += 0.07f;
            flash_on = !flash_on;
            yield return new WaitForSeconds(0.07f);
        }
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
        invulerable = false;
    }
}
