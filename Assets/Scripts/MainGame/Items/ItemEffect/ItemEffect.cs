using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
*  ItemEffect is a base class for every effect item. 
*  The id of an SubClass of ItemEffect is alsways the ClassName (1:1)!
*/
public abstract class ItemEffect
{
    public string id;
    public int price;
    public ItemTemplate shopItem;


    //Action for the purchase button in the shop ui of an item.
    public virtual void PurchaseButtonAction(ItemTemplate shopItem)
    {
        this.shopItem = shopItem;
        CalculateNewPrice();
        CalculateNewAmount();
    }

    //Calculates new price, default --> keeps old price.
    public abstract int CalculateNewPrice();

    public abstract int CalculateNewAmount();

    public abstract void EffectOfItem();
}
