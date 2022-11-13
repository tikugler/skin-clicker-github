using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class DoubleEffect : ItemEffect
{
    //Amount of Double/2X Item in player inventory. 
    public new string id = "DoubleEffect";
    private int multiplicator = 1;


    public DoubleEffect (ShopManager manager) : base (manager) {
        base.shopManager = manager;
    }
    public void Start() {
        shopManager.dummyButtonObj.SetMultiplicator(multiplicator);
    }

    public override void PurchaseButtonAction() {
        EffectOfItem();
    }

    public override int CalculateNewPrice(ItemTemplate shopItem) {
        return shopItem.price *= 4;
    }

    public override void EffectOfItem()
    {
        multiplicator *= 2;
        shopManager.dummyButtonObj.multiplicator = multiplicator;
    }
}