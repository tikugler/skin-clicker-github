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
        CreateAchievement("AchievementTable", "Earn 10 Points !", "You earned 10 Points !", 1);
        achievementPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Account.credits >= 10)
        {
            EarnAchievement("Earn 10 Points !");
        }
    }

    public void EarnAchievement(string title)
    {
        if (achievements[title].EarnAchievement())
        {
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

    public void CreateAchievement(string parent, string title, string description, int spriteIndex)
    {
        GameObject achievementGO = (GameObject)Instantiate(achievementPrefab);
        Achievement newAchievement = new Achievement(title, description, spriteIndex, achievementGO); //oder name?
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
    }
}
