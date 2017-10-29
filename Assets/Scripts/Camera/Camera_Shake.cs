using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Shake : MonoBehaviour {

    public float shake_mult = 1;

    public IEnumerator Shake(float magnitude, int time = 5)
    {
        magnitude *= 0.1f;
        for(int i = 0; i < time; ++i)
        {
            //float randx = Random.Range(-magnitude, magnitude)) * shake_mult;
            //float randy = Random.Range(-magnitude, magnitude)) * shake_mult
            //print()
            transform.position = new Vector3(transform.position.x + Random.Range(-magnitude, magnitude) * shake_mult, transform.position.y + Random.Range(-magnitude, magnitude) * shake_mult, -10);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
