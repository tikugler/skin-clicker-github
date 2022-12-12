using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoubleEffect : ItemEffect
{
    //Amount of Double/2X Item in player inventory. 
    public override string id { get; set; } = ItemNames.DoubleEffect;
    public override int price { get; set; } = 2;
    public override string description { get; set; } = "Doubles score and credits.\nItem is stackable.";
    public override string rarity { get; set; } = Rarities.Common;
    public override Sprite icon { get; set; } = Resources.Load<Sprite>("2X");
    public override ItemTemplate shopItem { get; set; }
    public int multiplicator = 1;



    public void Start()
    {
        ContentDistributor.contentDistributor.mainButton.SetMultiplicator(multiplicator);
        shopItem.price = price;
    }

    public override void PurchaseButtonAction(ItemTemplate shopItem)
    {
        this.shopItem = shopItem;
        CalculateNewPrice();
        CalculateNewAmount();
        EffectOfItem();
    }

    //Increasment is hardcoded, if changed, also change tests of TestDoubleEffect.
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