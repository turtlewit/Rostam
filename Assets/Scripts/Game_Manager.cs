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

    private Vector3[] colors = new Vector3[12];

    public int[] wave_enemy_count;
    int wave = 0;
    int death_count = 0;

    void Start()
    {
        colors[0] = new Vector3(.2588f, .7607f, .9568f);
        colors[1] = new Vector3(40f / 255f, 132f / 255f, 168 / 255f);
        colors[2] = new Vector3(69f / 255f, 181f / 255f, 52f / 255f);
        colors[3] = new Vector3(15f / 255f, 119f / 255f, 0);
        colors[4] = new Vector3(219f / 255f, 70f / 255f, 70f / 255f);
        colors[5] = new Vector3(170f / 255f, 8f / 255f, 30f / 255f);
        colors[6] = new Vector3(170f / 255f, 8f / 255f, 146f / 255f);
        colors[7] = new Vector3(234f / 255f, 100f / 255f, 214f / 255f);
        colors[8] = new Vector3(100f / 255f, 234f / 255f, 230f / 255f);
        colors[9] = new Vector3(207f / 255f, 216f / 255f, 23f / 255f);
        colors[10] = new Vector3(246f / 255f, 1, 81f / 255f);
        colors[11] = new Vector3(64f / 255f, 206f / 255f, 66f / 255f);
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
        if (player_shoot.deepen_shot < 0.3)
            player_shoot.deepen_shot += game_multiplier / 20f;
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
            Vector3 new_color = colors[Random.Range(0, 13)];
            gsr.color = new Color(new_color.x, new_color.y, new_color.z) ;
			g.GetComponent<Enemy_Destroy> ().initial_color = gsr.color;

            yield return new WaitForSeconds(Random.Range(0.6f, 1.2f));
            count++;
        }
    }

}
