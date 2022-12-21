using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    private GameObject canvasGameObject;
    private ContentDistributor distributor;
    private Button mainButton;
    public string doubleId = ItemNames.DoubleEffect;

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
    public IEnumerator TestDoubleEffect()
    {
        DummyButton button = mainButton.GetComponent<DummyButton>();
        button.multiplier = 1;
        yield return null;

        DoubleEffect effect = new DoubleEffect();
        effect.EffectOfItem();
        yield return null;

        Assert.AreEqual(button.multiplier, 2);
        yield return null;

        effect.EffectOfItem();
        Assert.AreEqual(button.multiplier, 4);

    }

    [UnityTest]
    public IEnumerator TestDoubleEffectPurchaseButtonAction()
    {
        DoubleEffect effect = new DoubleEffect();
        int startPrice = effect.price;
        int startAmount = 0;
        ItemTemplate item = new ItemTemplate();
        item.amount = startAmount;
        item.description = "Random Description";
        item.id = doubleId;
        item.icon = null;
        item.price = startPrice;
        item.title = doubleId;

        Assert.AreEqual(startAmount, item.amount);
        Assert.AreEqual(startPrice, item.price);
        yield return null;

        startPrice *= 4;
        startAmount += 1;
        effect.PurchaseButtonAction(item);
        Assert.AreEqual(startAmount, item.amount);
        Assert.AreEqual(startPrice, item.price);
        yield return null;

        startPrice *= 4;
        startAmount += 1;
        effect.PurchaseButtonAction(item);
        Assert.AreEqual(startAmount, item.amount);
        Assert.AreEqual(startPrice, item.price);
    }
}
