using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Item increases critical hit chance.
/// </summary>
public class CriticalHitEffect : ItemEffect
{
    public override string id { get; set; } = ItemNames.CriticalHitEffect;
    public override int price { get; set; } = 2;
    public override string description { get; set; } = "Increase Critical Hit by " + (int)(critChance * 100) + "%.\n" +
                                                        "Item is stackable.";
    public override string rarity { get; set; } = Rarities.Common;
    public override Sprite icon { get; set; } = Resources.Load<Sprite>("crit");
    public override ItemTemplate shopItem { get; set; }
    private static float critChance = 0.05f;

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
        return shopItem.price *= 10;
    }

    public override void EffectOfItem()
    {
        ContentDistributor.contentDistributor.mainButton.AddCriticalChance(critChance);
    }

    public override int CalculateNewAmount()
    {
        shopItem.amount += 1;
        int newAmount = shopItem.amount;
        return newAmount;
    }
}
