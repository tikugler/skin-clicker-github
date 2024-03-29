using System;
using System.Collections.Generic;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Account class - contains important informations about player.
/// </summary>
public static class Account
{
    public static string guestCustomID; // only for guests
    public static string accountId;
    public static string accountName;
    public static int selectedPictureId = 0;
    public static DateTime lastReward = DateTime.MinValue;
    public static int daysLoggedInARow = 1;
    public static int points;
    public static int realMoney;
    public static int credits;
    public static int LeavingGameTimestamp;
    public static long inGameCurrency;
    public static List<SkinEffect> skinList = new List<SkinEffect>();
    public static List<string> skinIdList = new List<string>();
    public static Dictionary<string, int> upgradeList = new Dictionary<string, int>();  //Maybe enum instead of string soon
    public static string activeSkinId ="";
    private static SkinEffect activeSkin;
    public static SkinEffect ActiveSkin {
        get { return activeSkin; }
        set { activeSkin = value; activeSkinId = value.id;  PlayfabUpdateUserData.UpdateSelectedSkinOnPlayFab(); }
    }
    public static bool LoggedIn { get { return accountId != null; } }
    public static List<FriendInfo> friendsList = new List<FriendInfo>();
    public static List<string> earnedAchievements = new List<string>();
    private static List<string> usedCoupons  = new List<string>();
    public static List<string> UsedCoupons { get { return usedCoupons; } }

    public static Action<int> ChangeProfilPicture = delegate { };
    public static Action<string> ChangeAccountNameText = delegate { };


    /// <summary>
    /// If the Player logs in, the saved data gets dragged frrom the database.
    /// </summary>
    public static void OnClickGetAccountData()
    {

    }

    /// <summary>
    /// If the player exits the game the data gets saved to the database.
    /// </summary>
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
                    LoadEarnedAchievements(); //temp methode für achievements kann geloscht werden spater
                    break;
                case "LeavingGameTime":
                    Account.LeavingGameTimestamp = stat.Value;
                    break;
                case "LoggedInDaysInARow":
                    Account.daysLoggedInARow = stat.Value;
                    break;
                case "LastRewardDate":
                    Account.lastReward = DateTimeConverter.UnixTimeStampToDateTime(stat.Value);
                    break;
                case "SelectedPictureId":
                    Account.selectedPictureId = stat.Value;
                    break;
                case string skin when skin.StartsWith("SKIN_"):
                    AddSkinId(skin, stat.Value);
                    break;
                case string coupon when coupon.StartsWith("USED_"):
                    usedCoupons.Add(coupon.Substring(5));
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

    private static void LoadEarnedAchievements()
    {
        if (Account.credits >= 10)
        {
            earnedAchievements.Add(AchievementIdentifier.Achieve10Points);
        }
        if (Account.credits >= 500)
        {
            earnedAchievements.Add(AchievementIdentifier.Achieve500Points);
        }
        if (Account.credits >= 5000)
        {
            earnedAchievements.Add(AchievementIdentifier.Achieve5000Points);
        }
        if (Account.credits >= 50000)
        {
            earnedAchievements.Add(AchievementIdentifier.Achieve50000Points);
        }
        if (Account.credits >= 500000)
        {
            earnedAchievements.Add(AchievementIdentifier.Achieve500000Points);
        }
        if (Account.credits >= 1000000)
        {
            earnedAchievements.Add(AchievementIdentifier.Achieve1000000Points);
        }
        if (Account.credits >= 5000000)
        {
            earnedAchievements.Add(AchievementIdentifier.Achieve5000000Points);
        }
        if (Account.credits >= 10000000)
        {
            earnedAchievements.Add(AchievementIdentifier.Achieve10000000Points);
        }
    }

    /// <summary>
    /// Sets the Username and Password of an Account in the Preferences
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    public static void SetUserLoginPlayerPrefs(string username, string password)
    {
        PlayerPrefs.SetString("username", username);
        PlayerPrefs.SetString("password", password);
        CleanGuestCustomIdPlayerPrefs();
    }
    /// <summary>
    /// Check, if player is new.
    /// </summary>
    /// <returns>True, if player is new</returns>
    public static bool IsNewPlayer()
    {
        return PlayerPrefs.GetInt("newPlayer", 1) == 0 ? false : true;
    }

    /// <summary>
    /// Sets the value, whether a player is new or not.
    /// </summary>
    /// <param name="value">1 if player is new, 0 if not</param>
    public static void SetNewPlayer(int value)
    {
        PlayerPrefs.SetInt("newPlayer", value);
    }

    /// <summary>
    /// Gest Username of an Account.
    /// </summary>
    /// <returns>Username of Account</returns>
    public static string GetUsernamePlayerPrefs()
    {
        return PlayerPrefs.GetString("username");
    }

    /// <summary>
    /// Gets the Password of an Account.
    /// </summary>
    /// <returns>Password of Account</returns>
    public static string GetPasswordPlayerPrefs()
    {
        return PlayerPrefs.GetString("password");
    }

    /// <summary>
    /// Check, if an Account exists.
    /// </summary>
    /// <returns>True, if Account exists</returns>
    public static bool GetIfThereIsSavedUserLoginInfoPlayerPrefs()
    {
        //Debug.Log("PlayerPrefs username: " + PlayerPrefs.GetString("username"));
        return PlayerPrefs.HasKey("username") && PlayerPrefs.HasKey("password");
    }

    private static void CleanUserLoginPlayerPrefs()
    {
        PlayerPrefs.DeleteKey("username");
        PlayerPrefs.DeleteKey("password");
    }

    /// <summary>
    /// Saves ID of a Guest Account.
    /// </summary>
    /// <param name="guestCustomID">ID of the Guest Account</param>
    public static void SetGuestCustomIdPlayerPrefs(string guestCustomID)
    {
        PlayerPrefs.SetString("guestCustomID", guestCustomID);
        CleanUserLoginPlayerPrefs();

    }

    /// <summary>
    /// Get the Guest ID.
    /// </summary>
    /// <returns>Guest ID</returns>
    public static string GetGuestCustomIdPlayerPrefs()
    {
        return PlayerPrefs.GetString("guestCustomID");
    }

    /// <summary>
    /// Check, if a Guest ID exists.
    /// </summary>
    /// <returns>True, if guest ID exists</returns>
    public static bool GetIfThereIsSavedGuestCustomIdPlayerPrefs()
    {
        return PlayerPrefs.HasKey("guestCustomID");
    }

    private static void CleanGuestCustomIdPlayerPrefs()
    {
        PlayerPrefs.DeleteKey("guestCustomID");
    }

    /// <summary>
    /// This Method makes sure, that all Account data is reset,
    /// so that a new Account or new Guest Account doesn't have
    /// Credits or Skins etc.
    /// </summary>
    public static void LogOutUser()
    {
        CleanGuestCustomIdPlayerPrefs();
        CleanUserLoginPlayerPrefs();
        ChangeAccountNameText?.Invoke("No user logged in");
        selectedPictureId = 0;
        ChangeProfilPicture?.Invoke(selectedPictureId);
        accountId = null;
        accountName = null;
        upgradeList = new Dictionary<string, int>();
        friendsList = new List<FriendInfo>();
        earnedAchievements = new List<string>();
        activeSkin = null;
        skinIdList = new List<string>();
        skinList = new List<SkinEffect>();
        LeavingGameTimestamp = 0;
        credits = 0;
        points = 0;
    }

    /// <summary>
    /// add the skinId into skinIdList
    /// In Playfab, each statistic has a key (string) and a value (int)
    /// After purchase, skin is saved with the key SKIN_ (prefix) + SKIN_ID and the state 1.
    /// The state of active skin is 2
    /// </summary>
    /// <param name="skin">this is always (prefix to distinguish) SKIN_ + SKIN_ID</param>
    /// <param name="state">
    /// state 1 => purchased skin
    /// state 2 => purchased and active skin
    /// </param>
    private static void AddSkinId(string skin, int state)
    {
        string skinId = skin.Substring(5);

        if (state == 1)
        {
            skinIdList.Add(skinId);
        }
        else if(state == 2)
        {
            skinIdList.Add(skinId);
            activeSkinId = skinId;
            
        }
    }

    /// <summary>
    /// Checks if the given skin/id of skin exists in current account.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Return true, if given id matches id in Account.skinIdList.</returns>
    public static bool IsSkinIdInSkinIdList(string id)
    {
        foreach (string skin in Account.skinIdList)
        {
            if (skin.Equals(id))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Checks if the given skin/id of skin exists in current account.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Return true, if given id matches id in Account.skinList.</returns>
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

    /// <summary>
    /// Updates an Upgrade by a specific Upgrade Amount.
    /// </summary>
    /// <param name="upgradeName">Name of the upgrade</param>
    /// <param name="upgradeAmount">Amount of the upgrade</param>
    public static void SetPurchasedItemCount(string upgradeName, int upgradeAmount)
    {
        upgradeList[upgradeName] = upgradeAmount;
        PlayfabUpdateUserData.SetUpgradeAmountOnPlayFab(upgradeName, upgradeAmount);
    }

    /// <summary>
    /// Adds skinsId to skinIdList, if id is unknown to list.
    /// </summary>
    /// <param name="skinId"></param>
    public static void AddSkin(string skinId)
    {
        if (!skinIdList.Contains(skinId))
        {
            skinIdList.Add(skinId);
            PlayfabUpdateUserData.AddSkinAsStatisticOnPlayFab(skinId);
        }
    }
}
