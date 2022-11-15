using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class DoubleEffect : ItemEffect
{
    //Amount of Double/2X Item in player inventory. 
    public new string id = "DoubleEffect";
    public new ItemTemplate shopItem;
    private int multiplicator = 1;



    public void Start() 
    {
        ContentDistributor.contentDistributor.mainButton.SetMultiplicator(multiplicator);
    }

    public override void PurchaseButtonAction(ItemTemplate shopItem) 
    {
        this.shopItem = shopItem;
        CalculateNewPrice();
        CalculateNewAmount();
        EffectOfItem();
    }

    public override int CalculateNewPrice() 
    {
        return shopItem.price *= 4;
    }

    public override void EffectOfItem()
    {
        multiplicator *= 2;
        ContentDistributor.contentDistributor.mainButton.SetMultiplicator(multiplicator);
    }

    public override int CalculateNewAmount()
    {
        return shopItem.amount += 1;
    }
}