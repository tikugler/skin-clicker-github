using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultSkin : SkinEffect
{
    public static int skinMulti = 1;
    public static float critChance = 0.02f;
    public static int critMulti = 5;
    public override string id { get; set; } = SkinNames.Default;
    public override int price { get; set; } = 0;
    public override string description { get; set; } = "Unser Geschenk für Anfänger\n" +
                                                        "Skin Multiplikator: " + skinMulti +
                                                        "\nKrit. Treffer Chance: " + critChance +
                                                        "\nKrit. Treffer Multi: " + critMulti;
    public override bool bought { get; set; } = true;
    public override string rarity { get; set; } = Rarities.Common;
    public override int multiplierOfSkin { get; set; } = skinMulti;
    public override float criticalChance { get; set; } = critChance;
    public override float criticalMultiplier { get; set; } = critMulti;
    public override Sprite icon { get; set; } = Resources.Load<Sprite>("ButtonSkins/smash");
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
        ContentDistributor.contentDistributor.parallax.SwitchBackground(BackgroundArrays.Default);
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
