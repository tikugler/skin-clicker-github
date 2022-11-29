using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButtonManager : MonoBehaviour
{
    public void exitGame() {
        Application.Quit();
        Debug.Log("Exit Game");
    }
}
