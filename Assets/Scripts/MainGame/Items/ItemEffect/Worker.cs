using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class Worker : ItemEffect
{
    public new string id = ItemNames.Worker;
    public new ItemTemplate shopItem;
    public Button purchaseButton;
    private bool hasUpdated = false;
    private int creditsPerSec;
    private int workerAmount = 0;
    private float timer = 0.0f;
    private int credits;


    void Update()
    {
        creditsPerSec = workerAmount * 5;

        credits = creditsPerSec;
        Account.credits += credits;
    }

    public override void PurchaseButtonAction(ItemTemplate shopItem)
    {
        this.shopItem = shopItem;
        CalculateNewAmount();
        CalculateNewPrice();
        EffectOfItem();
    }

    //Refreshrate is set to "yield return new WaitForSeconds(1);" in AutomatedButtonWorkers, if changed, tests have to change too
    public override void EffectOfItem()
    {
        workerAmount++;
        GameObject.FindGameObjectWithTag("MainButton").GetComponent<AutomatedButtonWorkers>().SetLevel1WorkerCount(workerAmount);
    }

    public override int CalculateNewAmount()
    {
        return shopItem.amount += 1;
    }

    //Hardcoded value, if changed --> tests have to change too
    public override int CalculateNewPrice()
    {
        return shopItem.price *= 2;
    }
}
