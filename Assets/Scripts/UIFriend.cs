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
    public static Action<string> OnInviteFriend = delegate { };
    public static Action<string> OnGetCurrentStatus = delegate { };
    public static Action OnGetRoomStatus = delegate { };

    private FriendInfo friendInfo;
    public static PlayFabFriendManager friendManager;

    private void Awake()
    {
        //PhotonChatFriendController.OnStatusUpdated += HandleStatusUpdated;
        //deleteFriendButton.onClick.AddListener += 
    }
    private void OnDestroy()
    {
        //PhotonChatFriendController.OnStatusUpdated -= HandleStatusUpdated;
    }

    private void OnEnable()
    {
        if (string.IsNullOrEmpty(friendName)) return;
        OnGetCurrentStatus?.Invoke(friendName);
        OnGetRoomStatus?.Invoke();
    }

    //public void Initialize(FriendInfo friend)
    //{
    //    Debug.Log($"{friend.UserId} is online: {friend.IsOnline} ; in room: {friend.IsInRoom} ; room name: {friend.Room}");

    //    SetupUI();
    //}
    //public void Initialize(string friendName)
    public void Initialize(FriendInfo f)
    {
        //friendManager = playFabFriendManager;
        Debug.Log($"{friendName} is added");
        //this.friendName = friendName;
        this.friendName = f.Username;
        this.friendInfo = f;

        SetupUI();
        OnGetCurrentStatus?.Invoke(friendName);
        OnGetRoomStatus?.Invoke();
    }


    //private void HandleStatusUpdated(PhotonStatus status)
    //{
    //    if (string.Compare(friendName, status.PlayerName) == 0)
    //    {
    //        Debug.Log($"Updating status in UI for {status.PlayerName} to status {status.Status}");
    //        SetStatus(status.Status);
    //    }
    //}

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
            OnGetRoomStatus?.Invoke();
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
