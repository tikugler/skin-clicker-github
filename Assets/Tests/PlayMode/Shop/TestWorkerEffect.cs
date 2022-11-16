using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class TestWorkerEffect : MonoBehaviour
{
    private GameObject canvasGameObject;
    private ContentDistributor distributor;
    private Button mainButton;
    public string workerId = "Worker";

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
    public IEnumerator TestWorkerEffectOfItem()
    {
        int demoCredits = 0;
        DummyButton button = mainButton.GetComponent<DummyButton>();
        button.multiplicator = 1;
        button.basePoints = 1;
        button.credits = demoCredits;
        yield return null;

        Assert.AreEqual(button.credits, demoCredits);
        Worker worker = new Worker();
        worker.EffectOfItem();
        yield return new WaitForSeconds(1);

        Assert.AreEqual(button.credits, 1);
        yield return new WaitForSeconds(1);

        Assert.AreEqual(button.credits, 2);

    }

    [UnityTest]
    public IEnumerator TestDoubleEffectPurchaseButtonAction()
    {
        int startPrice = 10;
        int startAmount = 0;
        ItemTemplate item = new ItemTemplate();
        item.amount = startAmount;
        item.description = "Random Description";
        item.id = workerId;
        item.icon = null;
        item.price = startPrice;
        item.title = workerId;

        DoubleEffect effect = new DoubleEffect();
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
