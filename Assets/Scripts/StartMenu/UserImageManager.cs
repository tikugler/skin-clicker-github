using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserImageManager : MonoBehaviour
{
    public GameObject profilePicturePopup;
    public Image userImage;
    public Sprite[] allProfilePictures;
    public List<GameObject> createdGameObjects;

    void Start()
    {
        createdGameObjects = new List<GameObject>();
        userImage.sprite = Resources.Load<Sprite>("ProfilePicture/" + Account.selectedPictureId);
    }
    public void OpenImagePopup()
    {
        profilePicturePopup.SetActive(true);
        AddImages();
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
        userImage.sprite = Resources.Load<Sprite>("ProfilePicture/" + id);
        Account.selectedPictureId = Convert.ToInt32(id);
        //TODO: Update Database here to save the selected picture ID?
    }

}
