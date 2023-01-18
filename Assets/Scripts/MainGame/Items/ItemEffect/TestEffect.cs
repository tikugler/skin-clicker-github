using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// TestItem for displaying item in inventory.
/// </summary>
public class TestEffect : ItemEffect
{
    public override string id { get; set; } = ItemNames.TestEffect;
    public override int price { get; set; } = 0;
    public override string description { get; set; } = "Berkans Hoffnungen und Träume.\nLeise und Leer, existiert aber <3";
    public override string rarity { get; set; } = Rarities.Common;
    public override Sprite icon { get; set; } = Resources.Load<Sprite>("berkancoin");
    public override ItemTemplate shopItem { get; set; }

    public override void PurchaseButtonAction(ItemTemplate shopItem)
    {
        this.shopItem = shopItem;
        shopItem.price = price;
        ContentDistributor.contentDistributor.boughtItemsOfPlayer.Add(this);
        CalculateNewAmount();
    }

    //Random color for header to show useButtonEffect.
    public override void EffectOfItem()
    {
        var header = GameObject.Find("Header");
        header.GetComponent<Image>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        shopItem.amount -= 1;
    }

    public override int CalculateNewAmount()
    {
        return shopItem.amount += 1;
    }

    public override int CalculateNewPrice()
    {
        return shopItem.price;
    }
}
