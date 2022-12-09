using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSkin : SkinEffect
{
    public override string id { get; set; } = SkinNames.TestEffect;
    public override int price { get; set; } = 10;
    public override bool bought { get; set; } = false;
    public override string rarity { get; set; } = Rarities.Legendary;
    public override int multiplicatorOfSkin { get; set; } = 5;
    public override float criticalChance { get; set; } = 0.2f;
    public override float criticalMultiplicator { get; set; } = 10;
    public override SkinTemplate skinTemplate { get; set; }

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
