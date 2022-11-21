using System.Collections;
using System.Collections.Generic;
using PlayFab.ClientModels;
using UnityEngine;

public static class Account
{
    public static string accountId;
    public static string accountName;
    public static int credits;
    public static long inGameCurrency;
    public static List<string> skinList;
    public static Dictionary<string, int> upgradeList = new Dictionary<string, int>();  //Maybe enum instead of string soon
    public static string activeSkin;
    public static bool LoggedIn { get { return accountId != null; } }

    // Skin objekt? hat id, wert, image, boolean ausgew�hlt 
    // account objekt serializable?

    //If the Player logs in, the saved data gets dragged frrom the database.
    public static void OnClickGetAccountData()
    {

    }

    //If the player exits the game the data gets saved to the database.
    public static void OnClickSaveAccountData()
    {

    }

    /// <summary>
    /// this method sets the accountId, accountName
    /// </summary>
    /// <param name="playFabId">accountId</param>
    /// <param name="username">accountName</param>
    public static void SetPlayFabIdAndUserName(string playFabId, string username)
    {
        accountId = playFabId;
        accountName = username;

    }


    /// <summary>
    /// this method sets the statistics such as credits und upgrades
    /// </summary>
    /// <param name="statistics">credits or number of performed upgrade</param>
    public static void SetStatistics(List<StatisticValue> statistics)
    {

        foreach (var stat in statistics)
        {
            Debug.Log("Statistic: " + stat.StatisticName + ", Wert: " + stat.Value);
            switch (stat.StatisticName)
            {
                case "Credits":
                    Account.credits = stat.Value;
                    break;
                default:
                    upgradeList.Add(stat.StatisticName, stat.Value);
                    break;
            }
        }

        foreach(string item in upgradeList.Keys)
        {
            Debug.Log(item + ": " + upgradeList[item]);
        }
    }
}
