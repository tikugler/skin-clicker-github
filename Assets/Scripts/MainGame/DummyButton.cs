using System.Collections;
using System.ComponentModel;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.UI;

public class DummyButton : MonoBehaviour
{
    public int multiplicator = 1;
    public int basePoints = 1;
    public int multiplicatorOfSkin = 1;
    public static float criticalMultiplicator = 1;
    public static float criticalChance = 0f;

    public GameObject visualClickObject;
    public Text clicktext;


    private void Start()
    {
        visualClickObject.SetActive(false);
    }

    private void Update()
    {
        float randValue = Random.value;
        if (randValue > (1.0f - criticalChance))
        {

            int pointsWithCrit = (int)System.Math.Round(basePoints * multiplicator * multiplicatorOfSkin * criticalMultiplicator);
            clicktext.text = "+" + pointsWithCrit;
        }
        else
        { 
            int pointsWithoutCrit = basePoints * multiplicator * multiplicatorOfSkin;
            clicktext.text = "+" + pointsWithoutCrit;

        }
    }

    public void MainButtonAction()
    {
        float randValue = Random.value;
        if (randValue > (1.0f - criticalChance))
        {
            Account.credits += (int)System.Math.Round(basePoints * multiplicator * multiplicatorOfSkin * criticalMultiplicator);
            VisualizeButtonClick();
        }
        else
        {
            Account.credits += basePoints * multiplicator * multiplicatorOfSkin;
            VisualizeButtonClick();
        }
        
    }

    public void WorkerAction(int worker)
    {
        Account.credits += basePoints * multiplicatorOfSkin * worker;
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

    public void VisualizeButtonClick() {
        
        visualClickObject.SetActive(false);
        visualClickObject.transform.position = new Vector3(1150f, 580f, 0);
        visualClickObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(Fly());

    }

    IEnumerator Fly() {
        for (int i = 0; i <= 19; i++) 
        { 
        yield return new WaitForSeconds(0.01f);
        visualClickObject.transform.position = new Vector3(visualClickObject.transform.position.x, visualClickObject.transform.position.y, 0);
        }
        visualClickObject.SetActive(false) ;
    }
    
}
