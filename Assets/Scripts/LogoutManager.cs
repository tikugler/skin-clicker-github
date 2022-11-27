using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogoutManager : MonoBehaviour
{
    Button logoutButton;

    // Start is called before the first frame update
    void Start()
    {
        if (!Account.LoggedIn)
            gameObject.GetComponent<Button>().interactable = false;
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallLogOut()
    {
        Account.LogOutUser();
        SceneManager.LoadScene("StartMenu");
    }
}
