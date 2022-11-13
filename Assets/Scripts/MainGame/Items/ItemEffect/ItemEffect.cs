using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ItemEffect is a base class for every effect item.
public class ItemEffect
{
    public string id;
    public int price;
    public ItemTemplate shopItem;
    public ShopManager shopManager;


    //Action for the purchase button in the shop ui of an item.
    public virtual void PurchaseButtonAction(ItemTemplate shopItem){
        this.shopItem = shopItem;
        CalculateNewPrice();
        CalculateNewAmount();
    }

    //Calculates new price, default --> keeps old price.
    public virtual int CalculateNewPrice(){
        return shopItem.price;
    }

    public virtual int CalculateNewAmount(){
        return shopItem.amount += 1;
    }

    public virtual void EffectOfItem() {}
}
