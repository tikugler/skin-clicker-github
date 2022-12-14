using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "shopItem", menuName = "ScriptableObjects/New Shop Item", order = 1)]
public class ItemTemplate : ScriptableObject
{
    public string id;
    public string title;
    public string description;
    public int startPrice;
    public string rarity;
    // price = startPrice * (ItemEffect.CalculateNewPrice.multiplier) ^ amount
    // (DoubleEffect) For example amount=3, startPrice=1, multiplier=4
    // price = 1 * (4 ^ 3) = 64 (this is the price of 4 times upgraded DoubleEffect)
    [HideInInspector]
    public int price; 
    public int amount;
    public Sprite icon;
}
