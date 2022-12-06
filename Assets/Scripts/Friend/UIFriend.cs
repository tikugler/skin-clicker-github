using System;
using TMPro;
//using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using Photon.Chat;
using PlayFab.ClientModels;


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

    private void Awake()
    {
       

    }
    private void OnDestroy()
    {
        Debug.Log($"{friendName} is destroyed");
    }

    private void OnEnable()
    {
        if (string.IsNullOrEmpty(friendName)) return;
        //OnGetCurrentStatus?.Invoke(friendName);
    }

    //public void Initialize(FriendInfo friend)
    //{
    //    Debug.Log($"{friend.UserId} is online: {friend.IsOnline} ; in room: {friend.IsInRoom} ; room name: {friend.Room}");

    //    SetupUI();
    //}
    //public void Initialize(string friendName)
    public void Initialize(FriendInfo f)
    {
        PhotonChatManager.OnFriendStatusUpdate += HandleStatusUpdated;
        this.friendName = f.TitleDisplayName;
        this.friendInfo = f;
        //friendManager = playFabFriendManager;
        Debug.Log($"{friendName} is added");
        //this.friendName = friendName;
        

        SetupUI();
        //OnGetCurrentStatus?.Invoke(friendName);
    }


    private void HandleStatusUpdated(string playerName, int status)
    {
        Debug.Log($"---- friendName: {friendName}, playerName: {playerName}");
        if (string.Compare(friendName, playerName) == 0)
        {
            Debug.Log($"Updating status in UI for {playerName} to status {status}");
            SetStatus(status);
        }
    }

    private void SetupUI()
    {
        friendNameText.SetText(friendName);
    }

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

    public void RemoveFriend()
    {
        Debug.Log($"Clicked to remove friend {friendName}");
        OnRemoveFriend?.Invoke(friendInfo, gameObject);
    }


}
