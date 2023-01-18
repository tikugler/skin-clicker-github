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

    [SerializeField] Transform friendScrollView;
    public GameObject uiFriendPrefab;

    Dictionary<FriendInfo, UIFriend> myFriendsInScrollView = new Dictionary<FriendInfo, UIFriend>();
    [SerializeField] Button friendToggleButton;

    public static Action<string[]> OnFriendsAddToPhoton = delegate{ };
    public static Action<string[]> OnFriendsRemoveFromPhoton = delegate { };

    bool isFirstRun = true;
    private string friendSearch;
    [SerializeField] GameObject friendPanel;



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

    // removes event
    private void OnDestroy()
    {
        UIFriend.OnRemoveFriend -= RemoveFriend;
    }

    /// <summary>
    /// updates the friendlist panel
    /// </summary>
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
        isFirstRun = false;
    }




    void DisplayPlayFabError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }


    /// <summary>
    /// send an PlayFab-Client-API request to get the lsit of friends
    /// </summary>
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


    /// <summary>
    /// adds friend in PlazFab according to given Type.
    /// we add by using DisplayName
    /// </summary>
    /// <param name="idType">type of given friendId</param>
    /// <param name="friendId">id to add</param>
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
            Debug.Log("Account.friendsList.Remove(friendInfo): " + Account.friendsList.Remove(friendInfo));
            Debug.Log("myFriendsInScrollView.Remove(friendInfo): " + myFriendsInScrollView.Remove(friendInfo));
            
            OnFriendsRemoveFromPhoton?.Invoke(new string[] { friendInfo.TitleDisplayName });
            Destroy(uiFriend);
        }, DisplayPlayFabError);
    }


    /// <summary>
    /// this method is called when user input anything in the inputfield in Friend Panel
    /// friendSearch is assigned to given value
    /// </summary>
    /// <param name="friendDisplayName"></param>
    public void InputFriendUserName(string friendDisplayName)
    {
        friendSearch = friendDisplayName;
    }

    /// <summary>
    /// tries to add friend usinf DispLayName
    /// </summary>
    public void SubmitFriendRequest()
    {
        AddFriend(FriendIdType.DisplayName, friendSearch);
    }

    /// <summary>
    /// toggles friend panel
    /// </summary>
    public void ToggleFriendPanel()
    {
        friendPanel.SetActive(!friendPanel.activeSelf);
    }


}
