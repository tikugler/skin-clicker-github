using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Account
{
    public static string accountId;
    public static string accountName;
    public static long points;
    public static long inGameCurrency;
    public static List<string> skinList;
    public static Dictionary<string, int> upgradeList;  //Maybe enum instead of string soon
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
}
