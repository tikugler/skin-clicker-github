using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class LeaderboardManager : MonoBehaviour
{
    public GameObject LeaderboardPanel;
    public GameObject[] LeaderboardEntry;
    private int firstPlayerPosition;
    private int playerCount;


    void Start()
    {
        LoginUserOnPlayFab();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenLeaderboard()
    {
        //GetLeaderboardAroundPlayer();
        GetLeaderboard();
        
    }

    public void CloseLeaderboard()
    {
        LeaderboardPanel.SetActive(false);

    }


    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest();
        request.StatisticName = "Credits";
        request.MaxResultsCount = 10;
        request.StartPosition = 0;
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardSuccess, error => Debug.Log(error));
    }

    public void GetLeaderboard(int startPosition)
    {
        var request = new GetLeaderboardRequest();
        request.StatisticName = "Credits";
        request.MaxResultsCount = 10;
        request.StartPosition = startPosition;
        request.AuthenticationContext = new PlayFabAuthenticationContext();
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardSuccess, error => Debug.Log(error));
        
    }



    private void OnLeaderboardSuccess(GetLeaderboardResult result)
    {
        int i = 0;
        foreach (var item in result.Leaderboard)
        {
            LeaderboardEntry[i].GetComponent<LeaderboardEntryTemplate>().
                SetTexts((item.Position + 1).ToString(), item.DisplayName, item.StatValue.ToString());
            if (i == 0)
            {
                firstPlayerPosition = item.Position;
            }
            //Debug.Log(item.Position + " - " + item.DisplayName + " - " + item.StatValue);
            i++;

        }

        LeaderboardPanel.SetActive(true);


        //Debug.Log(result.Leaderboard.ToArray().GetValue(0).Position);
    }


    public void GetLeaderboardAroundPlayer()
    {
        var request = new GetLeaderboardAroundPlayerRequest();
        request.StatisticName = "Credits";
        request.MaxResultsCount = 10;
       
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnLeaderboardAroundPlayerSuccess, error => Debug.Log(error));
        


    }

 
    private void OnLeaderboardAroundPlayerSuccess(GetLeaderboardAroundPlayerResult result)
    {

        int i = 0;
        Debug.Log(result.CustomData);
        Debug.Log(result.Request);
        Debug.Log(result.Leaderboard.Count);
        Debug.Log(result.Leaderboard.Capacity);
        foreach (var item in result.Leaderboard)
        {
            LeaderboardEntry[i].GetComponent<LeaderboardEntryTemplate>().
                SetTexts((item.Position + 1).ToString(), item.DisplayName, item.StatValue.ToString());
            //Debug.Log(item.Position + " - " + item.DisplayName + " - " + item.StatValue);
            i++;

        }

        LeaderboardPanel.SetActive(true);


        //Debug.Log(result.Leaderboard.ToArray().GetValue(0).Position);
    }


    private void LoginUserOnPlayFab()
    {
        var request = new LoginWithPlayFabRequest();
        request.TitleId = PlayFabSettings.TitleId;
        request.Username = "as8";
        request.Password = "123456";

        PlayFabClientAPI.LoginWithPlayFab(request, success => Debug.Log("Successful"),
            error => Debug.Log("Failed"));

        

        //var segmentRequest = new GetPlayerSegmentsRequest ;


    }

    public void GetLeaderboardLeftPage()
    {
        firstPlayerPosition -= 10;
        if (firstPlayerPosition < 0)
            firstPlayerPosition = 0;
        GetLeaderboard(firstPlayerPosition);

    }

    public void GetLeaderboardRightPage()
    {
        firstPlayerPosition += 10;
        //if (firstPlayerPosition < 0)
            //firstPlayerPosition = 0;
        GetLeaderboard(firstPlayerPosition);

    }


}
