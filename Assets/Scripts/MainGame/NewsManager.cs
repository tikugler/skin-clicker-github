using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;

public class NewsManager : MonoBehaviour
{


    [SerializeField] private Button openNewsPanelButton = null;
    [SerializeField] private Button closeNewsPanelButton = null;

    [SerializeField] private Text newsBodyText = null;
    [SerializeField] private Text newsTitleText = null;



    [SerializeField] private GameObject newsPanel = null;
    //[SerializeField] Button newsOpenerButton;


    private bool isPanelOpen = false;

    private void Awake()
    {
        if (!Account.LoggedIn)
        {
            openNewsPanelButton.interactable = false;

        }


    }

    private void OnEnable()
    {

        openNewsPanelButton.onClick.AddListener(OpenNewsPanel);
        closeNewsPanelButton.onClick.AddListener(CloseNewsPanel);
    }

    private void OnDisable()
    {
        openNewsPanelButton.onClick.RemoveListener(OpenNewsPanel);
        closeNewsPanelButton.onClick.RemoveListener(CloseNewsPanel);

    }


    void OpenNewsPanel()
    {
        if (isPanelOpen)
            return;


        ReadTitleNewsAndOpenPanel();

        
    }

    void CloseNewsPanel()
    {
        isPanelOpen = false;
        newsPanel.SetActive(false);
    }


    void ReadTitleNewsAndOpenPanel()
    {
        PlayFabClientAPI.GetTitleNews(new GetTitleNewsRequest(),
            GetNewsSuccess, error => Debug.LogError(error.GenerateErrorReport()));
    }

    private void GetNewsSuccess(GetTitleNewsResult obj)
    {

        newsTitleText.text = obj.News[0].Title;
        newsBodyText.text = obj.News[0].Body;

        isPanelOpen = false;
        newsPanel.SetActive(true);


    }
}
