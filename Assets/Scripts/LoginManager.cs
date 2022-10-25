using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class LoginManager : MonoBehaviour
{
    public GameObject loginPopUp;
    public InputField username;
    public InputField password;
    public GameObject wrongloginWarning;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openLoginWindow()
    {
        loginPopUp.SetActive(true);
    }

    public void loginButton() {
        Debug.Log(username.text);
        if(username.text != "1234" || password.text != "1234")
        {
            wrongloginWarning.SetActive(true);
        }
        else
        {
            loginPopUp.SetActive(false);
            wrongloginWarning.SetActive(false);
        }
    }
    public void cancleLoginScene()
    {
        loginPopUp.SetActive(false);
    }
}
