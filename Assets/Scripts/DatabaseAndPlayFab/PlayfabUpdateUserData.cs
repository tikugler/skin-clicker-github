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
    public void StartSetScoreOnPlayFabRepeating()
    {
        if (Account.LoggedIn)
        {
            InvokeRepeating("SetScoreOnPlayFab", 0, 15);
        }
    }


    // updates credits in DB
    private void SetScoreOnPlayFab()
    {
        int credits = Account.credits;
        Debug.Log("credits: " + credits);
        var request = new UpdatePlayerStatisticsRequest();
        request.Statistics = new List<StatisticUpdate>();
        var statCredits = new StatisticUpdate { StatisticName = "Credits", Value = credits };
        int leavingGameTime = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        var statLeavingTime = new StatisticUpdate { StatisticName = "LeavingGameTime", Value = leavingGameTime };
        request.Statistics.Add(statCredits);
        request.Statistics.Add(statLeavingTime);

        PlayFabClientAPI.UpdatePlayerStatistics(request, OnSetStatsSuccessful, OnSetStatsFailed);
    }


    /// <summary>
    /// this method is called when user performs upgrade in shop.
    /// it updates remained credits and performed upgrade for selected item in PlayFab
    /// </summary>
    /// <param name="upgradeName">name of selected upgrade</param>
    /// <param name="upgradeAmount">number of performed upgrade for selected item</param>
    public static void SetUpgradeAmountOnPlayFab(string upgradeName, int upgradeAmount)
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

    public static void UpdateStatisticOnPlayFab(string statisticName, int statisticValue)
    {
        if (!Account.LoggedIn)
            return;

        var request = new UpdatePlayerStatisticsRequest();
        request.Statistics = new List<StatisticUpdate>();
        var statUpgrade = new StatisticUpdate { StatisticName = statisticName, Value = statisticValue };
        request.Statistics.Add(statUpgrade);
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnSetStatsSuccessful, OnSetStatsFailed);
    }




    public static void UpdateStatisticOnPlayFab(Dictionary<string, int> statistics)
    {
        if (!Account.LoggedIn)
            return;

        var request = new UpdatePlayerStatisticsRequest();
        request.Statistics = new List<StatisticUpdate>();
        foreach(KeyValuePair<string, int> entry in statistics)
        {
            var statUpgrade = new StatisticUpdate { StatisticName = entry.Key, Value = entry.Value };
            request.Statistics.Add(statUpgrade);

        }
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
