using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "skinItem", menuName = "ScriptableObjects/New Skin Item", order = 1)]
public class SkinTemplate : ScriptableObject
{
    public string id;
    public string title;
    public string description;
    
    [HideInInspector]
    public int price;
    //public int amount;
    public Sprite icon;
    public Sprite fullPicture;
}