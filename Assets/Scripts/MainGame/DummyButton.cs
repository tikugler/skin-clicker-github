using UnityEngine;
using UnityEngine.UI;

public class DummyButton : MonoBehaviour
{
    public int multiplicator = 1;
    public int basePoints = 1;
    public int multiplicatorOfSkin = 1;
    public static float criticalMultiplicator = 1;
    public static float criticalChance = 0f;


    public void MainButtonAction()
    {

        float randValue = Random.value;
        if (randValue > (1.0f - criticalChance))
        {
            Account.credits += (int)System.Math.Round(basePoints * multiplicator * multiplicatorOfSkin * criticalMultiplicator);
        }
        else
        {
            Account.credits += basePoints * multiplicator * multiplicatorOfSkin;
        }
        AchievementManager.Instance.CheckForAchievements();

    }

    public void WorkerAction(int worker)
    {
        Account.credits += basePoints * multiplicatorOfSkin * worker;
        AchievementManager.Instance.CheckForAchievements();
    }


    public void SetMultiplicator(int multi)
    {
        multiplicator = multi;
    }

    public void SetSkin(Sprite skin)
    {
        this.GetComponentInChildren<Text>().text = "";
        this.GetComponent<Image>().color = Color.white;
        this.GetComponent<Image>().sprite = skin;
        //this.GetComponent<RectTransform>().localScale = new Vector3(1920 / (skin.rect.width / 16), 1080 / (skin.rect.height / 9), 0);
        this.GetComponent<RectTransform>().localScale = new Vector3(1, 2.4f, 1);
    }
}
