using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class Worker : ItemEffect
{
    public override string id { get; set; } = ItemNames.Worker;
    public override int price { get; set; } = 1;
    public override string description { get; set; } = "Automated button clicker.\n1 click per secound.";
    public override string rarity { get; set; } = Rarities.Common;
    public override Sprite icon { get; set; } = Resources.Load<Sprite>("worker");
    public override ItemTemplate shopItem { get; set; }
    public int creditsPerSec;
    public static int workerAmount = 0;
    private int credits;

    public static int workerAmountWorkaround = 0;

    void Update()
    {
        creditsPerSec = workerAmount * 5;

        credits = creditsPerSec;
        Account.credits += credits;
    }

    public override void PurchaseButtonAction(ItemTemplate shopItem)
    {
        this.shopItem = shopItem;
        workerAmountWorkaround++;
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
        shopItem.amount += 1;
        int newAmount = shopItem.amount;
        GameObject.Find("multipleworker").GetComponent<VisualFeedBackWorker>().MultipleWorker(newAmount);
        return newAmount; 
    }

    //Hardcoded value, if changed --> tests have to change too
    public override int CalculateNewPrice()
    {
        //Debug.Log("Worker Price(Template): " + shopItem.price);
        //Debug.Log("Worker Price: " + price);

        return shopItem.price *= 2;
    }

    public int GetWorkerAmount() 
    {
        return workerAmount;
    }
}
