using PlayFab.ClientModels;
using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FeedbackManager : MonoBehaviour

{
    public GameObject feedbackPanel;
    public GameObject feedbackTitle;
    public GameObject feedbackField;

    public void OpenFeedbackpanel()
    {
        feedbackPanel.SetActive(true);
    }
    public void CloseFeedbackpanel()
    {
        feedbackPanel.SetActive(false);
    }

    public void SendFeedbackToDatabase() {
        string titleText = feedbackTitle.GetComponent<TMP_InputField>().text;
        string feedbackText = feedbackField.GetComponent<TMP_InputField>().text;
        

        // Set up the request object
        WriteClientPlayerEventRequest request = new WriteClientPlayerEventRequest();
        request.EventName = "Feedback";
        request.Body = new Dictionary<string, object>
{
    {"Betreff" , titleText},
    {"Inhalt", feedbackText},
    
};

        // Send the request to PlayFab
        PlayFabClientAPI.WritePlayerEvent(request, result => {
            Debug.Log("Feedback sent to developers!");
            feedbackTitle.GetComponent<TMP_InputField>().text = "";
            feedbackField.GetComponent<TMP_InputField>().text = "";
            

            CloseFeedbackpanel();
        }, error => {
            Debug.LogError("Error sending feedback to developers: " + error.GenerateErrorReport());
        });
        

    }
}
