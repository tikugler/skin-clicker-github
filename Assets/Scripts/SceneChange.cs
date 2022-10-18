using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public String SceneName = "MainGame";
    public void SwitchToMainGameScreen()
    {
        {
            SceneManager.LoadScene(SceneName);
        }

    }
}
