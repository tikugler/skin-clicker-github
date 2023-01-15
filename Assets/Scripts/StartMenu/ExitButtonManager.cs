using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Action for exit button
/// </summary>
public class ExitButtonManager : MonoBehaviour
{
    /// <summary>
    /// Quits application and stops game.
    /// </summary>
    public void exitGame()
    {
        Application.Quit();
        Debug.Log("Exit Game");
    }
}
