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
    public override string description { get; set; } = "TestSkin mit guten Attributen\n" +
                                                        "Skin Multiplikator: " + skinMulti +
                                                        "\nKrit. Treffer Chance: " + critChance +
                                                        "\nKrit. Treffer Multi: " + critMulti;
    public override bool bought { get; set; } = false;
    public override string rarity { get; set; } = Rarities.Legendary;
    public override int multiplierOfSkin { get; set; } = skinMulti;
    public override float criticalChance { get; set; } = critChance;
    public override float criticalMultiplier { get; set; } = critMulti;
    public override Sprite icon { get; set; } = Resources.Load<Sprite>("ButtonSkins/cactus");
    public override SkinTemplate skinTemplate { get; set; }

    public override void PurchaseButtonAction(SkinTemplate skinTemplate)
    {
        this.skinTemplate = skinTemplate;
        Account.skinList.Add(this);
    }

    public override void EquipSkin()
    {
        Account.ActiveSkin = this;
        EffectOfSkin();
        ContentDistributor.contentDistributor.mainButton.SetSkin(icon);
        ContentDistributor.contentDistributor.parallax.SwitchBackground(BackgroundArrays.FoggyMountain);
    }

    public override void EffectOfSkin()
    {
        ContentDistributor.contentDistributor.mainButton.SetSkinMultiplier(skinMulti);
        ContentDistributor.contentDistributor.mainButton.AddCriticalChance(criticalChance);
        ContentDistributor.contentDistributor.mainButton.MultiplyCriticalMultiplier(criticalMultiplier);
    }


    public override void UnequipSkin()
    {
        ContentDistributor.contentDistributor.mainButton.RemoveSkinMultiplier();
        ContentDistributor.contentDistributor.mainButton.RemoveCriticalChance(criticalChance);
        ContentDistributor.contentDistributor.mainButton.RemoveCriticalMultiplier(criticalMultiplier);
    }
}
