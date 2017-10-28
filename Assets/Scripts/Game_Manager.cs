using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour {

    private float game_multiplier = 0;
    public PlayerShooting player_shoot;

    void Start()
    {
        StartCoroutine(Game_Increase());
    }

	// Use this for initialization
	void Up_the_Ante () {
        print("upped");
        var main = player_shoot.explosion_ps.main;
        main.startSizeMultiplier += game_multiplier;
        var main2 = player_shoot.shoot_ps.main;
        main2.startSizeMultiplier += game_multiplier;
    }

    IEnumerator Game_Increase()
    {
        while(true)
        {
            game_multiplier += 0.002f;
            Up_the_Ante();
            yield return new WaitUntil(player_shoot.get_hit_enemy);
            player_shoot.reset_hit_enemy();
        }

    }
}
