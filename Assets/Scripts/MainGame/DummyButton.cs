using System.Collections;
using System.ComponentModel;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.UI;

public class DummyButton : MonoBehaviour
{
    public int multiplier = 1;
    public int basePoints = 1;
    public int multiplierOfSkin = 1;
    public static float criticalMultiplier = 1.5f;
    public static float criticalChance = 0f;

    public GameObject visualClickObject;
    public Text clicktext;


    private void Start()
    {
        visualClickObject.SetActive(false);
    }

    public void MainButtonAction()
    {

        float randValue = Random.value;
        if (randValue > (1.0f - criticalChance))
        {
            int creditsWithCrit = (int)System.Math.Round(basePoints * multiplier * multiplierOfSkin * criticalMultiplier);
            Account.credits += creditsWithCrit;
            VisualizeButtonClick();
            clicktext.color = Color.red;
            clicktext.text = "+" + creditsWithCrit;
        }
        else
        {
            int creditsWithoutCrit = basePoints * multiplier * multiplierOfSkin;
            Account.credits += creditsWithoutCrit;
            VisualizeButtonClick();
            clicktext.color = Color.black;
            clicktext.text = "+" + creditsWithoutCrit;
        }
        AchievementManager.Instance.CheckForAchievements();

    }

    public void WorkerAction(int worker)
    {
        AchievementManager.Instance.CheckForAchievements();
        Account.credits += basePoints * multiplierOfSkin * worker;
    }


    public void MultiplyMultiplier(int multi)
    {
        multiplier *= multi;
    }

    public void RemoveMultiplier(int multi)
    {
        multiplier /= multi;
    }

    public void AddCriticalChance(float chance)
    {
        criticalChance += chance;
    }

    public void RemoveCriticalChance(float chance)
    {
        criticalChance -= chance;
    }

    public void MultiplyCriticalMultiplier(float critMulti)
    {
        criticalMultiplier *= critMulti;
    }

    public void RemoveCriticalMultiplier(float critMulti)
    {
        criticalMultiplier /= critMulti;
    }

    public void SetSkinMultiplier(int multi)
    {
        multiplierOfSkin = multi;
    }

    public void RemoveSkinMultiplier()
    {
        multiplierOfSkin = 1;
    }

    public void SetSkin(Sprite skin)
    {
        this.GetComponentInChildren<Text>().text = "";
        this.GetComponent<Image>().color = Color.white;
        //this.GetComponent<RectTransform>().localScale = new Vector3(30, 60);
        this.GetComponent<Image>().sprite = skin;
    }

    public void VisualizeButtonClick()
    {
        visualClickObject.SetActive(false);
        visualClickObject.transform.position =
            new Vector3(Random.Range(Screen.width / 2 * 0.95f, Screen.width / 2 * 1.3f), Random.Range(Screen.height / 2 * 0.8f, Screen.height / 2 * 1.18f), 0);
        visualClickObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(Fly());
    }

    // Numbers are flying for 19 pixels upwards
    IEnumerator Fly()
    {
        for (int i = 0; i <= 19; i++)
        {
            yield return new WaitForSeconds(0.01f);
            visualClickObject.transform.position = new Vector3(visualClickObject.transform.position.x, visualClickObject.transform.position.y + 2, 0);
        }
        visualClickObject.SetActive(false);
    }

}
