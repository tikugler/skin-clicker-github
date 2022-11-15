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
    public int price;
    public int amount;
    public Sprite icon;
}
