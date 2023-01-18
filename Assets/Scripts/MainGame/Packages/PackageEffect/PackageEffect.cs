using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
*  SkinEffect is a base class for every effect item. 
*  The id of an SubClass of ItemEffect is alsways the ClassName (1:1)!
*/
public abstract class PackageEffect
{
    public abstract string id { get; set; }
    public abstract int price { get; set; }
    public abstract int realMoneyAmount { get; set; }
    public abstract int creditsAmount { get; set; }
    public abstract string rarity { get; set; }
    public abstract Sprite icon { get; set; }
    public abstract PackageTemplate packageTemplate { get; set; }


    //Action for the purchase button in the shop ui of an item.
    public void PurchaseButtonAction(PackageTemplate packageTemplate)
    {
        Account.realMoney += realMoneyAmount;
        Account.credits += creditsAmount;
    }

    //Action for the purchase button in the shop ui of an item.
    public string GetDescription()
    {
        return $"enthält {realMoneyAmount} Realmoney und {creditsAmount} Credits";
    }

    //Applies effect of skin to game.
    public void EffectOfPackage()
    {
        Account.realMoney += realMoneyAmount;
    }

}
