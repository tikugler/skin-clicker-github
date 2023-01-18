using System;
using TMPro;
//using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using Photon.Chat;
using PlayFab.ClientModels;
using PlayFab;
using System.Net;
using System.Collections.Generic;

public class UIFriend : MonoBehaviour
{
    [SerializeField] private Button deleteFriendButton;
    [SerializeField] private TMP_Text friendNameText;
    [SerializeField] private string friendName;
    [SerializeField] private bool isOnline;
    [SerializeField] private Image onlineImage;
    [SerializeField] private Color onlineColor = new Color(0, 236, 76);
    [SerializeField] private Color offlineColor = new Color(209, 209, 209);

    public static Action<FriendInfo, GameObject> OnRemoveFriend = delegate { };

    private FriendInfo friendInfo;
    public static PlayFabFriendManager friendManager;

    public static Action<int, string, int, int, Dictionary<string, int>, List<string>, string> OpenPlayerInfoPanelForFriendAction = delegate { };

    /// <summary>
    /// this method is called first and adds a listener
    /// </summary>
    private void Awake()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(FriendEntryClicked);
    }

    /// <summary>
    /// removes listener
    /// </summary>
    private void OnDestroy()
    {
        gameObject.GetComponent<Button>().onClick.RemoveListener(FriendEntryClicked);
    }


    /// <summary>
    /// assigns a delegate to PhotonChatManager.OnFriendStatusUpdate
    /// </summary>
    /// <param name="f">FriendInfo from PlayFab</param>
    public void Initialize(FriendInfo f)
    {
        PhotonChatManager.OnFriendStatusUpdate += HandleStatusUpdated;
        this.friendName = f.TitleDisplayName;
        this.friendInfo = f;
        SetupUI();
    }

    /// <summary>
    /// the arguments are given by PhotonChatManager.OnFriendStatusUpdate
    /// this method is called for each UIFriend class in the scene
    /// only one UIFriend,friendName is equal to playerName
    /// this method changes the status of a friend in the friendlist,
    /// whose playername is same 
    /// </summary>
    /// <param name="playerName">Displayname of player whose status is changed </param>
    /// <param name="status"status 0 means offline, 2 means online></param>
    private void HandleStatusUpdated(string playerName, int status)
    {
        if (string.Compare(friendName, playerName) == 0)
        {
            SetStatus(status);
        }
    }

    /// <summary>
    /// changes the text of friend in the UI
    /// </summary>
    private void SetupUI()
    {
        friendNameText.SetText(friendName);
    }

    /// <summary>
    /// changes the color to green to point out that friens is online if status equal to ChatUserStatus.Online (2)
    /// otherwise red
    /// </summary>
    /// <param name="status">status 0 means offline, 2 means online</param>
    private void SetStatus(int status)
    {
        if (status == ChatUserStatus.Online)
        {
            onlineImage.color = onlineColor;
            isOnline = true;
        }
        else
        {
            onlineImage.color = offlineColor;
            isOnline = false;
        }
    }

    /// <summary>
    /// calls the action to remove the friend from friendlist and db,
    /// which is done by PlayFabFriendManager
    /// </summary>
    public void RemoveFriend()
    {
        OnRemoveFriend?.Invoke(friendInfo, gameObject);
    }

    /// <summary>
    /// called when UI item for friend is clicked
    /// sends an PlayFab Client API request to get friends statistics ans player profile
    /// </summary>
    private  void FriendEntryClicked()
    {
        var request = new GetPlayerProfileRequest();
        request.PlayFabId = friendInfo.FriendPlayFabId;
        request.ProfileConstraints = new PlayerProfileViewConstraints()
        {
            ShowStatistics = true
        };
        PlayFabClientAPI.GetPlayerProfile(request, GetProfileSuccess, error=>Debug.Log(error.GenerateErrorReport()));
    }

    /// <summary>
    /// in this function, statistics are transformed into the right format
    /// for player statistics panel
    /// and an action is triggered to fill the panel and open it
    /// </summary>
    /// <param name="result"></param>
    private void GetProfileSuccess(GetPlayerProfileResult result)
    {
        string userDisplayName = friendName;
        List<string> skinIdList = new List<string>();
        Dictionary<string, int> upgradeList = new Dictionary<string, int>();  
        string activeSkinId = null;
        int credits = 0;
        int passedSeconds = -1;
        int selectedPictureId = 0;

        foreach (var stat in result.PlayerProfile.Statistics)
        {
            switch (stat.Name)
            {
                case "Credits":
                    credits = stat.Value;
                    break;
                case "LeavingGameTime":
                    if(!isOnline)
                        passedSeconds = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds - stat.Value;
                    break;
                case "SelectedPictureId":
                    selectedPictureId = stat.Value;
                    break;
                case string skin when skin.StartsWith("SKIN_"):
                    string skinId = skin.Substring(5);
                    if (stat.Value == 1)
                    {
                        skinIdList.Add(skinId);
                    }
                    else if (stat.Value == 2)
                    {
                        skinIdList.Add(skinId);
                        activeSkinId = skinId;
                    }
                    break;
                default:
                    upgradeList.Add(stat.Name, stat.Value);
                    break;
            }
        }

        OpenPlayerInfoPanelForFriendAction?.Invoke(selectedPictureId, userDisplayName, passedSeconds, credits, upgradeList, skinIdList, activeSkinId);

    }
}
