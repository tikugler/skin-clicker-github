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
    public IEnumerator TestAutomatedWorkerPurchaseButtonAction()
    {
        int startPrice = 5;
        int startAmount = 0;
        ItemTemplate item = new ItemTemplate();
        item.amount = startAmount;
        item.description = "Random Description";
        item.id = workerId;
        item.icon = null;
        item.price = startPrice;
        item.title = workerId;

        Worker effect = new Worker();
        Assert.AreEqual(startAmount, item.amount);
        Assert.AreEqual(startPrice, item.price);
        yield return null;

        //Hardcoded values in Worker Class 
        startPrice *= 2;
        startAmount += 1;
        effect.PurchaseButtonAction(item);
        Assert.AreEqual(startAmount, item.amount);
        Assert.AreEqual(startPrice, item.price);
        yield return null;

        startPrice *= 2;
        startAmount += 1;
        effect.PurchaseButtonAction(item);
        Assert.AreEqual(startAmount, item.amount);
        Assert.AreEqual(startPrice, item.price);
    }

    [UnityTest]
    public IEnumerator TestEffectOfWorker()
    {
        DummyButton button = mainButton.GetComponent<DummyButton>();
        button.multiplicator = 1;
        button.credits = 0;
        yield return null;

        Worker effect = new Worker();
        Assert.AreNotEqual(button.credits, 1);
        effect.EffectOfItem();
        yield return new WaitForSeconds(1);

        Assert.AreEqual(button.credits, 1);
        yield return new WaitForSeconds(1);

        Assert.AreEqual(button.credits, 2);
    }

    [UnityTest]
    public IEnumerator TestTwoWorkers()
    {
        DummyButton button = mainButton.GetComponent<DummyButton>();
        button.multiplicator = 1;
        button.credits = 0;
        yield return null;

        Worker effect = new Worker();
        Assert.AreNotEqual(button.credits, 1);
        effect.EffectOfItem();
        effect.EffectOfItem();

        Assert.AreNotEqual(button.credits, 1);
        yield return new WaitForSeconds(1);

        Assert.AreEqual(button.credits, 2);
        yield return new WaitForSeconds(1);

        Assert.AreEqual(button.credits, 4);

        yield return new WaitForSeconds(3);
        Assert.AreEqual(button.credits, 10);
    }
}
