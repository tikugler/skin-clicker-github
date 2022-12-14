using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementPopUp : MonoBehaviour
{
    public GameObject AchievementPanel;
    public GameObject AchievementButton;
    public GameObject CloseButton;
    
    public void OpenAchievements()
    {
        AchievementPanel.SetActive(true);
    }

    public void CloseAchievements()
    {
        AchievementPanel.SetActive(false);
    }
}
