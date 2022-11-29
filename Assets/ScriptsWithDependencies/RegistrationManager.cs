using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class RegistrationManager : MonoBehaviour
{

    [Header("Input Fields")]
    public InputField UsernameField;
    public InputField PasswordField;
    public InputField PasswordConfirmField;
    public InputField EmailField;

    [Header("Buttons")]
    public Button CloseRegistrationPanelButton;
    public Button SubmitButton;

    [Header("Text")]
    public Text InfoText;

    [Header("Panel")]
    public GameObject RegistrationPanel;



    private bool isUsernameValid = false;
    private bool isPasswordValid = false;
    private bool isEmailValid = false;
    private bool IsPasswordConfirmed = false;

    private Regex userRegex, passwordRegex, emailRegex;


    /// <summary>
    /// defines some regex patterns for username, password and email
    /// </summary>
    void Start()
    {
        emailRegex = new Regex("^(?!.{51})([a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)$");
        userRegex = new Regex("^.{3,12}$");
        passwordRegex = new Regex("^.{6,20}$");
    }


    /// <summary>
    /// verifies if all inputs are valid.
    /// If this is true, then Submitbutton is going to be interactable,
    /// otherwise not
    /// </summary>
    private void VerifyAll()
    {
        if (!isUsernameValid)
            InfoText.text = "Benutzername muss zwischen 3-12 Zeichen lang sein";
        else if (!isPasswordValid)
            InfoText.text = "Password muss zwischen 6-20 Zeichen lang sein";
        else if (!IsPasswordConfirmed)
            InfoText.text = "Passwörter sind nicht identisch";
        else if (!isEmailValid)
            InfoText.text = "Email ist ungültig";
        else
            SubmitButton.interactable = true;
    }

    /// <summary>
    /// verifies if player gave a valid password
    /// valid passwords must be 6 to 20 characters long
    /// </summary>
    public void VerifyPasswordOnValueChanged(string passwordInput)
    {

        isPasswordValid = passwordRegex.IsMatch(passwordInput);
        if (isPasswordValid)
        {
            //InfoText.text = "";
            CheckPasswordsAreSameAfterTypingOnFirstPassField(passwordInput);
        }
        else
        {
            InfoText.text = "Password muss zwischen 6-20 Zeichen lang sein";
            SubmitButton.interactable = false;
        }
    }

    /// <summary>
    /// verifies if user gave a valid username
    /// valid username must be 3 to 12 characters long
    /// </summary>
    public void VerifyUsernameOnValueChanged(string usernameInput)
    {

        isUsernameValid = userRegex.IsMatch(usernameInput);
        if (isUsernameValid)
        {
            InfoText.text = "";
            VerifyAll();
        }
        else
        {
            InfoText.text = "Benutzername muss zwischen 3-20 Zeichen lang sein";
            SubmitButton.interactable = false;
        }
    }

    /// <summary>
    /// called after user tipped in the first password field.
    /// If the given password is valid, it will be compared to the second one
    /// which confirms whether the user entered the same password twice
    /// </summary>
    /// <param name="passwordInput">first input field for password</param>
    public void CheckPasswordsAreSameAfterTypingOnFirstPassField(string passwordInput)
    {
        IsPasswordConfirmed = PasswordConfirmField.text == passwordInput;
        if (IsPasswordConfirmed)
        {
            InfoText.text = "";
            VerifyAll();
            return;
        }
        InfoText.text = "Passwörter sind nicht identisch";
        SubmitButton.interactable = false;
    }

    /// <summary>
    /// confirms if the player gave the same password again
    /// </summary>
    public void ConfirmPasswordOnValueChanged(string confirmPasswordInput)
    {
        IsPasswordConfirmed = PasswordField.text == confirmPasswordInput;
        if (IsPasswordConfirmed)
        {
            InfoText.text = "";
            VerifyAll();
        }
        else
        {
            InfoText.text = "Passwörter sind nicht identisch";
            SubmitButton.interactable = false;
        }
    }


    /// <summary>
    /// verifies if the player gave a valid email address
    /// </summary>
    public void VerifyEmailOnValueChanged(string emailInput)
    {
        isEmailValid = emailRegex.IsMatch(emailInput);
        if (isEmailValid)
        {    
            InfoText.text = "";
            VerifyAll();
        }
        else
        {
            InfoText.text = "Email ist ungültig";
            SubmitButton.interactable = false;
        }
    }

    public void CloseRegistrationPanel()
    {
        RegistrationPanel.SetActive(false);
        UsernameField.text = "";
        PasswordField.text = "";
        PasswordConfirmField.text = "";
        EmailField.text = "";
        InfoText.text = "";
    }

    public void OpenRegistrationPanel()
    {
        RegistrationPanel.SetActive(true);

    }

    public void CallSubmitInRegistration()
    {
        Debug.Log("Submit...");
        SubmitButton.interactable = false;
        RegisterUserOnPlayFab();
    }

    private void RegisterUserOnPlayFab()
    {
        var request = new RegisterPlayFabUserRequest();
        request.TitleId = PlayFabSettings.TitleId;
        request.Email = EmailField.text;
        request.Username = UsernameField.text;
        request.Password = PasswordField.text;

        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailed);
    }

    private void OnRegisterFailed(PlayFabError obj)
    {
        Debug.Log("registration has failed");
        InfoText.text = "Error: " + obj.Error;
        SubmitButton.interactable = true;
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult obj)
    {
        Debug.Log("registration is successful");
        PlayerInfo.username = obj.Username;
        PlayerInfo.score = 0;
        SceneManager.LoadScene("StartNewsMenu");
    }
}