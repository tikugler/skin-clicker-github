using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "shopMenu", menuName = "ScriptableObjects/New Shop Item", order = 1)]
public class ShopItem : ScriptableObject
{
    public string id;
    public string title;
    public string description;
    public int price;
    public int amount;
    public Sprite icon;
}
