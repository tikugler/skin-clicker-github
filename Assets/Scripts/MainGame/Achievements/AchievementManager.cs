using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{

    public GameObject achievementPrefab;
    public Sprite[] sprites;
    public GameObject visualAchievement;
    public Dictionary<string, Achievement> achievements = new Dictionary<string, Achievement>();
    public Sprite unlockedSprite;
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
        CreateAchievement("AchievementTable", "Earn 10 Points !", "You earned 10 Points !", 1);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    EarnAchievement("Press W");
        //}
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
            SetAchievementInfo("EarnCanvas", achievement, title);

            StartCoroutine(HideAchievement(achievement));
        }
    }

    public IEnumerator HideAchievement(GameObject achievement)
    {
        yield return new WaitForSeconds(3);
        Destroy(achievement);
    }

    public void CreateAchievement(string parent, string title, string description, int spriteIndex)
    {
        GameObject achievementGO = (GameObject)Instantiate(achievementPrefab);
        Achievement newAchievement = new Achievement(name, description, spriteIndex, achievementGO);
        achievements.Add(title, newAchievement);
        SetAchievementInfo(parent, achievementGO, title);
    }

    public void SetAchievementInfo(string parent, GameObject achievement, string title)
    {
        achievement.transform.SetParent(GameObject.Find(parent).transform);
        achievement.transform.localScale = new Vector3(1, 1, 1);
        achievement.transform.GetChild(0).GetComponent<Text>().text = title;
        achievement.transform.GetChild(2).GetComponent<Text>().text = achievements[title].Description;
        achievement.transform.GetChild(1).GetComponent<Image>().sprite = sprites[achievements[title].SpriteIndex];
    }
}
