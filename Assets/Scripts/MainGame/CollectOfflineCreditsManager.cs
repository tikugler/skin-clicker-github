using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectOfflineCreditsManager : MonoBehaviour
{

    public TMP_Text passedSecondsText;
    public TMP_Text collectedCreditsText;
    public GameObject collectOfflinePanel;

    int passedSeconds;
    int collectedCredits;

    public static bool isUserTakingOfflineCollection = false;

    public static void StartCollectOfflineCreditsManagerStatic()
    {
        if (Account.LoggedIn && Worker.workerAmount != 0 && Account.LeavingGameTimestamp != 0)
            GameObject.Find("CollectOfflineCreditsArea").GetComponent<CollectOfflineCreditsManager>().StartCollectOfflineCreditsManager();
        else
            GameObject.FindGameObjectWithTag("PlayFabUpdate").GetComponent<PlayfabUpdateUserData>().StartSetScoreOnPlayFabRepeating();

    }

    public void StartCollectOfflineCreditsManager()
    {
        isUserTakingOfflineCollection = true;
        CalculatePassedSeconds();
        CalculateCollectedCredits();
        collectOfflinePanel.SetActive(true);
    }

    /// <summary>
    /// calculates the elapsed time in seconds since you last exited the game
    /// </summary>
    public void CalculatePassedSeconds()
    {
        passedSeconds = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds - Account.LeavingGameTimestamp;
        passedSecondsText.text = passedSeconds.ToString() + " Sekunden";
    }

    public void CalculateCollectedCredits()
    {
        collectedCredits = passedSeconds * Worker.workerAmount;
        collectedCreditsText.text = collectedCredits.ToString() + " Berkan Coin";
    }

    public void CloseCollectOfflinePanel()
    {
      
        Account.credits += collectedCredits;
        isUserTakingOfflineCollection = false;
        GameObject.FindGameObjectWithTag("PlayFabUpdate").GetComponent<PlayfabUpdateUserData>().StartSetScoreOnPlayFabRepeating();
        collectOfflinePanel.SetActive(false);
    }



}
