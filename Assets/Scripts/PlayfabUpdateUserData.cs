using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;
using System;

/// <summary>
/// This script is used to update user data in DB Server
/// it is going to be developed more after PlayerInfo script is finished
/// </summary>
public class PlayfabUpdateUserData : MonoBehaviour
{
    DummyButton dummyButton;

    // the method SetScoreOnPlayFab is called every 15 seconds
    void Start()
    {
        if (PlayerInfo.LoggedIn)
        {
            dummyButton = GameObject.FindGameObjectWithTag("MainButton").GetComponent<DummyButton>();
            dummyButton.SetCredits(PlayerInfo.score);
            InvokeRepeating("SetScoreOnPlayFab", 15, 15);
        }  
    }

    // updates credits in DB
    public void SetScoreOnPlayFab()
    {
        int credits = dummyButton.GetCredits();
        Debug.Log("credits: " + credits);
        var request = new UpdatePlayerStatisticsRequest();
        request.Statistics = new List<StatisticUpdate>();
        var stat = new StatisticUpdate { StatisticName = "Credits", Value = credits };
        request.Statistics.Add(stat);
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnSetStatsSuccessful, OnSetStatsFailed);
    }

    // is called when UpdatePlayerStatistics succeed
    private void OnSetStatsSuccessful(UpdatePlayerStatisticsResult obj)
    {
        Debug.Log("Stats are successfully updated");
    }

    // is called when UpdatePlayerStatistics fails
    private void OnSetStatsFailed(PlayFabError obj)
    {
        Debug.Log("Stats cannot be updated");
        Debug.Log(obj.Error);

    }


}
