using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanSkin : SkinEffect
{
    public static int skinMulti = 15;
    public static float critChance = 0.1f;
    public static int critMulti = 5;
    public override string id { get; set; } = SkinNames.Snowman;
    public override int price { get; set; } = 200;
    public override string description { get; set; } = "Christmas special\n" +
                                                        "Cold snowman in warm weather - safe him now or it will be too late" +
                                                        "Skin Multiplicator: " + skinMulti +
                                                        "\nCritical Chance: " + critChance +
                                                        "\nCritical Multiplicator: " + critMulti;
    public override bool bought { get; set; } = false;
    public override string rarity { get; set; } = Rarities.Legendary;
    public override int multiplierOfSkin { get; set; } = skinMulti;
    public override float criticalChance { get; set; } = critChance;
    public override float criticalMultiplier { get; set; } = critMulti;
    public override Sprite icon { get; set; } = Resources.Load<Sprite>("ButtonSkins/SnowmanSkin");
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
        ContentDistributor.contentDistributor.mainButton.multiplierOfSkin = multiplierOfSkin;
        DummyButton.criticalChance = criticalChance;
        DummyButton.criticalMultiplier = criticalMultiplier;
    }

    public override void EquipSkin()
    {
        ContentDistributor.contentDistributor.parallax.SwitchBackground(BackgroundArrays.Snow);
        ContentDistributor.contentDistributor.mainButton.SetSkin(icon);
        Account.ActiveSkin = this;
        EffectOfSkin();
    }

    public override void UnequipSkin()
    {
        ContentDistributor.contentDistributor.mainButton.RemoveSkinMultiplier();
        ContentDistributor.contentDistributor.mainButton.RemoveCriticalChance(criticalChance);
        ContentDistributor.contentDistributor.mainButton.RemoveCriticalMultiplier(criticalMultiplier);
    }
}
