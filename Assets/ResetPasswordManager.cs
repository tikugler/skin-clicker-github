using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class ResetPasswordManager : MonoBehaviour
{

    [SerializeField] Button openResetPasswordPanelButton;
    [SerializeField] Button closeResetPasswordButton;
    [SerializeField] GameObject resetPasswordPanel;
    [SerializeField] InputField requestedEmailInputField;
    [SerializeField] Button resetPasswordRequestButton;
    [SerializeField] Text infoText;



    private void OnEnable()
    {
        openResetPasswordPanelButton.onClick.AddListener(OpenResetPasswordPanel);
        closeResetPasswordButton.onClick.AddListener(CloseResetPasswordPanel);
        resetPasswordRequestButton.onClick.AddListener(RequestPassword);
        requestedEmailInputField.onValueChanged.AddListener(MakeInteractableResetPasswordButton);

    }

    private void OnDisable()
    {
        openResetPasswordPanelButton.onClick.RemoveListener(OpenResetPasswordPanel);
        closeResetPasswordButton.onClick.RemoveListener(CloseResetPasswordPanel);
        resetPasswordRequestButton.onClick.AddListener(RequestPassword);
        requestedEmailInputField.onValueChanged.RemoveListener(MakeInteractableResetPasswordButton);


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OpenResetPasswordPanel()
    {
        resetPasswordPanel.active = true;
    }
    void CloseResetPasswordPanel()
    {
        resetPasswordPanel.active = false;
    }


    void RequestPassword()
    {
        string emailText = requestedEmailInputField.text;
        if(emailText != "")
        {
            resetPasswordRequestButton.interactable = false;
            infoText.text = "";
            var request = new SendAccountRecoveryEmailRequest();
            request.Email = emailText;
            request.TitleId = PlayFabSettings.TitleId;
            PlayFabClientAPI.SendAccountRecoveryEmail(request, OnRecoveryEmailSuccess, OnRecoveryEmailError);
        }
        else
        {
            infoText.text = "Geben Sie bitte Ihre E-Mail-Adresse ein";

        }
    }

    private void OnRecoveryEmailSuccess(SendAccountRecoveryEmailResult obj)
    {
        infoText.text = "Ein Link zum Zurücksetzen Ihres Passworts wurde an Ihre E-Mail-Adresse gesendet";
    }

    private void OnRecoveryEmailError(PlayFabError obj)
    {
        infoText.text = obj.GenerateErrorReport();
    }

    private void MakeInteractableResetPasswordButton(string newPasswordText)
    {
        if(newPasswordText != "")
        {
            resetPasswordRequestButton.interactable = true;

        }
        else
        {
            resetPasswordRequestButton.interactable = false;

        }
    }
}
