using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
*  SkinEffect is a base class for every effect item. 
*  The id of an SubClass of ItemEffect is alsways the ClassName (1:1)!
*/
public abstract class SkinEffect
{
    public abstract string id { get; set; }
    public abstract int price { get; set; }
    public abstract string description { get; set; }
    public abstract bool bought { get; set; }
    public abstract string rarity { get; set; }
    public abstract int multiplierOfSkin { get; set; }
    public abstract float criticalChance { get; set; }
    public abstract float criticalMultiplier { get; set; }
    public abstract Sprite icon { get; set; }
    public abstract SkinTemplate skinTemplate { get; set; }


    //Action for the purchase button in the shop ui of an item.
    public abstract void PurchaseButtonAction(SkinTemplate skinTemplate);

    //Applies effect of skin to game.
    public abstract void EffectOfSkin();

    //Uses/Equips skin --> setter for current skin.
    public abstract void EquipSkin();
}
