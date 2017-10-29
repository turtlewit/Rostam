using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highlight_Script : MonoBehaviour {

    public Text t;
    private AudioSource a;

    void Start()
    {
        a = GetComponent<AudioSource>();
    }


	void OnMouseEnter()
    {
        t.color = new Color(1, 1, 0);
        a.Play();
    }

    void OnMouseExit()
    {
        t.color = new Color(1, 1, 1);
    }
}
