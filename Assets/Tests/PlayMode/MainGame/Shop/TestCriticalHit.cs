using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class TestCriticalHit : MonoBehaviour
{
    private GameObject canvasGameObject;
    private ContentDistributor distributor;
    private Button mainButton;
    public string critId = ItemNames.CriticalHitEffect;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        SceneManager.LoadScene("MainGame");
    }


    [UnitySetUp]
    public IEnumerator Setup()
    {
        yield return null; //Wait till next update
        canvasGameObject = GameObject.Find("Canvas");

        GameObject contentDistributor = FindObjectHelper.
            FindObjectInParent(canvasGameObject, "ContentDistributor");
        distributor = contentDistributor.GetComponentInParent<ContentDistributor>();

        GameObject mainButton = FindObjectHelper.
            FindObjectInParent(canvasGameObject, "MainButton");
        this.mainButton = mainButton.GetComponent<Button>();
    }

    [UnityTest]
    public IEnumerator TestCriticalEffect()
    {
        DummyButton button = mainButton.GetComponent<DummyButton>();
        DummyButton.criticalChance = 0;
        yield return null;

        CriticalHitEffect effect = new CriticalHitEffect();
        effect.EffectOfItem();
        yield return null;

        Assert.AreEqual(DummyButton.criticalChance, 0.05f);
        yield return null;

        effect.EffectOfItem();
        Assert.AreEqual(DummyButton.criticalChance, 0.1f);

    }

    [UnityTest]
    public IEnumerator TestCriticalEffectPurchaseButtonAction()
    {
        CriticalHitEffect effect = new CriticalHitEffect();
        int startPrice = effect.price;
        int startAmount = 0;
        ItemTemplate item = new ItemTemplate();
        item.amount = startAmount;
        item.description = "Random Description";
        item.id = critId;
        item.icon = null;
        item.price = startPrice;
        item.title = critId;

        Assert.AreEqual(startAmount, item.amount);
        Assert.AreEqual(startPrice, effect.price);
        yield return null;

        Debug.Log("First");
        startPrice *= 10;
        Debug.Log("startPrice = " + startPrice);
        startAmount += 1;
        effect.PurchaseButtonAction(item);
        Debug.Log("startPrice = " + startPrice);
        Assert.AreEqual(startAmount, item.amount);
        Assert.AreEqual(startPrice, item.price);
        yield return null;

        Debug.Log("Sec");
        startPrice *= 10;
        startAmount += 1;
        effect.PurchaseButtonAction(item);
        Assert.AreEqual(startAmount, item.amount);
        Assert.AreEqual(startPrice, item.price);
    }
}
