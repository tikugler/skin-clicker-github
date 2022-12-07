using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
*  SkinEffect is a base class for every effect item. 
*  The id of an SubClass of ItemEffect is alsways the ClassName (1:1)!
*/
public abstract class SkinEffect
{
    public string id;
    public int price;
    public int skinMultiplicator;
    public int skinCrit;
    public bool bought;
    public string rarity;
    public SkinTemplate skinTemplate;


    //Action for the purchase button in the shop ui of an item.
    public virtual void PurchaseButtonAction(SkinTemplate skinTemplate)
    {
        this.skinTemplate = skinTemplate;
    }

    //Applies effect of skin to game.
    public abstract void EffectOfSkin();

    //Uses/Equips skin --> setter for current skin.
    public abstract void EquipSkin();
}
