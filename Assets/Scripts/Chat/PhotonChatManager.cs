using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Chat;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System;
using PlayFab.ClientModels;


public class PhotonChatManager : MonoBehaviour, IChatClientListener
{


    [SerializeField] Button chatOpenerButton;
    ChatClient chatClient;
    bool isConnected;
    [SerializeField] string username; //= Account.accountName;
    [SerializeField] TMP_Dropdown messageReceiverDropDown;

    
    public static Action<string, int> OnFriendStatusUpdate = delegate { };


    /// <summary>
    /// establish a connection with photon chat server using Photon Chat App ID
    /// </summary>
    public void ChatConnectOnClick()
    {
        isConnected = true;
        chatClient = new ChatClient(this);
        chatClient.ConnectAndSetStatus("2156777f-aabb-4176-a2a2-e55b553b8289", "1.0.0", new AuthenticationValues(Account.accountName));
    }

    // All debug output of the library will be reported through this method.Print it or put it in a
    // A method from  IChatClientListener Interface
    public void DebugReturn(DebugLevel level, string message)
    {
        Debug.Log(message);
    }

    //The ChatClient's state changed. Usually, OnConnected and OnDisconnected are the callbacks to react to.
    // A method from  IChatClientListener Interface
    public void OnChatStateChange(ChatState state)
    {
        Debug.Log(state.ToString());
    }

    // called when client is connected
    // adds friends from Account.friendsList to PhotonChatFriend
    public void OnConnected()
    {
        chatClient.Subscribe(new string[] { "RegionChannel" });
        AddPhotonChatFriend(Account.friendsList.Select(f => f.TitleDisplayName).ToArray());
    }

    // called when user disconnected
    // A method from IChatClientListener Interface
    public void OnDisconnected()
    {
    }

    // Notifies app that client got new messages from server
    // updates the chat text by adding the received new global message
    // A method from  IChatClientListener Interface
    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        string msgs = "";
        for (int i = 0; i < senders.Length; i++)
        {
            msgs = string.Format("{0}: {1}", senders[i], messages[i]);
            chatDisplay.text += "\n " + msgs;
        }
    }

    // Notifies client about private message
    // updates the chat text by adding the received new private message
    // A method from  IChatClientListener Interface
    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        string msgs = "";
        msgs = string.Format("(Private) {0}: {1}", sender, message);
        chatDisplay.text += "\n " + msgs;
    }

    // called when friend changed status
    // this methods add or removed friend from the dropdown menu in chat according to his/her new status
    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        FriendInfo friendInfo = findFriendInfoByUsername(user);
        OnFriendStatusUpdate?.Invoke(user, status);

        if(status == 0)
            RemoveItemFromChatDropDown(user);
        else if(status == 2)
            AddItemToChatDropDown(user);
    }

    // A method from  IChatClientListener Interface
    public void OnSubscribed(string[] channels, bool[] results)
    {
        Debug.Log("subribed");
    }

    // A method from  IChatClientListener Interface
    public void OnUnsubscribed(string[] channels)
    {
        Debug.Log("unsubribed");
    }

    // A method from  IChatClientListener Interface
    // called when the user is unsubscribed to the channel
    public void OnUserSubscribed(string channel, string user)
    {
        Debug.Log("user subribed");
    }

    // A method from  IChatClientListener Interface
    // called when the user is subscribed to the channel
    public void OnUserUnsubscribed(string channel, string user)
    {
        Debug.Log("user unsubribed");
    }


    private FriendInfo findFriendInfoByUsername(string chatDisplayName)
    {
        FriendInfo item = Account.friendsList.First(friend => friend.TitleDisplayName == chatDisplayName);
        return item;
    }

    [SerializeField] GameObject chatPanel;
    string currentChat;
    string privateReceiver = "";
    public TMP_InputField chatField;
    [SerializeField] TMP_Text chatDisplay;

    [SerializeField] string selectedReceiverToSendMessage;


    private void Awake()
    {

        if (!Account.LoggedIn || Account.accountName.Equals("Guest"))
        {
            chatOpenerButton.interactable = false;

        }
            
        else
        {
            ChatConnectOnClick();

        }

        PlayFabFriendManager.OnFriendsAddToPhoton += AddPhotonChatFriend;
        PlayFabFriendManager.OnFriendsRemoveFromPhoton += RemovePhotonChatFriend;

        messageReceiverDropDown.onValueChanged.AddListener(delegate {
            DropdownItemSelected(messageReceiverDropDown);});

    }

    // called when new receiver in dropdown menu is selected
    // assign the name of selected menuitem to selectedReceiverToSendMessage
    void DropdownItemSelected(TMP_Dropdown dropdown)
    {
        int index = dropdown.value;
        selectedReceiverToSendMessage = dropdown.options[index].text;
    }

    // removes delegates and a listener
    private void OnDestroy()
    {
        PlayFabFriendManager.OnFriendsAddToPhoton -= AddPhotonChatFriend;
        PlayFabFriendManager.OnFriendsRemoveFromPhoton -= RemovePhotonChatFriend;
        messageReceiverDropDown.onValueChanged.RemoveAllListeners();
    }

    private void Start()
    {
        username = Account.accountName;
        messageReceiverDropDown.onValueChanged.Invoke(messageReceiverDropDown.value);
    }


    /// <summary>
    /// this method is called in each frame
    /// It sends the message when the user has typed something in the chat and pressed enter
    /// </summary>
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


    // sends public message if Global in DropDrown Menu is selected 
    private void SubmitPublicChatOnClick()
    {
        if(selectedReceiverToSendMessage.Equals("Global"))
        {
            chatClient.PublishMessage("RegionChannel", currentChat);
            chatField.text = "";
            currentChat = "";
        }
    }
    // assign the given value to currentChat,
    // which is sent when user press enter oder send button in UI
    public void TypeChatOnValueChange(string valueIn)
    {
        currentChat = valueIn;
    }


    // assign valueIn to privateReceiver
    public void ReceiverOnValueChange(string valueIn)
    {
        privateReceiver = valueIn;
    }


    /// <summary>
    /// sends a private message if something else than Global in Dropdown Menu is selected
    /// </summary>
    private void SubmitPrivateChatOnClick()
    {
        if (!selectedReceiverToSendMessage.Equals("Global"))
        {
            chatClient.SendPrivateMessage(selectedReceiverToSendMessage, currentChat);
            chatField.text = "";
            currentChat = "";
        }
    }

    /// <summary>
    /// opens chat
    /// </summary>
    public void CallOpenChat()
    {
        chatOpenerButton.gameObject.SetActive(false);
        chatPanel.SetActive(true);
    }

    /// <summary>
    /// closes chat panel and make visible the button to open chat again
    /// </summary>
    public void CloseChatPanel()
    {

        chatPanel.SetActive(false);
        chatOpenerButton.gameObject.SetActive(true);

    }

    /// <summary>
    /// adds friend to Photon Chat to get changes in their status
    /// </summary>
    /// <param name="friendsToAdd">Username of friends as a list of strings</param>
    private void AddPhotonChatFriend(string[] friendsToAdd)
    {
        if (friendsToAdd.Length != 0)
        {
            chatClient.AddFriends(friendsToAdd);
        }
    }

    /// <summary>
    /// removes friend from Photon Chat and DropDown Menu
    /// </summary>
    /// <param name="friendsToRemove">Username of friends as a list of strings to remove</param>
    public void RemovePhotonChatFriend(string[] friendsToRemove)
    {
        if (friendsToRemove.Length != 0)
        {
            chatClient.RemoveFriends(friendsToRemove);

            foreach (string friendDisplayName in friendsToRemove)
            {
                RemoveItemFromChatDropDown(friendDisplayName);
            }
        }
    }

    private void AddItemToChatDropDown(string receiver)
    {
        messageReceiverDropDown.options.Add(new TMP_Dropdown.OptionData() { text = receiver });
    }
    /// <summary>
    /// removes menu item from dropdown according to given receiver string
    /// </summary>
    /// <param name="receiver">username of the player to remove from dropdown menu</param>
    private void RemoveItemFromChatDropDown(string receiver)
    {
        messageReceiverDropDown.options.Remove(new TMP_Dropdown.OptionData() { text = receiver });

        for (int x = 0; x < messageReceiverDropDown.options.Count; x++)
        {
            if (messageReceiverDropDown.options[x].text == receiver)
            {
                messageReceiverDropDown.options.RemoveAt(x);
                break;
            }
        }
        
    }  
}
