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
        friendPanel.SetActive(!friendPanel.activeSelf);
    }


}
