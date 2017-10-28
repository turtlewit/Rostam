using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Shake : MonoBehaviour {

    public IEnumerator Shake(float magnitude, int time = 5)
    {
        magnitude *= 0.1f;
        for(int i = 0; i < time; ++i)
        {
            transform.position = new Vector3(transform.position.x + Random.Range(-magnitude, magnitude), transform.position.y + Random.Range(-magnitude, magnitude), -10);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
