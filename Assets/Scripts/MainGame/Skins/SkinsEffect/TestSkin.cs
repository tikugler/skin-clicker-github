using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSkin : SkinEffect
{
    public new string id = SkinNames.TestEffect;
    public new int price = 10;
    public new bool bought = false;
    public new string rarity = Rarities.Legendary;
    public new int multiplicatorOfSkin = 5;
    public new float criticalChance = 0.2f;
    public new float criticalMultiplicator = 10;

    public override void PurchaseButtonAction(SkinTemplate skinTemplate)
    {
        this.skinTemplate = skinTemplate;
        Account.skinList.Add(this);
    }

    public override void EffectOfSkin()
    {
        ContentDistributor.contentDistributor.mainButton.multiplicatorOfSkin = multiplicatorOfSkin;
        DummyButton.criticalChance = criticalChance;
        DummyButton.criticalMultiplicator = criticalMultiplicator;
    }

    public override void EquipSkin()
    {
        Account.activeSkin = this;
        EffectOfSkin();
    }
}
