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

    [SerializeField] Camera UICamera;


    [SerializeField] SoundSceneManager soundManager;
    [SerializeField] VisualEffectManager visualEffectManager;



    private void Start()
    {
        visualClickObject.SetActive(false);

    }

    /// <summary>
    /// Calculates new value for credits/score and increases score, credits per click.
    /// </summary>
    public void MainButtonAction()
    {

        float randValue = Random.value;
        if (randValue > (1.0f - criticalChance))
        {
            soundManager.PlayCriticalHitSound();
            visualEffectManager.CriticalHitVisualEffect();
            int creditsWithCrit = (int)System.Math.Round(basePoints * multiplier * multiplierOfSkin * criticalMultiplier);
            Account.credits += creditsWithCrit;
            VisualizeButtonClick();
            clicktext.color = Color.red;
            clicktext.text = "+" + creditsWithCrit;

        }
        else
        {
            soundManager.PlayHitSound();
            visualEffectManager.HitVisualEffect();
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


    /// <summary>
    /// Multiplies the current multiplier by int multi.
    /// </summary>
    /// <param name="multi"></param>
    public void MultiplyMultiplier(int multi)
    {
        multiplier *= multi;
    }

    /// <summary>
    /// Divides the multiplier by multi.
    /// </summary>
    /// <param name="multi"></param>
    public void RemoveMultiplier(int multi)
    {
        multiplier /= multi;
    }

    /// <summary>
    /// Adds critical chance --> increase of critical hit chance.
    /// </summary>
    /// <param name="chance"></param>
    public void AddCriticalChance(float chance)
    {
        criticalChance += chance;
    }

    /// <summary>
    /// Removes critial chance by chance.
    /// </summary>
    /// <param name="chance"></param>
    public void RemoveCriticalChance(float chance)
    {
        criticalChance -= chance;
    }

    /// <summary>
    /// Multiplies the current criticalMultiplier by critMulti.
    /// </summary>
    /// <param name="critMulti"></param>
    public void MultiplyCriticalMultiplier(float critMulti)
    {
        criticalMultiplier *= critMulti;
    }

    /// <summary>
    /// Divides the criticalMultiplier by critMulti.
    /// </summary>
    /// <param name="critMulti"></param>
    public void RemoveCriticalMultiplier(float critMulti)
    {
        criticalMultiplier /= critMulti;
    }

    /// <summary>
    /// Sets multiplierOfSkin to multi.
    /// </summary>
    /// <param name="multi"></param>
    public void SetSkinMultiplier(int multi)
    {
        multiplierOfSkin = multi;
    }

    /// <summary>
    /// Removes multiplierOfSkin to default value 1.
    /// </summary>
    public void RemoveSkinMultiplier()
    {
        multiplierOfSkin = 1;
    }

    /// <summary>
    /// Setter for skin.
    /// </summary>
    /// <param name="skin"></param>
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
        
        // adapted to new camera for UI
        float randomSlipXRange = Screen.width / 8;
        float randomSlipYRange = Screen.width / 10;
        Vector3 worldPosition = UICamera.ScreenToWorldPoint(Input.mousePosition);
        visualClickObject.transform.localPosition = new Vector3(worldPosition.x + Random.Range(-1* randomSlipXRange, randomSlipXRange), worldPosition.y + Random.Range(-1 * randomSlipYRange, randomSlipYRange), 1);

        //visualClickObject.transform.position =
        //    new Vector3(Random.Range(Screen.width / 2 * 0.95f, Screen.width / 2 * 1.3f), Random.Range(Screen.height / 2 * 0.8f, Screen.height / 2 * 1.18f), 0);




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
            visualClickObject.transform.localPosition = new Vector3(visualClickObject.transform.localPosition.x, visualClickObject.transform.localPosition.y + 2, 1);
            //visualClickObject.transform.Position = new Vector3(visualClickObject.transform.localPosition.x, visualClickObject.transform.localPosition.y + 2, 0);


        }
        visualClickObject.SetActive(false);
    }

}
