using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalHitEffect : ItemEffect
{

    public override string id { get; set; } = ItemNames.CriticalHitEffect;
    public override int price { get; set; } = 2;
    public override string description { get; set; } = "Increase Critical Hits by 5%.\n" +
                                                        "Item is stackable.";
    public override string rarity { get; set; } = Rarities.Common;
    public override Sprite icon { get; set; } = Resources.Load<Sprite>("");
    public override ItemTemplate shopItem { get; set; }

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
        return price * 2;
    }

    public override void EffectOfItem()
    {

    }

    public override int CalculateNewAmount()
    {
        shopItem.amount += 1;
        int newAmount = shopItem.amount;
        return newAmount;
    }
}
