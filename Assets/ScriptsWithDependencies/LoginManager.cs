using System;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    public GameObject loginPopUp;
    public InputField username;
    public InputField password;
    public GameObject wrongloginWarning;

    /// <summary>
    /// if User login info is saved in PlayerPrefs,
    /// it is loaded
    /// </summary>
    private void Awake()
    {

        if (Account.GetIfThereIsSavedUserLoginInfoPlayerPrefs())
            LoginUserOnPlayFab(Account.GetUsernamePlayerPrefs(),
                Account.GetPasswordPlayerPrefs());

        else if (Account.GetIfThereIsSavedGuestCustomIdPlayerPrefs())
            LoginWithGuestCustomID(Account.GetGuestCustomIdPlayerPrefs());
    }

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
        string usernameText = username.text;
        string passwordText = password.text;
        LoginUserOnPlayFab(usernameText, passwordText);

    }

    public void CancelLoginScene()
    {
        loginPopUp.SetActive(false);
    }

    /// <summary>
    /// makes a login request by using given inputs
    /// </summary>
    private void LoginUserOnPlayFab(string usernameText, string passwordText)
    {
        var request = new LoginWithPlayFabRequest();
        request.TitleId = PlayFabSettings.TitleId;
        request.Username = usernameText;
        request.Password = passwordText;

        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginFailed);
    }


    /// <summary>
    /// called if login-request has failed
    /// </summary>
    /// <param name="obj"></param>
    private void OnLoginFailed(PlayFabError obj)
    {
        Debug.Log("login has failed");
        Debug.Log(obj.Error);
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
        Debug.Log(obj.ToJson());
        Debug.Log("login is successful");
        Account.SetPlayFabIdAndUserName(obj.PlayFabId, username.text);
        if (!Account.GetIfThereIsSavedUserLoginInfoPlayerPrefs())
            Account.SetUserLoginPlayerPrefs(username.text, password.text);
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
        //request.StatisticNames = new List<string>() { "Credits" };
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


    /// <summary>
    /// called when user click on "Als Gast Spielen" Button
    /// user is created as guast in Playfab
    /// </summary>
    public void CallPlayAsGuest()
    {
        Account.guestCustomID = DateTime.Now.ToString("yyyyMMddHHmmssffff") + SystemInfo.deviceUniqueIdentifier;
        var request = new LoginWithCustomIDRequest { CustomId = Account.guestCustomID, CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccessAsGuest, error => Debug.Log(error.Error));


    }


    /// <summary>
    /// user loggs in as guest with the given questCustomID
    /// </summary>
    /// <param name="questCustomID">unique guest id</param>
    private void LoginWithGuestCustomID(string questCustomID)
    {
        var request = new LoginWithCustomIDRequest { CustomId = questCustomID, CreateAccount = false };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccessAsGuest, error => Debug.Log(error.Error));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj">success result</param>
    private void OnLoginSuccessAsGuest(LoginResult obj)
    {

        Account.SetPlayFabIdAndUserName(obj.PlayFabId, "Guest");
        if (!Account.GetIfThereIsSavedGuestCustomIdPlayerPrefs())
            Account.SetGuestCustomIdPlayerPrefs(Account.guestCustomID);
        LoadUserStatistics();
        SceneManager.LoadScene("StartNewsMenu");
    }
}
