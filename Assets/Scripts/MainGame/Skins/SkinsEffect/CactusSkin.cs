using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusSkin : SkinEffect
{
    public static int skinMulti = 20;
    public static float critChance = 0.05f;
    public static int critMulti = 40;
    public override string id { get; set; } = SkinNames.Cactus;
    public override int price { get; set; } = 400;
    public override string description { get; set; } = "In Germany we say: Mein kleiner grüner Kaktus\n" +
                                                        "Skin Multiplicator: " + skinMulti +
                                                        "\nCritical Chance: " + critChance +
                                                        "\nCritical Multiplicator: " + critMulti;
    public override bool bought { get; set; } = false;
    public override string rarity { get; set; } = Rarities.Legendary;
    public override int multiplicatorOfSkin { get; set; } = skinMulti;
    public override float criticalChance { get; set; } = critChance;
    public override float criticalMultiplicator { get; set; } = critMulti;
    public override Sprite icon { get; set; } = Resources.Load<Sprite>("ButtonSkins/cactus");
    public override SkinTemplate skinTemplate { get; set; }
    private GameObject mainButton;

    public override void PurchaseButtonAction(SkinTemplate skinTemplate)
    {
        this.skinTemplate = skinTemplate;
        Account.skinList.Add(this);
        bought = true;
    }

    public override void EffectOfSkin()
    {
        ContentDistributor.contentDistributor.mainButton.multiplicatorOfSkin = multiplicatorOfSkin;
        DummyButton.criticalChance = criticalChance;
        DummyButton.criticalMultiplicator = criticalMultiplicator;
    }

    public override void EquipSkin()
    {
        ContentDistributor.contentDistributor.parallax.SwitchBackground(BackgroundArrays.Desert);
        ContentDistributor.contentDistributor.mainButton.SetSkin(icon);
        Account.ActiveSkin = this;
        EffectOfSkin();
    }
}
