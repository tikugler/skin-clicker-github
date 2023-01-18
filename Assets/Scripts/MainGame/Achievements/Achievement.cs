using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Achievement class with fields
/// </summary>
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
    private string bonus;
    public string Bonus
    {
        get { return bonus; }
        set { bonus = value; }
    }

    /// <summary>
    /// Constructor of Achievements with name, description, index of the used sprite, a reference to the Gameobject and the bonus.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <param name="spriteIndex"></param>
    /// <param name="achievementRef"></param>
    /// <param name="bonus"></param>
    public Achievement(string name, string description, int spriteIndex, GameObject achievementRef, string bonus)
    {
        this.name = name;
        this.description = description;
        this.unlocked = false;
        this.spriteIndex = spriteIndex;
        this.achievementRef = achievementRef;
        this.bonus = bonus;
    }

    /// <summary>
    /// When earning an achievement, the color changes to gold and the achievement gets saved.
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Save the Earned Achievement in the Accounnt-class.
    /// </summary>
    public void SaveAchievement()
    {
        unlocked = true;
        Account.earnedAchievements.Add(this.name);
    }
}
