using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "shopMenu", menuName = "ScriptableObjects/New Shop Item", order = 1)]
public class ShopItem : ScriptableObject
{
    public string id;
    public string title;
    public string description;
    public int price;
}
