using PlayFab.ClientModels;
using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void SendFeedbackToDatabase() {
        string feedback = "Das ist wieder mal ein Test";
        // Set up the request object
        WriteClientPlayerEventRequest request = new WriteClientPlayerEventRequest();
        request.EventName = "Feedback";
        request.Body = new Dictionary<string, object>
{
    {"Feedback", feedback}
};

        // Send the request to PlayFab
        PlayFabClientAPI.WritePlayerEvent(request, result => {
            Debug.Log("Feedback sent to developers!");
        }, error => {
            Debug.LogError("Error sending feedback to developers: " + error.GenerateErrorReport());
        });



    }
}
