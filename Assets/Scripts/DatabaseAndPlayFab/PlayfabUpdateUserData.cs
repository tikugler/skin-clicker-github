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

    /// <summary>
    /// This method calls SetScoreOnPlayFab every 15 Seconds
    /// </summary>
    public void StartSetScoreOnPlayFabRepeating()
    {
        if (Account.LoggedIn)
        {
            InvokeRepeating("SetScoreOnPlayFab", 0, 15);
        }
    }


    /// <summary>
    /// This method updates a few statistics on PlayFab.
    /// </summary>
    private void SetScoreOnPlayFab()
    {
        int credits = Account.credits;
        int realMoney = Account.realMoney;

        var request = new UpdatePlayerStatisticsRequest();
        request.Statistics = new List<StatisticUpdate>();
        var statCredits = new StatisticUpdate { StatisticName = "Credits", Value = credits };
        var statRealMoney = new StatisticUpdate { StatisticName = "RealMoney", Value = realMoney };
        int leavingGameTime = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        var statLeavingTime = new StatisticUpdate { StatisticName = "LeavingGameTime", Value = leavingGameTime };
        request.Statistics.Add(statRealMoney);
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
        int realMoney = Account.realMoney;
        var request = new UpdatePlayerStatisticsRequest();
        request.Statistics = new List<StatisticUpdate>();
        var statCredits = new StatisticUpdate { StatisticName = "Credits", Value = credits };
        var statUpgrade = new StatisticUpdate { StatisticName = upgradeName, Value = upgradeAmount };
        request.Statistics.Add(statCredits);
        request.Statistics.Add(statUpgrade);
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnSetStatsSuccessful, OnSetStatsFailed);
    }

    /// <summary>
    /// in Playfab, statistics consist of key and value
    /// key is the SKIN_ + SKIN_ID (SKIN_ is the prefix to distinguish the skin from items)
    /// As a default value, the number 1 is given (points out that skin is bought but not active)
    /// </summary>
    /// <param name="skinName">Id of skin</param>
    public static void AddSkinAsStatisticOnPlayFab(string skinName)
    {
        if (!Account.LoggedIn)
            return;

        int credits = Account.credits;
        int realMoney = Account.realMoney;
        var request = new UpdatePlayerStatisticsRequest();
        request.Statistics = new List<StatisticUpdate>();
        var statCredits = new StatisticUpdate { StatisticName = "Credits", Value = credits };
        var statRealMoney = new StatisticUpdate { StatisticName = "RealMoney", Value = realMoney };
        var statSkin = new StatisticUpdate { StatisticName = "SKIN_" + skinName, Value = 1 };
        request.Statistics.Add(statCredits);
        request.Statistics.Add(statRealMoney);
        request.Statistics.Add(statSkin);
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnSetStatsSuccessful, OnSetStatsFailed);
    }

    // <summary>
    /// in Playfab, statistics consist of key and value
    /// key is the SKIN_ + SKIN_ID (SKIN_ is the prefix to distinguish the skin from items)
    /// State 1 => skin is bought
    /// State 2 => skin is bought and active
    /// </summary>
    /// <param name="skinName">Id of skin</param>
    public static void UpdateSelectedSkinOnPlayFab()
    {
        if (!Account.LoggedIn || Account.ActiveSkin== null)
            return;

        var request = new UpdatePlayerStatisticsRequest();
        request.Statistics = new List<StatisticUpdate>();

        foreach (string skinId in Account.skinIdList)
        {
            int state;

            if (Account.ActiveSkin.id.Equals(skinId))
            {
                state = 2;
            }
            else
            {
                state = 1;
            }

            var statSkin = new StatisticUpdate { StatisticName = "SKIN_" + skinId, Value = state };
            request.Statistics.Add(statSkin);
        }
        
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnSetStatsSuccessful, OnSetStatsFailed);
    }
    /// <summary>
    /// Speichert in der Datenbank ab, wann die letzte Belohnung war.
    /// </summary>
    public static void SetLastRewardDate()
    {
        if (!Account.LoggedIn)
            return;

        var time = DateTimeConverter.ToUnixTimeSeconds(Account.lastReward);

        var request = new UpdatePlayerStatisticsRequest();
        request.Statistics = new List<StatisticUpdate>();
        var statLoggedInDays = new StatisticUpdate { StatisticName = "LoggedInDaysInARow", Value = Account.daysLoggedInARow };
        var statLastReward = new StatisticUpdate { StatisticName = "LastRewardDate", Value = time };
        request.Statistics.Add(statLoggedInDays);
        request.Statistics.Add(statLastReward);
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnSetStatsSuccessful, OnSetStatsFailed);
    }

    /// <summary>
    /// Das Geld und Echtgeld wird in der Datenbankstatistik aktualisiert.
    /// </summary>
    public static void SetMoneyAmount()
    {
        if (!Account.LoggedIn)
            return;

        var request = new UpdatePlayerStatisticsRequest();
        request.Statistics = new List<StatisticUpdate>();
        var statIngameMoney = new StatisticUpdate { StatisticName = "Credits", Value = Account.credits };
        var statRealMoney = new StatisticUpdate { StatisticName = "RealMoney", Value = Account.realMoney };
        request.Statistics.Add(statIngameMoney);
        request.Statistics.Add(statRealMoney);
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnSetStatsSuccessful, OnSetStatsFailed);
    }

    /// <summary>
    /// This method Updates a specific statistic on PlayFab.
    /// </summary>
    /// <param name="statisticName">Name of statistic</param>
    /// <param name="statisticValue">Value of statistic</param>
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

    /// <summary>
    /// This Method updates multiple statistics on PlayFab.
    /// </summary>
    /// <param name="statistics">Dictionary of statisticname and its value</param>
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
        //Debug.Log("Stats are successfully updated");
    }

    // is called when UpdatePlayerStatistics fails
    private static void OnSetStatsFailed(PlayFabError obj)
    {
        Debug.Log("Stats cannot be updated");
        Debug.Log(obj.Error);

    }
}
