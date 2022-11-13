using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestEffect : ItemEffect
{
    public new string id = "TestEffect";
    public TestEffect (ShopManager manager) : base (manager) {
        base.shopManager = manager;
    }

    public override void PurchaseButtonAction() {
        ShopManager.boughtItems.Add(this);
    }

    //Random color for header to show useButtonEffect.
    public override void EffectOfItem()
    {
        var header = GameObject.Find("Header");
        header.GetComponent<Image>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }
}
