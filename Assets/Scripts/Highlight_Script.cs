using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highlight_Script : MonoBehaviour {

    public Text t;


	void OnMouseEnter()
    {
        t.color = new Color(1, 1, 0);
    }

    void OnMouseExit()
    {
        t.color = new Color(1, 1, 1);
    }
}
