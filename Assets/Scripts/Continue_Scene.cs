using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Continue_Scene : MonoBehaviour
{

    public void restart()
    {
        SceneManager.LoadScene(1);
    }

    public void close()
    {
        Application.Quit();
    }
}
