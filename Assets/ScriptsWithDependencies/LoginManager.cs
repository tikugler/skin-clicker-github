using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using System;
using TMPro;

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

    /// <summary>
    /// makes a login request by using given inputs
    /// </summary>
    private void LoginUserOnPlayFab()
    {
        var request = new LoginWithPlayFabRequest();
        request.TitleId = PlayFabSettings.TitleId;
        request.Username = username.text;
        request.Password = password.text;

        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginFailed);
    }


    /// <summary>
    /// called if login-request has failed
    /// </summary>
    /// <param name="obj"></param>
    private void OnLoginFailed(PlayFabError obj)
    {
        Debug.Log("login has failed");
        wrongloginWarning.GetComponent<Text>().text = "Error: " + obj.Error;
        wrongloginWarning.SetActive(true);
    }


    /// <summary>
    /// called if LoginWithPlayFab returns a success message
    /// sets username, PlayerId and loads UserStatistics
    /// </summary>
    /// <param name="obj"></param>
    private void OnLoginSuccess(LoginResult obj)
    {
        Debug.Log("login is successful");
        Account.SetPlayFabIdAndUserName(obj.PlayFabId, username.text);
        LoadUserStatistics();
        var loggedInUser = GameObject.Find("UserName").GetComponent<TextMeshProUGUI>();
        loggedInUser.text = username.text;
        SceneManager.LoadScene("StartNewsMenu");
    }

    /// <summary>
    /// a method to load the user statictics such as credits or upgrades from DB
    /// </summary>
    private void LoadUserStatistics()
    {
        var request = new GetPlayerStatisticsRequest();
        request.StatisticNames = new List<string>() { "Credits" };
        PlayFabClientAPI.GetPlayerStatistics(request, OnGetStatisticsSuccess, error => Debug.LogError(error.GenerateErrorReport()));
    }


    /// <summary>
    /// called If GetPlayerStatistics returns a success message 
    /// updates statistics in Player class regarding to saved ones
    /// </summary>
    /// <param name="obj">positive response with statistics</param>
    private void OnGetStatisticsSuccess(GetPlayerStatisticsResult obj)
    {
        Account.SetStatistics(obj.Statistics);
        
    }
}
