using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayFabFriendManager : MonoBehaviour
{

    enum FriendIdType { PlayFabId, Username, Email, DisplayName };

    //List<FriendInfo> _friends = null;

    [SerializeField] Transform friendScrollView;
    public GameObject uiFriendPrefab;

    Dictionary<FriendInfo, UIFriend> myFriendsInScrollView = new Dictionary<FriendInfo, UIFriend>();
    [SerializeField] Button friendToggleButton;

    public static Action<string[]> OnFriendsAddToPhoton = delegate{ };
    public static Action<string[]> OnFriendsRemoveFromPhoton = delegate { };

    bool isFirstRun = true;



    private void Awake()
    {

        

        UIFriend.OnRemoveFriend += RemoveFriend;

        if (!Account.LoggedIn || Account.accountName.Equals("Guest"))
        {
            friendToggleButton.interactable = false;
            friendPanel.SetActive(false);
      
            return;
        }
        else
        {
            isFirstRun = true;
            GetFriends();
        }


    }

    private void OnDestroy()
    {
        UIFriend.OnRemoveFriend -= RemoveFriend;
    }

    private void Start()
    {
        
        //if(friendToggleButton.interactable)
            //GetFriends();
    }
    void DisplayFriends()
    {
        foreach (FriendInfo f in Account.friendsList)
        {
            bool isFound = false;

            foreach (FriendInfo g in myFriendsInScrollView.Keys)
            {
                if (f.FriendPlayFabId == g.FriendPlayFabId)
                {
                    isFound = true;
                    break;
                }
            }


            //I'm working on making it possible to see if your friend is online now.
            //    If he is online you can send a private message to this person


            //Ich arbeite daran, es möglich zu machen, zu sehen, ob Ihr Freund jetzt online ist.
            //    Wenn er online ist, können Sie dieser Person eine private Nachricht senden


            if (!isFound)
            {
                GameObject listing = Instantiate(uiFriendPrefab, friendScrollView);
                UIFriend uifriend = listing.GetComponent<UIFriend>();
                
                uifriend.Initialize(f);
                myFriendsInScrollView.Add(f, uifriend);

                if(!isFirstRun)
                    OnFriendsAddToPhoton?.Invoke(new string[] { f.TitleDisplayName });


            }


        }
        //myFriendsInScrollView = Account.friendsList;
        isFirstRun = false;
    }



    IEnumerator WaitForFriend()
    {
        yield return new WaitForSeconds(2);
        GetFriends();
    }

    public void RunWaitFunction()
    {
        StartCoroutine(WaitForFriend());
    }

    void DisplayPlayFabError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }


    void DisplayError(string error)
    {
        Debug.LogError(error);
    }



    public void GetFriends()
    {
        PlayFabClientAPI.GetFriendsList(new GetFriendsListRequest
        {
            IncludeSteamFriends = false,
            IncludeFacebookFriends = false,
            XboxToken = null
        }, result => {
            //_friends = result.Friends;
            Account.friendsList = result.Friends;
            DisplayFriends(); // triggers your UI
        }, DisplayPlayFabError);
    }



    void AddFriend(FriendIdType idType, string friendId)
    {
        var request = new AddFriendRequest();
        switch (idType)
        {
            case FriendIdType.PlayFabId:
                request.FriendPlayFabId = friendId;
                break;
            case FriendIdType.Username:
                request.FriendUsername = friendId;
                break;
            case FriendIdType.Email:
                request.FriendEmail = friendId;
                break;
            case FriendIdType.DisplayName:
                request.FriendTitleDisplayName = friendId;
                break;
        }
        
        PlayFabClientAPI.AddFriend(request, result => {
            Debug.Log("Friend added successfully!");
            GetFriends();
        }, DisplayPlayFabError);
    }


    // unlike AddFriend, RemoveFriend only takes a PlayFab ID
    // you can get this from the FriendInfo object under FriendPlayFabId
    void RemoveFriend(FriendInfo friendInfo, GameObject uiFriend)
    {
        PlayFabClientAPI.RemoveFriend(new RemoveFriendRequest
        {
            FriendPlayFabId = friendInfo.FriendPlayFabId
        }, result => {
            //_friends.Remove(friendInfo);
            //Account.friendsList.Remove(friendInfo);
            //myFriendsInScrollView.Remove(friendInfo);
            Debug.Log("Account.friendsList.Remove(friendInfo): " + Account.friendsList.Remove(friendInfo));
            Debug.Log("myFriendsInScrollView.Remove(friendInfo): " + myFriendsInScrollView.Remove(friendInfo));
            
            OnFriendsRemoveFromPhoton?.Invoke(new string[] { friendInfo.TitleDisplayName });
            Destroy(uiFriend);
        }, DisplayPlayFabError);
    }




    private string friendSearch;
    [SerializeField] GameObject friendPanel;

    public void InputFriendUserName(string friendDisplayName)
    {
        friendSearch = friendDisplayName;
    }

    public void SubmitFriendRequest()
    {
        AddFriend(FriendIdType.DisplayName, friendSearch);
    }

    public void ToggleFriendPanel()
    {
        //if (!friendPanel.activeSelf)
        //    GetFriends();
        friendPanel.SetActive(!friendPanel.activeSelf);
        //friendPanel.SetActive(!friendPanel.activeInHierarchy);

    }


}
