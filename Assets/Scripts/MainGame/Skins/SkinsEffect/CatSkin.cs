using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSkin : SkinEffect
{
    public static int skinMulti = 10;
    public static float critChance = 0.5f;
    public static int critMulti = 20;
    public override string id { get; set; } = SkinNames.Cat;
    public override int price { get; set; } = 1500;
    public override string description { get; set; } = "Black cats mean luck, don't they?\n" +
                                                        "Skin Multiplicator: " + skinMulti +
                                                        "\nCritical Chance: " + critChance +
                                                        "\nCritical Multiplicator: " + critMulti;
    public override bool bought { get; set; } = false;
    public override string rarity { get; set; } = Rarities.Rare;
    public override int multiplicatorOfSkin { get; set; } = skinMulti;
    public override float criticalChance { get; set; } = critChance;
    public override float criticalMultiplicator { get; set; } = critMulti;
    public override Sprite icon { get; set; } = Resources.Load<Sprite>("ButtonSkins/cat");
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
        ContentDistributor.contentDistributor.parallax.SwitchBackground(BackgroundArrays.Graveyard);
        ContentDistributor.contentDistributor.mainButton.SetSkin(icon);
        Account.ActiveSkin = this;
        EffectOfSkin();
    }
}
