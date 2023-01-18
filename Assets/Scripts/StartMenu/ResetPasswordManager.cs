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


    /// <summary>
    /// adds listeners to GameObjects
    /// </summary>
    private void OnEnable()
    {
        openResetPasswordPanelButton.onClick.AddListener(OpenResetPasswordPanel);
        closeResetPasswordButton.onClick.AddListener(CloseResetPasswordPanel);
        resetPasswordRequestButton.onClick.AddListener(RequestPassword);
        requestedEmailInputField.onValueChanged.AddListener(MakeInteractableResetPasswordButton);

    }

    /// <summary>
    /// removes listeners from GameObjects
    /// </summary>
    private void OnDisable()
    {
        openResetPasswordPanelButton.onClick.RemoveListener(OpenResetPasswordPanel);
        closeResetPasswordButton.onClick.RemoveListener(CloseResetPasswordPanel);
        resetPasswordRequestButton.onClick.AddListener(RequestPassword);
        requestedEmailInputField.onValueChanged.RemoveListener(MakeInteractableResetPasswordButton);
    }

  
    /// <summary>
    /// opens Panel to reset password
    /// </summary>
    void OpenResetPasswordPanel()
    {
        resetPasswordPanel.active = true;
    }

    /// <summary>
    /// cleans input fiends in panel and closes it
    /// </summary>
    void CloseResetPasswordPanel()
    {
        resetPasswordRequestButton.interactable = false;
        requestedEmailInputField.text = "";
        infoText.text = "";
        resetPasswordPanel.active = false;
    }

    /// <summary>
    /// If infoText contains any string than blank,
    /// then it send and SendAccountRecoveryEmail request to Playfab
    /// to get a link per email to reset the password
    /// </summary>
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj">contains success result</param>
    private void OnRecoveryEmailSuccess(SendAccountRecoveryEmailResult obj)
    {
        infoText.text = "Ein Link zum Zurücksetzen Ihres Passworts wurde an Ihre E-Mail-Adresse gesendet";
    }
    /// <summary>
    /// print the error in the infoText in panel
    /// </summary>
    /// <param name="obj">contains error details</param>
    private void OnRecoveryEmailError(PlayFabError obj)
    {
        infoText.text = obj.GenerateErrorReport();
    }

    private void MakeInteractableResetPasswordButton(string newEmailText)
    {
        if(newEmailText != "")
        {
            resetPasswordRequestButton.interactable = true;

        }
        else
        {
            resetPasswordRequestButton.interactable = false;
        }
    }
}
