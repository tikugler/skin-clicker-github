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
    public string workerId = ItemNames.Worker;

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
        Worker.workerAmount = 0; // this variable changed to static and needs to be set to zero before every test
    }

    [UnityTest]
    public IEnumerator TestWorkerEffectOfItem()
    {
        int demoCredits = 0;
        DummyButton button = mainButton.GetComponent<DummyButton>();
        button.multiplier = 1;
        button.basePoints = 1;
        Account.credits = demoCredits;
        yield return null;

        Assert.AreEqual(Account.credits, demoCredits);
        Worker worker = new Worker();
        worker.EffectOfItem();
        yield return new WaitForSeconds(1);

        Assert.AreEqual(Account.credits, 1);
        yield return new WaitForSeconds(1);

        Assert.AreEqual(Account.credits, 2);

    }

    [UnityTest]
    public IEnumerator TestAutomatedWorkerPurchaseButtonAction()
    {
        Worker effect = new Worker();
        int startPrice = effect.price;
        int startAmount = 0;
        ItemTemplate item = new ItemTemplate();
        item.amount = startAmount;
        item.description = "Random Description";
        item.id = workerId;
        item.icon = null;
        item.price = startPrice;
        item.title = workerId;

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
        button.multiplier = 1;
        Account.credits = 0;
        yield return null;

        Worker effect = new Worker();
        Assert.AreNotEqual(Account.credits, 1);
        effect.EffectOfItem();
        yield return new WaitForSeconds(1);

        Assert.AreEqual(Account.credits, 1);
        yield return new WaitForSeconds(1);

        Assert.AreEqual(Account.credits, 2);
    }

    [UnityTest]
    public IEnumerator TestTwoWorkers()
    {
        DummyButton button = mainButton.GetComponent<DummyButton>();
        button.multiplier = 1;
        Account.credits = 0;
        yield return null;

        Worker effect = new Worker();
        Assert.AreNotEqual(Account.credits, 1);
        effect.EffectOfItem();
        effect.EffectOfItem();

        Assert.AreNotEqual(Account.credits, 1);
        yield return new WaitForSeconds(1);

        Assert.AreEqual(Account.credits, 2);
        yield return new WaitForSeconds(1);

        Assert.AreEqual(Account.credits, 4);

        yield return new WaitForSeconds(3);
        Assert.AreEqual(Account.credits, 10);
    }
}
