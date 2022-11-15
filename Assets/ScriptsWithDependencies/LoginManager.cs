using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using System;

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

    public void LoginButton()
    {
        //StartCoroutine(CallLoginWithCoroutine());
        LoginUserOnPlayFab();

    }

    public void CancelLoginScene()
    {
        loginPopUp.SetActive(false);
    }

    private void LoginUserOnPlayFab()
    {
        var request = new LoginWithPlayFabRequest();
        request.TitleId = PlayFabSettings.TitleId;
        request.Username = username.text;
        request.Password = password.text;

        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginFailed);
    }


    private void OnLoginFailed(PlayFabError obj)
    {
        Debug.Log("login is failed");
        wrongloginWarning.GetComponent<Text>().text = "Error: " + obj.Error;
        wrongloginWarning.SetActive(true);
    }

    private void OnLoginSuccess(LoginResult obj)
    {
        Debug.Log("login is successful");
        LoadUserInfo();
        PlayerInfo.username = username.text;
        SceneManager.LoadScene("StartNewsMenu");
    }

    private void LoadUserInfo()
    {
        var request = new GetPlayerStatisticsRequest();
        request.StatisticNames = new List<string>() { "Credits", "money", "Point" };
        PlayFabClientAPI.GetPlayerStatistics(request, OnGetStaticsSuccess, error => Debug.LogError(error.GenerateErrorReport()));
    }

    private void OnGetStaticsSuccess(GetPlayerStatisticsResult obj)
    {
        foreach (var stat in obj.Statistics)
        {
            print("Statistic: " + stat.StatisticName + ", Wert: " + stat.Value);
            switch (stat.StatisticName)
            {
                case "Credits":
                    PlayerInfo.score = stat.Value;
                    break;
            }
        }
    }



    // this method is deprecated, use LoginUserOnPlayFab 
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
