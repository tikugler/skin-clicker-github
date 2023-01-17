using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "skinItem", menuName = "ScriptableObjects/New Skin Item", order = 1)]
public class SkinTemplate : ScriptableObject
{
    public string id;
    public string title;
    public string description;
    public string rarity;

    [HideInInspector]
    public int price;
    public int PriceAsRealMoney { get
        {
            return price / 100;
        }
    }

    //public int amount;
    public int startPrice;
    public Sprite icon; //mb like a thumbnail
    public Sprite fullPicture;
}