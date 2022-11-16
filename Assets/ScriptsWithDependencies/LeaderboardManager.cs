using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{
    public GameObject LeaderboardPanel;
    public GameObject LeaderboardButton;
    public GameObject[] LeaderboardEntry;
    private int firstPlayerPosition;
    private LeaderboardEntryTemplate selectedPlayer;


    private void Awake()
    {
        if (!PlayerInfo.LoggedIn)
            LeaderboardButton.GetComponent<Button>().interactable = false;
    }

    public void OpenLeaderboard()
    {
        GetLeaderboardAroundPlayer();        
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
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardSuccess, error => Debug.Log(error));
        
        
    }



    private void OnLeaderboardSuccess(GetLeaderboardResult result)
    {

        int i = 0;

        if(result.Leaderboard.Count == 0)
        {
            firstPlayerPosition -= 10;
            return;
        }

        if(selectedPlayer != null)
            selectedPlayer.RemoveHighlighting();

        foreach (var item in result.Leaderboard)
        {
            Debug.Log("item.PlayFabId: " + item.PlayFabId);
            Debug.Log("PlayerInfo.playerID): " + PlayerInfo.playerID);

            if (item.DisplayName == PlayerInfo.username)
            {
                selectedPlayer = LeaderboardEntry[i].GetComponent<LeaderboardEntryTemplate>();
                selectedPlayer.HighlightEntry();
            }

            LeaderboardEntry[i].GetComponent<LeaderboardEntryTemplate>().
                SetTexts((item.Position + 1).ToString(), item.DisplayName, item.StatValue.ToString());

            if (i == 0)
            {
                firstPlayerPosition = item.Position;
            }
            i++;
        }

        while (i < 10)
        {
            LeaderboardEntry[i].GetComponent<LeaderboardEntryTemplate>().
                SetTexts("", "", "");
            i++;
        }
        LeaderboardPanel.SetActive(true);


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

        if (selectedPlayer != null)
            selectedPlayer.RemoveHighlighting();

        int i = 0;
        foreach (var item in result.Leaderboard)
        {
            Debug.Log("item.PlayFabId: " + item.PlayFabId);
            Debug.Log("PlayerInfo.playerID): " + PlayerInfo.playerID);

            if (item.DisplayName == PlayerInfo.playerID)
            {
                selectedPlayer = LeaderboardEntry[i].GetComponent<LeaderboardEntryTemplate>();
                selectedPlayer.HighlightEntry();
            }

            LeaderboardEntry[i].GetComponent<LeaderboardEntryTemplate>().
                SetTexts((item.Position + 1).ToString(), item.DisplayName, item.StatValue.ToString());
            i++;

        }

        LeaderboardPanel.SetActive(true);
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
       
        GetLeaderboard(firstPlayerPosition);

    }


}
