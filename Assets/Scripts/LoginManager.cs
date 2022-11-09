using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public void OpenLoginWindow()
    {
        loginPopUp.SetActive(true);
    }

    public void LoginButton() {
        StartCoroutine(CallLoginWithCoroutine());

    }

    public void CancelLoginScene()
    {
        loginPopUp.SetActive(false);
    }



    public IEnumerator CallLoginWithCoroutine()
    {

        string enteredUsername = username.text;
        string enteredPassword = password.text;

        CoroutineWithData cd = new CoroutineWithData(this, DatabaseManager.LoginPlayer(enteredUsername, enteredPassword));
        yield return cd.coroutine;

        string result = (cd.result as string);


        if (result[0] == '0')
        {

            PlayerInfo.username = enteredUsername;
            PlayerInfo.score = int.Parse(result.Split('\t')[1]);

            SceneManager.LoadScene("StartNewsMenu");
        }
        else
        { 
            wrongloginWarning.GetComponent<Text>().text = "Error Code: #" + result;
            wrongloginWarning.SetActive(true);
        }     
    }
}
