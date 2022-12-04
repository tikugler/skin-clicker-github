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
using static System.Net.Mime.MediaTypeNames;

public class PhotonChatManager : MonoBehaviour, IChatClientListener
{


    [SerializeField] Button chatOpenerButton;
    ChatClient chatClient;
    bool isConnected;
    [SerializeField] string username; //= Account.accountName;
    [SerializeField] TMP_Dropdown messageReceiverDropDown;

    //public static Action <FriendInfo, int> OnFriendStatusUpdate = delegate { };

    public static Action<string, int> OnFriendStatusUpdate = delegate { };


    // Start is called before the first frame update
    public void ChatConnectOnClick()
    {
        isConnected = true;
        chatClient = new ChatClient(this);
        Debug.Log("---- You chat username: " + Account.accountName);
        //chatClient.Connect("2156777f-aabb-4176-a2a2-e55b553b8289", "1.0.0", new AuthenticationValues(Account.accountName));
        chatClient.ConnectAndSetStatus("2156777f-aabb-4176-a2a2-e55b553b8289", "1.0.0", new AuthenticationValues(Account.accountName));

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
        AddPhotonChatFriend(Account.friendsList.Select(f => f.TitleDisplayName).ToArray());


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
        //throw new System.NotImplementedException();

        //Debug.Log("----OnStatusUpdate calisiyor");
        Debug.Log($"user: {user}, status: {status}, got message: {gotMessage}, message: {message}");
        FriendInfo friendInfo = findFriendInfoByUsername(user);
        OnFriendStatusUpdate?.Invoke(user, status);
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        //Debug.Log(chatClient.UserId);

        Debug.Log("You successfully subscribed to the chat");
        StartCoroutine(PrintFriends());
        //FindPhotonFriends();


        //throw new System.NotImplementedException();
    }

    public IEnumerator PrintFriends()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);

            Debug.Log(Account.friendsList.Count);
            Debug.Log(Account.friendsList.Select(f => f.TitleDisplayName).ToArray());
        }
        

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






    }

    private void OnDestroy()
    {
        PlayFabFriendManager.OnFriendsAddToPhoton -= AddPhotonChatFriend;
        PlayFabFriendManager.OnFriendsRemoveFromPhoton -= RemovePhotonChatFriend;
    }

    private void Start()
    {
        username = Account.accountName;
        Debug.Log("Account name is: " + username);
        Debug.Log(Account.accountId);
        Debug.Log(Account.credits);
        


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


    public IEnumerator FindPhotonFriends()
    {
        //while (true)
        //{
            yield return new WaitForSeconds(5f);


            if (Account.friendsList.Count != 0)
            {
                string[] friendDisplayNames = Account.friendsList.Select(f => f.TitleDisplayName).ToArray();
                chatClient.AddFriends(friendDisplayNames);
                Debug.Log("Friend count: " + Account.friendsList.Count);

                //Debug.Log("----> " + Account.friendsList.Count);
                //string[] friendDisplayNames = Account.friendsList.Select(f => f.TitleDisplayName).ToArray();

                //string[] friendDisplayNames = new string[Account.friendsList.Count];
                //for (int i = 0; i < Account.friendsList.Count; i++)
                //{
                //    //Account.friendsList[i].
                //    friendDisplayNames[i] = Account.friendsList[i].TitleDisplayName;
                //    //Debug.Log("=>" + friendDisplayNames[i]);
                //}
                ////Debug.Log("----Friendlist: " + friendDisplayNames);
                //chatClient.AddFriends(friendDisplayNames);
            }
        //}

        
    }

    public void AddPhotonChatFriend(string[] friendsToAdd)
    {
        if (friendsToAdd.Length != 0)
        {
            //string[] friendDisplayNames = Account.friendsList.Select(f => f.TitleDisplayName).ToArray();
            //chatClient.AddFriends(friendDisplayNames);
            //Debug.Log("Friend count: " + Account.friendsList.Count);
            chatClient.AddFriends(friendsToAdd);
            Debug.Log("Friend count: " + Account.friendsList.Count);

            foreach (string friendDisplayName in friendsToAdd)
            {
                AddItemToChatDropDown(friendDisplayName);
            }

        }
    }

    public void RemovePhotonChatFriend(string[] friendsToRemove)
    {
        if (friendsToRemove.Length != 0)
        {
            //string[] friendDisplayNames = Account.friendsList.Select(f => f.TitleDisplayName).ToArray();
            //chatClient.AddFriends(friendDisplayNames);
            //Debug.Log("Friend count: " + Account.friendsList.Count);
            chatClient.RemoveFriends(friendsToRemove);
            Debug.Log("Friend count: " + Account.friendsList.Count);

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
