using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Chat;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhotonChatManager : MonoBehaviour, IChatClientListener
{


    [SerializeField] Button chatOpenerButton;
    ChatClient chatClient;
    bool isConnected;
    [SerializeField] string username; //= Account.accountName;

    

    // Start is called before the first frame update
    public void ChatConnectOnClick()
    {
        isConnected = true;
        chatClient = new ChatClient(this);

        chatClient.Connect("2156777f-aabb-4176-a2a2-e55b553b8289", "1.0.0", new AuthenticationValues(username));
        Debug.Log("Connecting");
    }

    public void DebugReturn(DebugLevel level, string message)
    {
        //throw new System.NotImplementedException();
        Debug.Log("level: " + level);
        Debug.Log("message: " + message);

    }

    public void OnChatStateChange(ChatState state)
    {
        //throw new System.NotImplementedException();
        Debug.Log("New state: " + state.ToString());
    }

    public void OnConnected()
    {
        //throw new System.NotImplementedException();
        Debug.Log("Connected");
        chatClient.Subscribe(new string[] { "RegionChannel" });
    }

    public void OnDisconnected()
    {
        throw new System.NotImplementedException();
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        //throw new System.NotImplementedException();
        Debug.Log("----- OnGetMessage is working");
        string msgs = "";
        for (int i = 0; i < senders.Length; i++)
        {
            msgs = string.Format("{0}: {1}", senders[i], messages[i]);

            chatDisplay.text += "\n " + msgs;

            //chatDisplay.text.

            Debug.Log("Yeni chat display: " + chatDisplay.text);

            Debug.Log(msgs);
        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        //throw new System.NotImplementedException();

        string msgs = "";
        msgs = string.Format("(Private) {0}: {1}", sender, message);
        chatDisplay.text += "\n " + msgs;
        Debug.Log(msgs);
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        throw new System.NotImplementedException();
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        Debug.Log("You successfully subscribet to the chat");
        //throw new System.NotImplementedException();
    }

    public void OnUnsubscribed(string[] channels)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserSubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }


    [SerializeField] GameObject chatPanel;
    string currentChat;
    string privateReceiver = "";
    public TMP_InputField chatField;
    [SerializeField] TMP_Text chatDisplay;


    private void Awake()
    {

        if (!Account.LoggedIn)
            chatOpenerButton.interactable = false;
    }

    private void Start()
    {
        username = Account.accountName;
        Debug.Log("Account name is: " + username);
        Debug.Log(Account.accountId);
        Debug.Log(Account.credits);
        

        ChatConnectOnClick();

    }



    void Update()
    {
        if (isConnected)
        {
            chatClient.Service();
        }

        if(chatField.text != "" && Input.GetKey(KeyCode.Return))
        {
            SubmitPublicChatOnClick();
            SubmitPrivateChatOnClick();
        }
    }


    

    public void SubmitPublicChatOnClick()
    {
        if(privateReceiver == "")
        {
            Debug.Log("called public submit chat");
            chatClient.PublishMessage("RegionChannel", currentChat);
            chatField.text = "";
            currentChat = "";
        }
    }

    public void TypeChatOnValueChange(string valueIn)
    {
        currentChat = valueIn;
    }







    public void ReceiverOnValueChange(string valueIn)
    {
        privateReceiver = valueIn;
    }



    public void SubmitPrivateChatOnClick()
    {
        if (privateReceiver != "")
        {
            chatClient.SendPrivateMessage(privateReceiver, currentChat);
            chatField.text = "";
            currentChat = "";
        }
    }




    public void CallOpenChat()
    {
        chatPanel.SetActive(true);
    }

    public void CloseChatPanel()
    {
        chatPanel.SetActive(false);
    }
}
