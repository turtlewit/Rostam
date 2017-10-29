using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour {

    private float game_multiplier = 0.05f;
    public PlayerShooting player_shoot;
    public Camera_Shake cs;
    public Player_Access pa;
    public Texture2D cursor;
    public GameObject[] spawn_locations;
    public GameObject[] enemies;
    public GameObject enemy_holder;
    public LineRenderer p_shot;

    public int[] wave_enemy_count;
    int wave = 0;
    int death_count = 0;

    void Start()
    {
        StartCoroutine(spawn());
        Cursor.SetCursor(cursor,new Vector2(16, 16), CursorMode.Auto);
        
    }

	// Use this for initialization
	public void Up_the_Ante () {
        cs.shake_mult += game_multiplier * 1.05f;
        var main = player_shoot.explosion_ps.main;
        main.startSizeMultiplier += game_multiplier;
        var main2 = player_shoot.shoot_ps.main;
        main2.startSizeMultiplier += game_multiplier;
        main2.startSpeedMultiplier += game_multiplier;
        p_shot.widthMultiplier += game_multiplier/10f;
        if(player_shoot.shoot_timer > 0.09f)
            player_shoot.shoot_timer -= game_multiplier / 30f;
        if (player_shoot.accuracy < 0.6)
            player_shoot.accuracy += game_multiplier / 20f;
        death_count++;
    }

    void Update()
    {
        if (death_count == wave_enemy_count[wave])
        {
            wave++;
            death_count = 0;
            StartCoroutine(spawn());
        }

    }

    private IEnumerator spawn()
    {
        int count = 0;
        yield return new WaitForSeconds(0.1f);
        if (wave == wave_enemy_count.Length)
            wave--;
        while (count < wave_enemy_count[wave])
        {
            GameObject g = Instantiate(enemies[Random.Range(0, 3)], spawn_locations[Random.Range(0, 4)].transform.position, Quaternion.identity, enemy_holder.transform);
			SpriteRenderer gsr = g.GetComponent<SpriteRenderer> ();
            gsr.color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
			g.GetComponent<Enemy_Destroy> ().initial_color = gsr.color;

            yield return new WaitForSeconds(Random.Range(0.6f, 1.2f));
            count++;
        }
    }

}
