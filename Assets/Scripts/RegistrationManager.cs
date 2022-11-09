using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

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
        emailRegex = new Regex("^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$");
        userRegex = new Regex("^.{5,20}$");
        passwordRegex = new Regex("^.{5,20}$");
    }


    private void VerifyAll()
    {
        if (!isUsernameValid)
            InfoText.text = "Benutzername muss zwischen 5-20 Zeichen lang sein";
        else if (!isPasswordValid)
            InfoText.text = "Password muss zwischen 5-20 Zeichen lang sein";
        else if (!IsPasswordConfirmed)
            InfoText.text = "Passwörter sind nicht identisch";
        else if (!isEmailValid)
            InfoText.text = "Email ist ungültig";
        else
            SubmitButton.interactable = true;
    }

    /// <summary>
    /// verifies if player gave a valid password
    /// valid passwords must be 5 to 20 characters long
    /// </summary>
    public void VerifyPasswordOnValueChanged(string passwordInput)
    {

        isPasswordValid = passwordRegex.IsMatch(passwordInput);
        if (isPasswordValid)
        {
            InfoText.text = "";
            VerifyAll();
        }
        else
        {
            InfoText.text = "Password muss zwischen 5-20 Zeichen lang sein";
            SubmitButton.interactable = false;
        }
    }

    /// <summary>
    /// verifies if player gave a valid username
    /// valid username must be 5 to 20 characters long
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
            InfoText.text = "Benutzername muss zwischen 5-20 Zeichen lang sein";
            SubmitButton.interactable = false;
        }
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
    }

}