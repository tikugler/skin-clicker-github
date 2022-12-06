using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogoutManager : MonoBehaviour
{

    void Awake()
    {
        if (!Account.LoggedIn)
            gameObject.GetComponent<Button>().interactable = false;

    }

    public void CallLogOut()
    {
        Account.LogOutUser();
        SceneManager.LoadScene("StartMenu");
    }
}
