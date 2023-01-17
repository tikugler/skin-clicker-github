using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UserInfoManager : MonoBehaviour
{

    public static bool isUserInfoPanelFilled = false;
    //public static Action<int> CreateUserInfo = delegate { };
    [SerializeField] GameObject userInfoPanel;

    [SerializeField] Transform userScrollView;
    [SerializeField] Button closeUserInfoButton;

    public GameObject profilInfoHeaderPrefab;
    public GameObject profilInfoKeyValuePanelPrefab;
    public GameObject profilInfoUpperPanelPrefab;


    private GameObject upperProfilInfoPanel;
    private GameObject profilInfoUserImageObject;
    private TextMeshProUGUI profilInfoUserName;
    private TextMeshProUGUI profilInfoUserLastLogin;


    public static Dictionary<string, TextMeshProUGUI> itemEntryDictionary = new Dictionary<string, TextMeshProUGUI>();
    public static Dictionary<string, TextMeshProUGUI> skinEntryDictionary = new Dictionary<string, TextMeshProUGUI>();


    private TextMeshProUGUI creditValue;
    private TextMeshProUGUI activeSkinValue;
    private TextMeshProUGUI itemCountValue;
    private TextMeshProUGUI skinsCountValue;
    //private TextMeshProUGUI creditsPerSecValue;

    public static Action OpenPlayerImagesPanelForSelectionAction = delegate { };

    // Start is called before the first frame update

    private void Awake()
    {

        ContentDistributor.AddItemsToProfilInfo += AddItemsToItemIdList;
        ContentDistributor.AddSkinsToProfilInfo += AddSkinsToSkinIdList;
        closeUserInfoButton.onClick.AddListener(CloseUserInfoPanel);
        UserImageManager.OpenPlayerInfoPanelAction += AdjustAndOpenProfilInfo;
        UserImageManager.ChangeImageInUserInfoManagerAction += ChangeImageInUserInfoManager;
        UIFriend.OpenPlayerInfoPanelForFriendAction += AdjustAndOpenProfilInfo;

        AddProfilInfoHeader();
        AddUpperProfilInfoPanel();
        AddCreditsEntry();

        


    }

    private void OnDestroy()
    {
        ContentDistributor.AddItemsToProfilInfo -= AddItemsToItemIdList;
        ContentDistributor.AddSkinsToProfilInfo -= AddSkinsToSkinIdList;
        closeUserInfoButton.onClick.RemoveListener(CloseUserInfoPanel);

        UserImageManager.OpenPlayerInfoPanelAction -= AdjustAndOpenProfilInfo;
        UserImageManager.ChangeImageInUserInfoManagerAction -= ChangeImageInUserInfoManager;

        UIFriend.OpenPlayerInfoPanelForFriendAction -= AdjustAndOpenProfilInfo;
        profilInfoUserImageObject.GetComponent<Button>().onClick.RemoveListener(OpenPlayerImagesPanelForSelection);


    }

    private void AddItemsToItemIdList(ItemTemplate[] items)
    {
        AddItemsHeader();

        foreach (var item in items)
        {
            string itemId = item.id;
            GameObject ItemObject = Instantiate(profilInfoKeyValuePanelPrefab, userScrollView);
            FindObjectHelper.FindObjectInParent(ItemObject, "ProfilInfoKeyText").GetComponent<TextMeshProUGUI>().text = itemId;
            TextMeshProUGUI itemValueText = FindObjectHelper.FindObjectInParent(ItemObject, "ProfilInfoValueText").GetComponent<TextMeshProUGUI>();
            itemEntryDictionary.Add(itemId, itemValueText);
        }
    }

    private void AddSkinsToSkinIdList(SkinTemplate[] skins)
    {
        AddSkinsHeader();

        foreach (var skin in skins)
        {
            string skinId = skin.id;
            GameObject ItemObject = Instantiate(profilInfoKeyValuePanelPrefab, userScrollView);
            FindObjectHelper.FindObjectInParent(ItemObject, "ProfilInfoKeyText").GetComponent<TextMeshProUGUI>().text = skinId;
            TextMeshProUGUI skinValueText = FindObjectHelper.FindObjectInParent(ItemObject, "ProfilInfoValueText").GetComponent<TextMeshProUGUI>();
            skinEntryDictionary.Add(skinId, skinValueText);
        }

        AddOthersHeader();

    }



    void Start()
    {
        //CreateUserInfo?.Invoke(3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddProfilInfoHeader()
    {
        GameObject profilInfoHeader = Instantiate(profilInfoHeaderPrefab, userScrollView);
        profilInfoHeader.GetComponent<TextMeshProUGUI>().text = "Profil Info";
    }
    private void AddItemsHeader()
    {
        GameObject itemsInfoHeader = Instantiate(profilInfoHeaderPrefab, userScrollView);
        itemsInfoHeader.GetComponent<TextMeshProUGUI>().text = "Items";
    }
    private void AddSkinsHeader()
    {
        GameObject skinsInfoHeader = Instantiate(profilInfoHeaderPrefab, userScrollView);
        skinsInfoHeader.GetComponent<TextMeshProUGUI>().text = "Skins";
    }
    private void AddOthersHeader()
    {
        GameObject othersInfoHeader = Instantiate(profilInfoHeaderPrefab, userScrollView);
        othersInfoHeader.GetComponent<TextMeshProUGUI>().text = "Others";
        AddEntriesForOthers();
    }

    private void AddCreditsEntry()
    {
        GameObject creditInfo = Instantiate(profilInfoKeyValuePanelPrefab, userScrollView);
        FindObjectHelper.FindObjectInParent(creditInfo, "ProfilInfoKeyText").GetComponent<TextMeshProUGUI>().text = "Credits";
        creditValue = FindObjectHelper.FindObjectInParent(creditInfo, "ProfilInfoValueText").GetComponent<TextMeshProUGUI>();
    }

    private void AddEntriesForOthers()
    {
        GameObject activeSkinInfo = Instantiate(profilInfoKeyValuePanelPrefab, userScrollView);
        FindObjectHelper.FindObjectInParent(activeSkinInfo, "ProfilInfoKeyText").GetComponent<TextMeshProUGUI>().text = "Active Skin";
        activeSkinValue = FindObjectHelper.FindObjectInParent(activeSkinInfo, "ProfilInfoValueText").GetComponent<TextMeshProUGUI>();

        GameObject itemCountInfo = Instantiate(profilInfoKeyValuePanelPrefab, userScrollView);
        FindObjectHelper.FindObjectInParent(itemCountInfo, "ProfilInfoKeyText").GetComponent<TextMeshProUGUI>().text = "Item Amount";
        itemCountValue = FindObjectHelper.FindObjectInParent(itemCountInfo, "ProfilInfoValueText").GetComponent<TextMeshProUGUI>();

        GameObject skinCountInfo = Instantiate(profilInfoKeyValuePanelPrefab, userScrollView);
        FindObjectHelper.FindObjectInParent(skinCountInfo, "ProfilInfoKeyText").GetComponent<TextMeshProUGUI>().text = "Skin Amount";
        skinsCountValue = FindObjectHelper.FindObjectInParent(skinCountInfo, "ProfilInfoValueText").GetComponent<TextMeshProUGUI>();

        isUserInfoPanelFilled = true;
    }

    private void AddUpperProfilInfoPanel()
    {
        upperProfilInfoPanel = Instantiate(profilInfoUpperPanelPrefab, userScrollView);
        profilInfoUserImageObject = FindObjectHelper.
            FindObjectInParent(upperProfilInfoPanel, "ProfilInfoUserImage");
        profilInfoUserImageObject.GetComponent<Button>().onClick.AddListener(OpenPlayerImagesPanelForSelection);
        profilInfoUserName = FindObjectHelper.
            FindObjectInParent(upperProfilInfoPanel, "ProfilInfoUserName").GetComponent<TextMeshProUGUI>();
        profilInfoUserLastLogin = FindObjectHelper.
            FindObjectInParent(upperProfilInfoPanel, "ProfilInfoUserLastLogin").GetComponent<TextMeshProUGUI>();
    }

    private void AdjustAndOpenProfilInfo(int selectedPictureId, string profilName, int lastOnlineSecAgo, int credits, Dictionary<string, int> items, List<string> skins, string activeSinId)
    {
        if (!Account.LoggedIn)
            return;
        if (Account.accountName.Equals(profilName)){
            profilInfoUserImageObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            profilInfoUserImageObject.GetComponent<Button>().interactable = false;
        }

        profilInfoUserImageObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("ProfilePicture/" + selectedPictureId);
        profilInfoUserName.text = profilName;

        int boughtItemCount = 0;
        int boughtSkinCount = 0;

        if (lastOnlineSecAgo == 0)
        {
            profilInfoUserLastLogin.text = "Online";
        }
        else if(lastOnlineSecAgo > 0)
        {
            profilInfoUserLastLogin.text = $"zuletzt gesehen: vor {FromSecondsToPassedTimeAsString(lastOnlineSecAgo)}";
        }
        else
        {
            profilInfoUserLastLogin.text = $"zuletzt gesehen: Unbekannt";


        }
        creditValue.text = credits.ToString();
        foreach (KeyValuePair<string, TextMeshProUGUI> entry in itemEntryDictionary)
        {

            int itemAmount = items.GetValueOrDefault(entry.Key, 0);
            entry.Value.text = itemAmount.ToString();
            boughtItemCount += itemAmount;
        }

     
        foreach (KeyValuePair<string, TextMeshProUGUI> entry in skinEntryDictionary)
        {

            if (skins.Contains(entry.Key))
            {
                entry.Value.text = "freigeschaltet";
                boughtSkinCount += 1;
            }
            else
            {
                entry.Value.text = "--";

            }
        }

        if (!String.IsNullOrEmpty(activeSinId))
        {
            activeSkinValue.text = activeSinId;
        }
        else
        {
            activeSkinValue.text = "--";

        }
        itemCountValue.text = boughtItemCount.ToString();
        skinsCountValue.text = boughtSkinCount.ToString() + "/" + skinEntryDictionary.Count.ToString();
        userInfoPanel.SetActive(true);
    }
    private void OpenPlayerImagesPanelForSelection()
    {
        OpenPlayerImagesPanelForSelectionAction?.Invoke();
    }

    private void CloseUserInfoPanel()
    {
        userInfoPanel.SetActive(false);
    }

    private void ChangeImageInUserInfoManager(int selectedPictureId)
    {
        profilInfoUserImageObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("ProfilePicture/" + selectedPictureId);

    }

    private string FromSecondsToPassedTimeAsString(int timeAsSecond)
    {
        
        string endTimeString = "";

        if(timeAsSecond >= (24 * 60 * 60))
        {
            int dayCount = timeAsSecond / (24 * 60 * 60);
            endTimeString = dayCount.ToString() + " Tage ";
            timeAsSecond -= dayCount * (24 * 60 * 60);
            return endTimeString;
        }
        if (timeAsSecond >= (60 * 60))
        {
            int hourCount = timeAsSecond / (60 * 60);
            endTimeString = hourCount.ToString() + " Stunden ";
            timeAsSecond -= hourCount * (60 * 60);
            return endTimeString;


        }
        if (timeAsSecond >= (60))
        {
            int minCount = timeAsSecond / (60);
            endTimeString = minCount.ToString() + " Minuten ";
            timeAsSecond -= minCount * (24 * 60 * 60);
            return endTimeString;

        }

        if (timeAsSecond > (0))
        {
            int secCount = timeAsSecond;
            endTimeString = secCount.ToString() + " Sekunden";
            return endTimeString;

        }
        return endTimeString;
    }
}
