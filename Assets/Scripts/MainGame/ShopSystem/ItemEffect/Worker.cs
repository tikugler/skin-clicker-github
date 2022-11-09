using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

class Worker : ItemEffect
{
    public new string id = "Worker";
    public ShopItem shopItem;
    public Button purchaseButton;
    private bool hasUpdated = false;
    private int creditsPerSec;
    private int workerAmount = 0;
    private float timer = 0.0f;
    private int credits;

    public Worker(ShopManager manager) : base(manager)
    {
        base.shopManager = manager;
    }

    void Update()
    {
        creditsPerSec = workerAmount * 5;
        
        credits = creditsPerSec;
        shopManager.dummyButtonObj.credits =+credits;
        Debug.Log(credits);
    }

    public override void PurchaseButtonAction() 
    {
        workerAmount++;
        GameObject.FindGameObjectWithTag("MainButton").GetComponent<AutomatedButtonWorkers>().SetLevel1WorkerCount(workerAmount);
    }

    public override int CalculateNewPrice(ShopItem shopItem)
    {
        return shopItem.price *= 2;
    }
}
