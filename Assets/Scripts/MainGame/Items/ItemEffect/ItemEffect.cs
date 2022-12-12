using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
*  ItemEffect is a base class for every effect item. 
*  The id of an SubClass of ItemEffect is alsways the ClassName (1:1)!
*/
public abstract class ItemEffect
{
    public abstract string id { get; set; }
    public abstract int price { get; set; }
    public abstract string description { get; set; }
    public abstract string rarity { get; set; }
    public abstract Sprite icon { get; set; }
    public abstract ItemTemplate shopItem { get; set; }


    //Action for the purchase button in the shop ui of an item.
    public abstract void PurchaseButtonAction(ItemTemplate shopItem);

    //Calculates new price, default --> keeps old price.
    public abstract int CalculateNewPrice();

    public abstract int CalculateNewAmount();

    public abstract void EffectOfItem();
}
