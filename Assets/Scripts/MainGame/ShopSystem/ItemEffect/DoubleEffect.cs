using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class DoubleEffect : ItemEffect
{
    //Amount of Double/2X Item in player inventory. 
    //public Text itemPrice; 
    //public Text itemAmount; 
    public ShopManager shopManager;
    public ShopItem shopItem;
    public Button purchaseButton;
    private int multiplicator = 1;

    public void Start() {
        shopManager.dummyButtonObj.SetMultiplicator(multiplicator);
    }

    public new void PurchaseButtonAction() {
        multiplicator *= 2;
        shopManager.dummyButtonObj.SetMultiplicator(multiplicator);
    }
}