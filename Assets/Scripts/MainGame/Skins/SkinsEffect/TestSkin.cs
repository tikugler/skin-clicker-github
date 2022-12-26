using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSkin : SkinEffect
{
    public static int skinMulti = 5;
    public static float critChance = 0.2f;
    public static int critMulti = 10;
    public override string id { get; set; } = SkinNames.TestEffect;
    public override int price { get; set; } = 10;
    public override string description { get; set; } = "TestSkin with good buffs!\nSkin Multiplicator: " + skinMulti + "\nCritical Chance: " + critChance + "\nCritical Multiplicator: " + critMulti;
    public override bool bought { get; set; } = false;
    public override string rarity { get; set; } = Rarities.Legendary;
    public override int multiplicatorOfSkin { get; set; } = skinMulti;
    public override float criticalChance { get; set; } = critChance;
    public override float criticalMultiplicator { get; set; } = critMulti;
    public override Sprite icon { get; set; } = Resources.Load<Sprite>("OurBoiii");
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
        Account.ActiveSkin = this;
        EffectOfSkin();
        ContentDistributor.contentDistributor.mainButton.SetSkin(icon);
        ContentDistributor.contentDistributor.parallax.SwitchBackground(BackgroundArrays.Graveyard);
    }
}
