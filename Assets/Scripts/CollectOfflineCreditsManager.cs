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
        Debug.Log("*****************StartCollectOfflineCreditsManagerStatic");
        Debug.Log("Worker.creditsPerSec"+  Worker.workerAmount);
        Debug.Log("Account.LeavingGameTimestamp: " + Account.LeavingGameTimestamp);
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


    public void CalculatePassedSeconds()
    {
        Debug.Log("**********************CalculatePassedSeconds");

        passedSeconds = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds - Account.LeavingGameTimestamp;
        passedSecondsText.text = passedSeconds.ToString() + " Sekunde";
    }

    public void CalculateCollectedCredits()
    {
        Debug.Log("*********************CalculateCollectedCredits");

        collectedCredits = passedSeconds * Worker.workerAmount;
        collectedCreditsText.text = collectedCredits.ToString() + " Berkan Coin";
    }

    public void CloseCollectOfflinePanel()
    {
        Debug.Log("CloseCollectOfflinePanel");
      
        Account.credits += collectedCredits;
        isUserTakingOfflineCollection = false;
        GameObject.FindGameObjectWithTag("PlayFabUpdate").GetComponent<PlayfabUpdateUserData>().StartSetScoreOnPlayFabRepeating();
        collectOfflinePanel.SetActive(false);
    }



}
