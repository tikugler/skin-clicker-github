using System;
using System.Collections.Generic;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Account
{
    public static string guestCustomID; // only for guests
    public static string accountId;
    public static string accountName;
    public static int selectedPictureId = 0;
    public static int points;
    public static int realMoney;
    public static int credits;
    public static int LeavingGameTimestamp;
    public static long inGameCurrency;
    public static List<SkinEffect> skinList = new List<SkinEffect>();
    public static Dictionary<string, int> upgradeList = new Dictionary<string, int>();  //Maybe enum instead of string soon
    public static SkinEffect activeSkin;
    public static bool LoggedIn { get { return accountId != null; } }
    public static List<FriendInfo> friendsList = new List<FriendInfo>();


    public static Action<int> ChangeProfilPicture = delegate { };
    public static Action<string> ChangeAccountNameText = delegate { };



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
    /// this method sets the accountId, accountName
    /// </summary>
    /// <param name="playFabId">accountId</param>
    /// <param name="username">accountName</param>
    /// <param name="updateUserNameText">determines if the username text at the left upper corner is updated</param>
    public static void SetPlayFabIdAndUserName(string playFabId, string username, bool updateUserNameText)
    {
        accountId = playFabId;
        accountName = username;

        if(updateUserNameText)
            ChangeAccountNameText?.Invoke(username);
    }


    /// <summary>
    /// this method sets the statistics such as credits und upgrades
    /// </summary>
    /// <param name="statistics">credits or number of performed upgrade</param>
    public static void SetStatistics(List<StatisticValue> statistics)
    {

        foreach (var stat in statistics)
        {
            //Debug.Log("Statistic: " + stat.StatisticName + ", Wert: " + stat.Value);
            switch (stat.StatisticName)
            {
                case "Credits":
                    Account.credits = stat.Value;
                    break;
                case "LeavingGameTime":
                    Account.LeavingGameTimestamp = stat.Value;
                    break;
                case "SelectedPictureId":
                    Account.selectedPictureId = stat.Value;
                    break;
                default:
                    upgradeList.Add(stat.StatisticName, stat.Value);
                    break;
            }
        }

        //foreach (string item in upgradeList.Keys)
        //{
        //    Debug.Log(item + ": " + upgradeList[item]);
        //}
        ChangeProfilPicture?.Invoke(selectedPictureId);
        ChangeAccountNameText?.Invoke(accountName);
        SceneManager.LoadScene("MainGame");

    }


    public static void SetUserLoginPlayerPrefs(string username, string password)
    {
        PlayerPrefs.SetString("username", username);
        PlayerPrefs.SetString("password", password);
        CleanGuestCustomIdPlayerPrefs();

    }
    public static bool IsNewPlayer()
    {
        return PlayerPrefs.GetInt("newPlayer", 1) == 0 ? false : true;
    }

    public static void SetNewPlayer(int value)
    {
        PlayerPrefs.SetInt("newPlayer", value);
    }
    public static string GetUsernamePlayerPrefs()
    {
        return PlayerPrefs.GetString("username");
    }

    public static string GetPasswordPlayerPrefs()
    {
        return PlayerPrefs.GetString("password");
    }

    public static bool GetIfThereIsSavedUserLoginInfoPlayerPrefs()
    {
        Debug.Log("PlayerPrefs username: " + PlayerPrefs.GetString("username"));
        return PlayerPrefs.HasKey("username") && PlayerPrefs.HasKey("password");
    }

    private static void CleanUserLoginPlayerPrefs()
    {
        PlayerPrefs.DeleteKey("username");
        PlayerPrefs.DeleteKey("password");
    }

    public static void SetGuestCustomIdPlayerPrefs(string guestCustomID)
    {
        PlayerPrefs.SetString("guestCustomID", guestCustomID);
        CleanUserLoginPlayerPrefs();

    }

    public static string GetGuestCustomIdPlayerPrefs()
    {
        return PlayerPrefs.GetString("guestCustomID");
    }

    public static bool GetIfThereIsSavedGuestCustomIdPlayerPrefs()
    {
        return PlayerPrefs.HasKey("guestCustomID");
    }

    private static void CleanGuestCustomIdPlayerPrefs()
    {
        PlayerPrefs.DeleteKey("guestCustomID");
    }

    public static void LogOutUser()
    {
        CleanGuestCustomIdPlayerPrefs();
        CleanUserLoginPlayerPrefs();
        ChangeAccountNameText?.Invoke("No user logged in");
        ChangeProfilPicture?.Invoke(0);
        accountId = null;
        accountName = null;
        upgradeList = new Dictionary<string, int>();
        LeavingGameTimestamp = 0;
        credits = 0;
        points = 0;
    }

    public static bool IsSkinInInventory(string id)
    {
        foreach (SkinEffect skin in Account.skinList)
        {
            if (skin.id.ToString().Equals(id))
            {
                return true;
            }
        }
        return false;
    }
}
