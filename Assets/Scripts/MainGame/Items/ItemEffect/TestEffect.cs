using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestEffect : ItemEffect
{
    public new string id = "TestEffect";
    public new ItemTemplate shopItem;

    public override void PurchaseButtonAction(ItemTemplate shopItem) {
        this.shopItem = shopItem;
        ContentDistributor.contentDistributor.boughtItemsOfPlayer.Add(this);
        CalculateNewAmount();
    }

    //Random color for header to show useButtonEffect.
    public override void EffectOfItem()
    {
        var header = GameObject.Find("Header");
        header.GetComponent<Image>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    public override int CalculateNewAmount(){
        return shopItem.amount += 1;
    }

    public override int CalculateNewPrice() {
        return shopItem.price;
    }
}
