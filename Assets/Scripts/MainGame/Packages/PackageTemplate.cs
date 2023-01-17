using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ItemTemplate is connected to the PreFabs and allows to display values.
/// </summary>
[CreateAssetMenu(fileName = "shopItem", menuName = "ScriptableObjects/New Shop Package", order = 3)]
public class PackageTemplate : ScriptableObject
{
    public string id;
    public string title;
    public string description;
    public string rarity;

    [HideInInspector]
    public int price;
    //public int amount;
    public int startPrice;
    public Sprite icon; //mb like a thumbnail
    public Sprite fullPicture;
}
