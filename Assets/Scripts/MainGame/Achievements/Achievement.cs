using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievement
{
    private string name;
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    private string description;
    public string Description
    {
        get { return description; }
        set { description = value; }
    }
    private bool unlocked;
    public bool Unlocked
    {
        get { return unlocked; }
        set { unlocked = value; }
    }
    private int spriteIndex;
    public int SpriteIndex
    {
        get { return spriteIndex; }
        set { spriteIndex = value; }
    }
    private GameObject achievementRef;
    public GameObject AchievementRef
    {
        get { return achievementRef; }
        set { achievementRef = value; }
    }

    public Achievement(string name, string description, int spriteIndex, GameObject achievementRef)
    {
        this.name = name;
        this.description = description;
        this.unlocked = false;
        this.spriteIndex = spriteIndex;
        this.achievementRef = achievementRef;
    }

    public bool EarnAchievement()
    {
        if (!unlocked)
        {
            achievementRef.transform.GetChild(0).GetComponent<Image>().color = new Color32(255, 225, 64, 255);
            SaveAchievement();
            return true;
        }
        return false;
    }

    public void SaveAchievement()
    {
        unlocked = true;
        Account.earnedAchievements.Add(this.name);
    }
}
