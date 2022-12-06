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

    // the method SetScoreOnPlayFab is called every 15 seconds
    void Start()
    {
        if (Account.LoggedIn)
        {
            InvokeRepeating("SetScoreOnPlayFab", 15, 15);
        }  
    }


    // updates credits in DB
    public void SetScoreOnPlayFab()
    {
        int credits = Account.credits;
        Debug.Log("credits: " + credits);
        var request = new UpdatePlayerStatisticsRequest();
        request.Statistics = new List<StatisticUpdate>();
        var stat = new StatisticUpdate { StatisticName = "Credits", Value = credits };
        request.Statistics.Add(stat);
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnSetStatsSuccessful, OnSetStatsFailed);
    }


    /// <summary>
    /// this method is called when user performs upgrade in shop.
    /// it updates remained credits and performed upgrade for selected item in PlayFab
    /// </summary>
    /// <param name="upgradeName">name of selected upgrade</param>
    /// <param name="upgradeAmount">number of performed upgrade for selected item</param>
    public void SetUpgradeAmountOnPlayFab(string upgradeName, int upgradeAmount)
    {
        if (!Account.LoggedIn)
            return;

        int credits = Account.credits;
        Debug.Log("credits: " + credits);
        var request = new UpdatePlayerStatisticsRequest();
        request.Statistics = new List<StatisticUpdate>();
        var statCredits = new StatisticUpdate { StatisticName = "Credits", Value = credits };
        var statUpgrade = new StatisticUpdate { StatisticName = upgradeName, Value = upgradeAmount };
        request.Statistics.Add(statCredits);
        request.Statistics.Add(statUpgrade);
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnSetStatsSuccessful, OnSetStatsFailed);
    }


    // is called when UpdatePlayerStatistics succeed
    private static void OnSetStatsSuccessful(UpdatePlayerStatisticsResult obj)
    {
        Debug.Log("Stats are successfully updated");
    }

    // is called when UpdatePlayerStatistics fails
    private static void OnSetStatsFailed(PlayFabError obj)
    {
        Debug.Log("Stats cannot be updated");
        Debug.Log(obj.Error);

    }
}
