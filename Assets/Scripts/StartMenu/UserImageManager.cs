using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserImageManager : MonoBehaviour
{
    public GameObject profilePicturePopup;
    public Button userImage;
    public TextMeshProUGUI userNameText;
    public Sprite[] allProfilePictures;
    public List<GameObject> createdGameObjects;
    public Button closePicturePopUpButton;

    private static UserImageManager instance;

    public static Action<int, string, int, int, Dictionary<string, int>, List<string>, string> OpenPlayerInfoPanelAction = delegate { };
    public static Action<int> ChangeImageInUserInfoManagerAction = delegate { };


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            Account.ChangeProfilPicture += ChangeProfilImageById;
            Account.ChangeAccountNameText += ChangeUserNameText;

            //userImage.onClick.AddListener(OpenImagePopup);
            userImage.onClick.AddListener(OpenPlayerInfoPanel);
            closePicturePopUpButton.onClick.AddListener(CloseProfile);
            DontDestroyOnLoad(gameObject);
            UserInfoManager.OpenPlayerImagesPanelForSelectionAction += OpenImagePopup;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OpenPlayerInfoPanel()
    {
        OpenPlayerInfoPanelAction?.Invoke(Account.selectedPictureId, Account.accountName, 0, Account.credits, Account.upgradeList, Account.skinIdList, Account.activeSkinId);
    }

    private void OnDestroy()
    {
        if(instance == this)
        {
            Account.ChangeProfilPicture -= ChangeProfilImageById;
            Account.ChangeAccountNameText -= ChangeUserNameText;

            //userImage.onClick.RemoveListener(OpenImagePopup);
            userImage.onClick.RemoveListener(OpenPlayerInfoPanel);
            closePicturePopUpButton.onClick.RemoveListener(CloseProfile);
            UserInfoManager.OpenPlayerImagesPanelForSelectionAction -= OpenImagePopup;

        }
    }

    //private void OnEnable()
    //{
        

    //}

    //private void OnDisable()
    //{
        


    //}
    void Start()
    {
        createdGameObjects = new List<GameObject>();
        userImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("ProfilePicture/" + Account.selectedPictureId);
    }
    public void OpenImagePopup()
    {
        if (!Account.LoggedIn)
            return;
        AddImages();
        profilePicturePopup.SetActive(true);
    }

    public void CloseProfile()
    {
        profilePicturePopup.SetActive(false);
    }

    private void AddImages()
    {
        // If Profile Pictures were already created in the Session
        // There is no reason to recreate them. We use the already
        // Created objects.
        if (createdGameObjects.Count != 0)
            return;

        // We need the X Position to get the Pictures to the right
        // Position after the line limit is reached
        var startX = -156.5f;

        // Track Current Position of the Picture
        var currX = -156.5f;
        var currY = 137f;

        var lineLimit = 6;
        var marginX = 60f;
        var marginY = 60f;
        // Loop through all available Profile Pictures while limiting
        // the Amount of Pictures on one line
        for (int currentImage = 0; currentImage < allProfilePictures.Length; currentImage++)
        {
            if (currentImage % lineLimit == 0)
            {
                currY -= marginY;
                currX = startX;
            }
            GameObject imageObject = new GameObject();
            imageObject.name = currentImage.ToString();
            imageObject.transform.SetParent(profilePicturePopup.transform);

            Image img = imageObject.AddComponent<Image>();
            Button button = imageObject.AddComponent<Button>();
            button.onClick.AddListener(delegate { OnButtonClicked(button); });
            img.sprite = Resources.Load<Sprite>("ProfilePicture/" + currentImage);
            var transform = imageObject.GetComponent<RectTransform>();
            transform.localPosition = new Vector2(currX, currY);
            transform.localScale = new Vector2(0.5f, 0.5f);
            currX += marginX;
            createdGameObjects.Add(imageObject);
        }
    }

    public void OnButtonClicked(Button button)
    {

        var id = button.transform.name;
        Debug.Log("Selected Id = " + id);
        userImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("ProfilePicture/" + id);
        Account.selectedPictureId = Convert.ToInt32(id);
        ChangeImageInUserInfoManagerAction?.Invoke(Account.selectedPictureId);
        //TODO: Update Database here to save the selected picture ID?
        PlayfabUpdateUserData.UpdateStatisticOnPlayFab("SelectedPictureId", Account.selectedPictureId);
    }

    public void ChangeProfilImageById(int id)
    {
        Account.selectedPictureId = Convert.ToInt32(id);
        userImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("ProfilePicture/" + id);
    }

    public void ChangeUserNameText(string newUserName)
    {
        userNameText.text = newUserName;
    }

}
