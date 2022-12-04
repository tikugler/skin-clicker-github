using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;

using UnityEngine;
using UnityEngine.UI;

public class PlayFabFriendManager : MonoBehaviour
{

    enum FriendIdType { PlayFabId, Username, Email, DisplayName };

    //List<FriendInfo> _friends = null;

    [SerializeField] Transform friendScrollView;
    public GameObject uiFriendPrefab;
    List<FriendInfo> myFriendsInScrollView = new List<FriendInfo>();
    [SerializeField] Button friendToggleButton;


    private void Awake()
    {
        UIFriend.OnRemoveFriend += RemoveFriend;
    }

    private void OnDestroy()
    {
        UIFriend.OnRemoveFriend -= RemoveFriend;
    }

    private void Start()
    {
        if (!Account.LoggedIn)
        {
            friendToggleButton.interactable = false;
            friendPanel.SetActive(false);
            return;
        }

        GetFriends();
    }
    void DisplayFriends(List<FriendInfo> friendsCache)
    {
        foreach (FriendInfo f in friendsCache)
        {
            bool isFound = false;

            foreach (FriendInfo g in myFriendsInScrollView)
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

            }


        }
        myFriendsInScrollView = friendsCache;
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
            DisplayFriends(Account.friendsList); // triggers your UI
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
        // Execute request and update friends when we are done
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
            Account.friendsList.Remove(friendInfo);
            Destroy(uiFriend);
        }, DisplayPlayFabError);
    }




    private string friendSearch;
    [SerializeField] GameObject friendPanel;

    public void InputFriendUserName(string friendUserName)
    {
        friendSearch = friendUserName;
    }

    public void SubmitFriendRequest()
    {
        AddFriend(FriendIdType.Username, friendSearch);
    }

    public void ToggleFriendPanel()
    {
        //if (!friendPanel.activeSelf)
        //    GetFriends();
        friendPanel.SetActive(!friendPanel.activeSelf);
        //friendPanel.SetActive(!friendPanel.activeInHierarchy);

    }


}
