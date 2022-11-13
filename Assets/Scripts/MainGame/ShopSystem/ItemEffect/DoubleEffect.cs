using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class DoubleEffect : ItemEffect
{
    //Amount of Double/2X Item in player inventory. 
    public new string id = "DoubleEffect";
    public ShopItem shopItem;
    public Button purchaseButton;
    private int multiplicator = 1;
    private bool hasUpdated = false;

    public DoubleEffect (ShopManager manager) : base (manager) {
        base.shopManager = manager;
    }
    public void Start() {
        shopManager.dummyButtonObj.SetMultiplicator(multiplicator);
    }

    public override void PurchaseButtonAction() {
        multiplicator *= 2;
        shopManager.dummyButtonObj.multiplicator = multiplicator;
    }

    public override int CalculateNewPrice(ShopItem shopItem) {
        return shopItem.price *= 4;
    }


}