using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Base class for every item in the game. 
/// </summary>
public abstract class ItemEffect
{
    public abstract string id { get; set; } ///value must be a static string of class Rarities
    public abstract int price { get; set; } ///price of item in int
    public abstract string description { get; set; } ///short description of item and effect of item
    public abstract string rarity { get; set; } ///value must be a static string of class Rarities
    public abstract Sprite icon { get; set; } ///=Resources.Load<Sprite>("iconName");   Default: Resources folder, don't add .png
    public abstract ItemTemplate shopItem { get; set; } ///ItemTemplate for displaying values ​​in the shop, added automatically by ContentDistributor


    /// <summary>
    /// Action of PurchaseButton in Shop System.
    /// Most of the time, the body should include:
    ///     this.shopItem = shopItem;  
    ///     CalculateNewPrice();
    ///     CalculateNewAmount();
    ///     EffectOfItem();
    /// If item should be added to inventory:
    ///     ContentDistributor.contentDistributor.boughtItemsOfPlayer.Add(this);
    /// </summary>
    /// <param name="shopItem"></param>
    public abstract void PurchaseButtonAction(ItemTemplate shopItem);

    /// <summary>
    /// Calculates new price.
    /// </summary>
    /// <returns>New price</returns>
    public abstract int CalculateNewPrice();

    /// <summary>
    /// Calculates new amount of item.
    /// </summary>
    /// <returns>New amount</returns>
    public abstract int CalculateNewAmount();

    /// <summary>
    /// Unique effect of item.
    /// </summary>
    public abstract void EffectOfItem();
}
