using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// If you want to create a new Achievement, first define the achievement with "CreateAchievement" in the Start Method.
// Then define the Achievement Condition in the Update-Method
public class AchievementManager : MonoBehaviour
{
    public GameObject achievementPanel;
    public GameObject achievementPrefab;
    public Sprite[] sprites;
    public GameObject visualAchievement;
    public Dictionary<string, Achievement> achievements = new Dictionary<string, Achievement>();
    private static AchievementManager instance;
    public static AchievementManager Instance
    {
        get 
        { 
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<AchievementManager>();
            }
            return AchievementManager.instance; 
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        achievementPanel.SetActive(true);
        CreateAchievement("AchievementTable", AchievementIdentifier.Achieve10Points, AchievementIdentifier.Achieve10PointsDes, 1, AchievementIdentifier.GetAchievementBonusText(AchievementIdentifier.Achieve10Points));
        CreateAchievement("AchievementTable", AchievementIdentifier.Achieve500Points, AchievementIdentifier.Achieve500PointsDes, 1, AchievementIdentifier.GetAchievementBonusText(AchievementIdentifier.Achieve500Points));
        CreateAchievement("AchievementTable", AchievementIdentifier.Achieve5000Points, AchievementIdentifier.Achieve5000PointsDes, 1, AchievementIdentifier.GetAchievementBonusText(AchievementIdentifier.Achieve5000Points));
        CreateAchievement("AchievementTable", AchievementIdentifier.Achieve50000Points, AchievementIdentifier.Achieve50000PointsDes, 1, AchievementIdentifier.GetAchievementBonusText(AchievementIdentifier.Achieve50000Points));
        CreateAchievement("AchievementTable", AchievementIdentifier.Achieve500000Points, AchievementIdentifier.Achieve500000PointsDes, 1, AchievementIdentifier.GetAchievementBonusText(AchievementIdentifier.Achieve500000Points));
        CreateAchievement("AchievementTable", AchievementIdentifier.Achieve1000000Points, AchievementIdentifier.Achieve1000000PointsDes, 1, AchievementIdentifier.GetAchievementBonusText(AchievementIdentifier.Achieve1000000Points));
        CreateAchievement("AchievementTable", AchievementIdentifier.Achieve5000000Points, AchievementIdentifier.Achieve5000000PointsDes, 1, AchievementIdentifier.GetAchievementBonusText(AchievementIdentifier.Achieve5000000Points));
        CreateAchievement("AchievementTable", AchievementIdentifier.Achieve10000000Points, AchievementIdentifier.Achieve10000000PointsDes, 1, AchievementIdentifier.GetAchievementBonusText(AchievementIdentifier.Achieve10000000Points));
        achievementPanel.SetActive(false);
        RefreshEarnedAchievements();
    }

    // Is called once per Click and once per Autoclick
    public void CheckForAchievements()
    {
        if (Account.credits >= 10)
        {
            EarnAchievement(AchievementIdentifier.Achieve10Points);
        }
        if (Account.credits >= 500)
        {
            EarnAchievement(AchievementIdentifier.Achieve500Points);
        }
        if (Account.credits >= 5000)
        {
            EarnAchievement(AchievementIdentifier.Achieve5000Points);
        }
        if (Account.credits >= 50000) {
            EarnAchievement(AchievementIdentifier.Achieve50000Points);
        }
        if (Account.credits >= 500000)
        {
            EarnAchievement(AchievementIdentifier.Achieve500000Points);
        }
        if (Account.credits >= 1000000)
        {
            EarnAchievement(AchievementIdentifier.Achieve1000000Points);
        }
        if (Account.credits >= 5000000)
        {
            EarnAchievement(AchievementIdentifier.Achieve5000000Points);
        }
        if (Account.credits >= 10000000)
        {
            EarnAchievement(AchievementIdentifier.Achieve10000000Points);
        }
    }

    public void EarnAchievement(string title)
    {
        //if (Account.earnedAchievements.Contains(title))
        //{
        //    return;
        //}
        if (!Account.earnedAchievements.Contains(title) && achievements[title].EarnAchievement())
        {
            AchievementIdentifier.GetAchievementBonus(title);
            GameObject achievement = (GameObject)Instantiate(visualAchievement);
            visualAchievement.GetComponent<Image>().color = new Color32(255, 225, 64, 255);
            SetAchievementInfo("EarnCanvas", achievement, title);
            StartCoroutine(HideAchievement(achievement));
        }
    }

    public IEnumerator HideAchievement(GameObject achievement)
    {
        yield return new WaitForSeconds(4);
        Destroy(achievement);
    }

    public void CreateAchievement(string parent, string title, string description, int spriteIndex, string bonus)
    {
        GameObject achievementGO = (GameObject)Instantiate(achievementPrefab);
        //if (Account.earnedAchievements.Contains(title))
        //{
        //    achievementGO.GetComponent<Image>().color = new Color32(255, 225, 64, 255);
        //}
        Achievement newAchievement = new Achievement(title, description, spriteIndex, achievementGO, bonus); //oder name?
        achievements.Add(title, newAchievement);
        SetAchievementInfo(parent, achievementGO, title);
    }

    public void SetAchievementInfo(string parent, GameObject achievement, string title)
    {
        achievement.transform.SetParent(GameObject.Find(parent).transform);
        achievement.transform.localScale = new Vector3(1, 1, 1);
        achievement.transform.GetChild(1).GetComponent<Text>().text = title;
        achievement.transform.GetChild(3).GetComponent<Text>().text = achievements[title].Description;
        achievement.transform.GetChild(2).GetComponent<Image>().sprite = sprites[achievements[title].SpriteIndex];
        achievement.transform.GetChild(4).GetComponent<Text>().text = achievements[title].Bonus;
    }

    public void RefreshEarnedAchievements()
    {
        foreach (string achievement in achievements.Keys)
        {
            if (Account.earnedAchievements.Contains(achievement))
            {
                achievements[achievement].AchievementRef.transform.GetChild(0).GetComponent<Image>().color = new Color32(255, 225, 64, 255);
            }
        }
    }
}
