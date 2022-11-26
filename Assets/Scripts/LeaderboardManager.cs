using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{
    public GameObject LeaderboardPanel;  // Panel for LEaderboard
    public GameObject LeaderboardButton;  // Button which opens Leaderboard
    public GameObject[] LeaderboardEntry;  // 10 Rows/Entries in Leaderboard
    private int firstPlayerPosition;  // the position of player in the displayed table 
    private LeaderboardEntryTemplate selectedPlayer;  // the entry of logged in user in the displayed table
    private const int ENTRY_COUNT = 10;


    /// <summary>
    /// Button to open leaderboard is not interactable
    /// if there is no logged in user
    /// (only logged in users can access the leaderboard)
    /// </summary>
    private void Awake()
    {
        if (!Account.LoggedIn)
            LeaderboardButton.GetComponent<Button>().interactable = false;
    }

    /// <summary>
    /// opens the leaderboard panel around the logged in player
    /// called when user clicks on the LeaderboardButton
    /// </summary>
    public void OpenLeaderboard()
    {
        GetLeaderboardAroundPlayer();        
    }

    /// <summary>
    /// closes the leaderboard panel
    /// </summary>
    public void CloseLeaderboard()
    {
        LeaderboardPanel.SetActive(false);

    }

    /// <summary>
    /// sends an API request to display the best 10 players by Credits
    /// </summary>
    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest();
        request.StatisticName = "Credits";
        request.MaxResultsCount = ENTRY_COUNT;
        request.StartPosition = 0;
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardSuccess, error => Debug.Log(error));
    }

    /// <summary>
    /// sends an API request to display 10 playes from given startPosition by Credits
    /// if startPosition is 3, then it showsplayers from position 3 to 12
    /// </summary>
    /// <param name="startPosition">the position of the player in the first row</param>
    public void GetLeaderboard(int startPosition)
    {
        var request = new GetLeaderboardRequest();
        request.StatisticName = "Credits";
        request.MaxResultsCount = ENTRY_COUNT;
        request.StartPosition = startPosition;
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardSuccess, error => Debug.Log(error));   
    }


    /// <summary>
    /// called if positive response is taken from GetLeaderboard requests.
    /// if response contains players, they are placed in the Leaderboard panel.
    /// if logged in player is among them, it is highlighted
    /// </summary>
    /// <param name="result"> response,
    /// contains Information of 0-10 players in leaderboard in given interval</param>
    private void OnLeaderboardSuccess(GetLeaderboardResult result)
    {
        int i = 0;
        if(result.Leaderboard.Count == 0)
        {
            firstPlayerPosition -= ENTRY_COUNT;
            return;
        }
        if(selectedPlayer != null)
            selectedPlayer.RemoveHighlighting();
        foreach (var item in result.Leaderboard)
        {
            if (item.PlayFabId == Account.accountId)
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

        while (i < ENTRY_COUNT)
        {
            LeaderboardEntry[i].GetComponent<LeaderboardEntryTemplate>().
                SetTexts("", "", "");
            i++;
        }
        LeaderboardPanel.SetActive(true);
    }


    /// <summary>
    /// sends a request to display the leaderboard with 10 players
    /// centered on the logged in player
    /// </summary>
    public void GetLeaderboardAroundPlayer()
    {
        var request = new GetLeaderboardAroundPlayerRequest();
        request.StatisticName = "Credits";
        request.MaxResultsCount = ENTRY_COUNT;    
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request,
            OnLeaderboardAroundPlayerSuccess, error => Debug.Log(error));
    }


    /// <summary>
    /// displays the leaderboard centered on the requested player
    /// if it is possible.
    /// Otherwise, the user might be at the top or bottom
    /// </summary>
    /// <param name="result">ontains Information of 0-10 players in
    /// leaderboard centered on the requested player</param>
    private void OnLeaderboardAroundPlayerSuccess(GetLeaderboardAroundPlayerResult result)
    {

        if (selectedPlayer != null)
            selectedPlayer.RemoveHighlighting();

        int i = 0;
        foreach (var item in result.Leaderboard)
        {

            if (item.PlayFabId == Account.accountId)
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

        LeaderboardPanel.SetActive(true);
    }

    /// <summary>
    /// shows previous 10 players in the leaderboard.
    /// the position of the player in the first row cannot be smaller than 0
    /// called if left arrow if clicked
    /// </summary>
    public void GetLeaderboardLeftPage()
    {
        firstPlayerPosition -= ENTRY_COUNT;
        if (firstPlayerPosition < 0)
            firstPlayerPosition = 0;
        GetLeaderboard(firstPlayerPosition);

    }

    /// <summary>
    /// shows next 10 players ın the leaderboard
    /// In the GetLeaderboard, it is checked, if there are players to show.
    // called if left arrow if clicked
    /// </summary>
    public void GetLeaderboardRightPage()
    {
        firstPlayerPosition += ENTRY_COUNT;
        GetLeaderboard(firstPlayerPosition);

    }
}
