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
    public override Sprite icon { get; set; }
    public override ItemTemplate shopItem { get; set; }
    public Button purchaseButton;
    private bool hasUpdated = false;
    private int creditsPerSec;
    private int workerAmount = 0;
    private float timer = 0.0f;
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
        return shopItem.amount += 1;
    }

    //Hardcoded value, if changed --> tests have to change too
    public override int CalculateNewPrice()
    {
        return shopItem.price *= 2;
    }
}
